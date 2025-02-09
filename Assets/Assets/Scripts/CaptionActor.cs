using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CaptionActor : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_TextMeshPro; //The TextMeshPro object that should be on the same game object this script is on
    public enum SizingMode { Automatic, Manual }

    [Header("Background Settings")]
    public Color backgroundColor = new Color(0, 0, 0, 0.5f); // Default: Semi-transparent black
    public SizingMode sizingMode = SizingMode.Automatic;
    public Vector2 manualSize = new Vector2(300, 100); // Default size for Manual mode
    public Vector2 buffer = new Vector2(10, 10); // Extra padding around the text

    private RectTransform backgroundRect;
    private Image backgroundImage;
    //private TextMeshProUGUI textMeshPro;

    private void OnEnable()
    {
        m_TextMeshPro = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        CaptionManager.OnCaptionSettingsChanged += UpdateCaptionsFromSettings;
        InitializeBackground();
    }
    private void UpdateCaptionsFromSettings(CaptionManager.CaptionSettings newSettings)
    {
        Color tempBgColor = backgroundColor;
        Color tempFontColor = m_TextMeshPro.color;

        //If captions are not enabled, set the opacity of text and background to 0
        if (!newSettings.captionsEnabled)
        {
            m_TextMeshPro.color = new Color(tempFontColor.r, tempFontColor.g, tempFontColor.b, 0f);
            backgroundColor = new Color(tempBgColor.r, tempBgColor.g, tempBgColor.b, 0f);
        }
        else
        {
            m_TextMeshPro.color = new Color(tempFontColor.r, tempFontColor.g, tempFontColor.b, 1f);
            m_TextMeshPro.fontSize = newSettings.captionTextSize;
            m_TextMeshPro.font = newSettings.captionFont;
            backgroundColor = new Color(tempBgColor.r, tempBgColor.g, tempBgColor.b, newSettings.captionBackground);
        }
        
        UpdateBackground();
    }

    private void InitializeBackground()
    {
        // Create a new GameObject for the background if it doesn't exist
        if (transform.Find("CaptionBackground") == null)
        {
            GameObject background = new GameObject("CaptionBackground");
            background.transform.SetParent(transform, false);

            // Add components for the background
            backgroundRect = background.AddComponent<RectTransform>();
            backgroundImage = background.AddComponent<Image>();

            // Set up the background to be rendered below the text
            backgroundImage.raycastTarget = false;
            backgroundImage.color = backgroundColor;
        }
        else
        {
            backgroundRect = transform.Find("CaptionBackground").GetComponent<RectTransform>();
            backgroundImage = backgroundRect.GetComponent<Image>();
        }

        // Ensure the background is at the bottom of the hierarchy
        backgroundRect.SetSiblingIndex(0);
    }

    private void UpdateBackground()
    {
        if (backgroundImage == null || backgroundRect == null || m_TextMeshPro == null)
            return;

        // Update background color
        backgroundImage.color = backgroundColor;
        backgroundRect.SetSiblingIndex(m_TextMeshPro.transform.GetSiblingIndex() - 1); //Ensure the background is always behind the text

        // Update size based on the selected sizing mode
        if (sizingMode == SizingMode.Automatic)
        {
            Vector2 textSize = new Vector2(m_TextMeshPro.preferredWidth, m_TextMeshPro.preferredHeight);
            backgroundRect.sizeDelta = textSize + buffer;
            backgroundRect.localPosition = new Vector3(0, -buffer.y,0);
        }
        else if (sizingMode == SizingMode.Manual)
        {
            backgroundRect.sizeDelta = manualSize;
        }
    }
}
