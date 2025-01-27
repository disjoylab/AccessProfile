using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AccessProfileManager;

[ExecuteInEditMode]

public class CaptionManager : MonoBehaviour
{
    public static CaptionManager instance;
    public static event Action<CaptionSettings> OnCaptionSettingsChanged;
    public TMP_FontAsset serif;
    public TMP_FontAsset sansSerif;

    [Serializable]
    public class CaptionSettings
    {
        [Tooltip("Enable or disable captions.")]
        public bool captionsEnabled = true;

        [Tooltip("Set the exact text size.")]
        public float captionTextSize = 12;

        [Tooltip("Set the caption font.")]
        public TMP_FontAsset captionFont; // Default to null; initialize later.

        [Tooltip("Set the exact alpha value of the caption background.")]
        public float captionBackground = 0.5f;
    }

    public CaptionSettings currentCaptionSettings = new CaptionSettings();

    private void OnEnable()
    {
        AccessProfileManager.OnAccessibilitySettingsChanged += SetManagerSeetings; //Subscribe to Accessibility Settings Being Changed

        instance = this;
        //Check to see if instance of manager exists, if not, create it
        if (AccessProfileManager.Instance == null)
        {
            AccessProfileManager.Instance = GameObject.FindObjectOfType<AccessProfileManager>();
        }

        //Set the default font here:
        if (currentCaptionSettings.captionFont == null)
        {
            currentCaptionSettings.captionFont = sansSerif; // Assign the default font
        }

        //Match the manager settings with the Access Profile Manager
        SetManagerSeetings(AccessProfileManager.Instance.currentAccessProfileSettings);
    }
    private void OnDisable()
    {
        AccessProfileManager.OnAccessibilitySettingsChanged -= SetManagerSeetings; //Unsubscribe to Accessibility Settings Being Changed
    }
    private void SetManagerSeetings(AccessProfileManager.AccessibilitySettings newSettings)
    {
        currentCaptionSettings.captionsEnabled = newSettings.captionsEnabled;

        //Change the font size depending on the AccessProfile Setting
        switch (newSettings.captionTextSize)
        {
            case AccessProfileManager.CaptionTextSize.Small:
                currentCaptionSettings.captionTextSize = 20;
                break;
            case AccessProfileManager.CaptionTextSize.Medium:
                currentCaptionSettings.captionTextSize = 40;
                break;
            case AccessProfileManager.CaptionTextSize.Large:
                currentCaptionSettings.captionTextSize = 60;
                break;
            default:
                Debug.LogWarning($"Unknown caption text size. Using default size.");
                currentCaptionSettings.captionTextSize = 40; // Default to "Medium" size
                break;
        }

        //Change the alpha value of the background depending on the AccessProfile Setting
        switch (newSettings.captionBackground)
        {
            case AccessProfileManager.CaptionBackground.Opaque:
                currentCaptionSettings.captionBackground = 1f;
                break;
            case AccessProfileManager.CaptionBackground.SemiOpaque:
                currentCaptionSettings.captionBackground = 0.5f;
                break;
            case AccessProfileManager.CaptionBackground.None:
                currentCaptionSettings.captionBackground = 0f;
                break;
            default:
                Debug.LogWarning($"Unknown caption background. Using semi-opaque.");
                currentCaptionSettings.captionBackground = 0.5f; // Default to SemiOpaque
                break;
        }

        //Change the font depending on the AccessProfile Setting
        switch (newSettings.captionFont)
        {
            case AccessProfileManager.CaptionFont.SansSerif:
                currentCaptionSettings.captionFont = sansSerif;
                break;
            case AccessProfileManager.CaptionFont.Serif:
                currentCaptionSettings.captionFont = serif;
                break;
            default:
                Debug.LogWarning($"Unknown caption font. Using Sans Serif.");
                currentCaptionSettings.captionFont = sansSerif;
                break;
        }

        //Broadcast changes
        OnCaptionSettingsChanged?.Invoke(currentCaptionSettings);
    }
}
