using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CaptionBackground : MonoBehaviour
{
    //TODO: Change this to be "CaptionManager" and integrate with AccessProfile
    public enum SizingMode { Automatic, Manual }

    [Header("Background Settings")]
    public Color backgroundColor = new Color(0, 0, 0, 0.5f); // Default: Semi-transparent black
    public SizingMode sizingMode = SizingMode.Automatic;
    public Vector2 manualSize = new Vector2(300, 100); // Default size for Manual mode
    public Vector2 buffer = new Vector2(10, 10); // Extra padding around the text

    private RectTransform backgroundRect;
    private Image backgroundImage;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        // Ensure there's a child GameObject for the background
        InitializeBackground();
    }

    private void Update()
    {
        UpdateBackground();
    }

    private void InitializeBackground()
    {
        // Get or create the TextMeshPro component
        textMeshPro = GetComponent<TextMeshProUGUI>();

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
        if (backgroundImage == null || backgroundRect == null || textMeshPro == null)
            return;

        // Update background color
        backgroundImage.color = backgroundColor;

        // Update size based on the selected sizing mode
        if (sizingMode == SizingMode.Automatic)
        {
            Vector2 textSize = new Vector2(textMeshPro.preferredWidth, textMeshPro.preferredHeight);
            backgroundRect.sizeDelta = textSize + buffer;
        }
        else if (sizingMode == SizingMode.Manual)
        {
            backgroundRect.sizeDelta = manualSize;
        }

        // Align the background to the center of the text
        //backgroundRect.anchorMin = textMeshPro.rectTransform.anchorMin;
        //backgroundRect.anchorMax = textMeshPro.rectTransform.anchorMax;
        //backgroundRect.anchoredPosition = textMeshPro.rectTransform.anchoredPosition;
        //backgroundRect.pivot = textMeshPro.rectTransform.pivot;
    }
}
