using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways] // Ensures updates in the Editor without play mode
public class LocalAccessManager : MonoBehaviour
{
    // Singleton instance for global access
    public static LocalAccessManager Instance { get; set; }

    // Event triggered when accessibility settings change
    public static event Action<LocalAccessibilitySettings> OnLocalAccessSettingsChanged;

    public enum LocalCaptionFont {OpenSans, Bodoni} //Names of the actual fonts you'd like to use

    // Accessibility settings class
    [Serializable]
    public class LocalAccessibilitySettings
    {
        [Tooltip("Enable or disable captions.")]
        public bool captionsEnabled = true;

        [Tooltip("Set the caption text size.")]
        public float captionTextSize = 40f;

        [Tooltip("Set the caption font.")]
        public LocalCaptionFont captionFont = LocalCaptionFont.OpenSans;

        [Tooltip("Set the caption background style.")]
        public float captionBackground = 0.5f;

        [Tooltip("Set the high contrast mode type.")]
        public bool highContrastModeType = false;
    }

    // Current settings
    [Tooltip("Current local accessibility settings.")]
   public LocalAccessibilitySettings currentLocalAccessSettings = new LocalAccessibilitySettings();

    private void OnEnable()
    {
        AccessProfileManager.OnAccessProfileChanged += UpdateLocalAccessFromAccessProfile;
        // Enforce singleton pattern
        if (Application.isPlaying)
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional, persists between scenes
        }

        if (AccessProfileManager.Instance == null)
        {
            AccessProfileManager.Instance = GameObject.FindObjectOfType<AccessProfileManager>();
        }

        //UpdateLocalAccessFromAccessProfile(AccessProfileManager.Instance.currentAccessProfileSettings); 
    }

    private void NotifyLocalAccessSettingsChanged()
    {
        OnLocalAccessSettingsChanged?.Invoke(currentLocalAccessSettings);
    }

    public void UpdateLocalAccessFromAccessProfile(AccessProfileManager.AccessProfileSettings newSettings)
    {
        //Change the font size depending on the AccessProfile Setting
        switch (newSettings.captionTextSize)
        {
            case AccessProfileManager.CaptionTextSize.Small:
                currentLocalAccessSettings.captionTextSize = 20;
                break;
            case AccessProfileManager.CaptionTextSize.Medium:
                currentLocalAccessSettings.captionTextSize = 40;
                break;
            case AccessProfileManager.CaptionTextSize.Large:
                currentLocalAccessSettings.captionTextSize = 60;
                break;
            default:
                Debug.LogWarning($"Unknown caption text size. Using default size.");
                currentLocalAccessSettings.captionTextSize = 40; // Default to "Medium" size
                break;
        }

        //Change the alpha value of the background depending on the AccessProfile Setting
        switch (newSettings.captionBackground)
        {
            case AccessProfileManager.CaptionBackground.Opaque:
                currentLocalAccessSettings.captionBackground = 1f;
                break;
            case AccessProfileManager.CaptionBackground.SemiOpaque:
                currentLocalAccessSettings.captionBackground = 0.5f;
                break;
            case AccessProfileManager.CaptionBackground.None:
                currentLocalAccessSettings.captionBackground = 0f;
                break;
            default:
                Debug.LogWarning($"Unknown caption background. Using semi-opaque.");
                currentLocalAccessSettings.captionBackground = 0.5f; // Default to SemiOpaque
                break;
        }

        //Change the font depending on the AccessProfile Setting
        switch (newSettings.captionFont)
        {
            case AccessProfileManager.CaptionFont.SansSerif:
                currentLocalAccessSettings.captionFont = LocalCaptionFont.OpenSans;
                break;
            case AccessProfileManager.CaptionFont.Serif:
                currentLocalAccessSettings.captionFont = LocalCaptionFont.Bodoni;
                break;
            default:
                Debug.LogWarning($"Unknown caption font. Using Sans Serif.");
                currentLocalAccessSettings.captionFont = LocalCaptionFont.OpenSans;
                break;
        }

        currentLocalAccessSettings.captionsEnabled = newSettings.captionsEnabled;
        currentLocalAccessSettings.highContrastModeType = (newSettings.highContrastModeType == AccessProfileManager.HighContrastModeType.On ? true : false);

        NotifyLocalAccessSettingsChanged();
    }

    // Called automatically when a value in the Inspector changes
    private void OnValidate()
    {
        NotifyLocalAccessSettingsChanged();
    }

    private void OnDestroy()
    {
        // Clean up the singleton instance if this object is destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }

}

