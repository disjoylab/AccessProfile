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
    public TMP_FontAsset fontBodono;
    public TMP_FontAsset fontOpenSans;

    [Serializable]
    public class CaptionSettings
    {
        [Tooltip("Enable or disable captions.")]
        public bool captionsEnabled = true;

        [Tooltip("Set the exact text size.")]
        public float captionTextSize = 40;

        [Tooltip("Set the caption font.")]
        public TMP_FontAsset captionFont; // Default to null; initialize later.

        [Tooltip("Set the exact alpha value of the caption background.")]
        public float captionBackground = 0.5f;
    }

    public CaptionSettings currentCaptionSettings = new CaptionSettings();

    private void OnEnable()
    {
        LocalAccessManager.OnLocalAccessSettingsChanged += SetCaptionManagerSettings; //Subscribe to Accessibility Settings Being Changed

        instance = this;
        //Check to see if instance of manager exists, if not, create it
        if (LocalAccessManager.Instance == null)
        {
            LocalAccessManager.Instance = GameObject.FindObjectOfType<LocalAccessManager>();
        }

        //Set the default font here:
        if (currentCaptionSettings.captionFont == null)
        {
            currentCaptionSettings.captionFont = fontOpenSans; // Assign the default font
        }

        //Match the manager settings with the Local Access Manager's Settings
        SetCaptionManagerSettings(LocalAccessManager.Instance.currentLocalAccessSettings);
    }
   
    private void SetCaptionManagerSettings(LocalAccessManager.LocalAccessibilitySettings newSettings)
    {
        currentCaptionSettings.captionsEnabled = newSettings.captionsEnabled;
        currentCaptionSettings.captionTextSize = newSettings.captionTextSize;
        currentCaptionSettings.captionBackground = newSettings.captionBackground;

        //Change the font depending on the AccessProfile Setting
        switch (newSettings.captionFont)
        {
            case LocalAccessManager.LocalCaptionFont.OpenSans:
                currentCaptionSettings.captionFont = fontOpenSans;
                break;
            case LocalAccessManager.LocalCaptionFont.Bodoni:
                currentCaptionSettings.captionFont = fontBodono;
                break;
            default:
                Debug.LogWarning($"Unknown caption font. Using Sans Serif.");
                currentCaptionSettings.captionFont = fontOpenSans;
                break;
        }

        //Broadcast changes
        OnCaptionSettingsChanged?.Invoke(currentCaptionSettings);
    }

    private void OnDisable()
    {
        LocalAccessManager.OnLocalAccessSettingsChanged -= SetCaptionManagerSettings; //Unsubscribe to Accessibility Settings Being Changed
    }
}
