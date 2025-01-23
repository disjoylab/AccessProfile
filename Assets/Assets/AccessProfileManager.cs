using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways] // Ensures updates in the Editor without play mode
public class AccessProfileManager : MonoBehaviour
{
    // Singleton instance for global access
    public static AccessProfileManager Instance { get; set; }

    // Event triggered when accessibility settings change
    public static event Action<AccessibilitySettings> OnAccessibilitySettingsChanged;

    [Tooltip("Current Access Summary Code that summarizes the current settings")]
    public string currentAccessSummaryCode;

    // Enums for accessibility settings
    public enum CaptionTextSize { Small, Medium, Large }
    public enum CaptionFont { SansSerif, Serif }
    public enum CaptionBackground { None, SemiOpaque, Opaque }
    public enum HighContrastModeType { Off, DarkMode, LightMode }    

    [Tooltip("The Access Summary Code that is staged. Settings will update to match this code when you click the button below")]
    public string stagedAccessSummaryCode;

    // Accessibility settings class
    [Serializable]
    public class AccessibilitySettings
    {
        [Tooltip("Enable or disable captions.")]
        public bool captionsEnabled = true;

        [Tooltip("Set the caption text size.")]
        public CaptionTextSize captionTextSize = CaptionTextSize.Medium;

        [Tooltip("Set the caption font.")]
        public CaptionFont captionFont = CaptionFont.SansSerif;

        [Tooltip("Set the caption background style.")]
        public CaptionBackground captionBackground = CaptionBackground.SemiOpaque;

        [Tooltip("Set the high contrast mode type.")]
        public HighContrastModeType highContrastModeType = HighContrastModeType.Off;
    }

    // Current settings
    [Tooltip("Current accessibility settings.")]
   public AccessibilitySettings currentAccessProfileSettings = new AccessibilitySettings();

    private void Awake()
    {
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

        // Initialize static settings with the Inspector-provided default settings
        currentAccessSummaryCode = EncodeSettings();
    }

    public AccessibilitySettings GetAccessSettings()
    {
        return currentAccessProfileSettings;
    }


    public void UpdateAccessSettings(AccessibilitySettings newSettings)
    {
        currentAccessProfileSettings = newSettings;
        UpdateCodeFromSettings();
        NotifyAccessibilitySettingsChanged();
    }

    private void NotifyAccessibilitySettingsChanged()
    {
        OnAccessibilitySettingsChanged?.Invoke(currentAccessProfileSettings);
    }

    public void ToggleCaptions(bool enabled)
    {
        currentAccessProfileSettings.captionsEnabled = enabled;
        NotifyAccessibilitySettingsChanged();
    }

    public void SetHighContrastModeType(HighContrastModeType modeType)
    {
        currentAccessProfileSettings.highContrastModeType = modeType;
        NotifyAccessibilitySettingsChanged();
    }

    public string EncodeSettings()
    {
        // Encodes the current accessibility settings into a 5-digit alphanumeric code.
        char captionsFlag = currentAccessProfileSettings.captionsEnabled ? '1' : '0';
        char textSize = (char)('A' + (int)currentAccessProfileSettings.captionTextSize);
        char font = (char)('X' + (int)currentAccessProfileSettings.captionFont);
        char background = (char)('M' + (int)currentAccessProfileSettings.captionBackground);
        char contrastMode = (char)('D' + (int)currentAccessProfileSettings.highContrastModeType);

        return $"{captionsFlag}{textSize}{font}{background}{contrastMode}";
    }

    public void DecodeSettings(string code)
    {
        AccessibilitySettings tempSettings = new AccessibilitySettings();

        // Decodes a 5-digit alphanumeric code and updates the current accessibility settings.
        if (code.Length != 5)
        {
            Debug.LogError("Invalid code. It must be exactly 5 characters long.");
            return;
        }

        try
        {
            tempSettings.captionsEnabled = code[0] == '1';
            tempSettings.captionTextSize = (CaptionTextSize)(code[1] - 'A');
            tempSettings.captionFont = (CaptionFont)(code[2] - 'X');
            tempSettings.captionBackground = (CaptionBackground)(code[3] - 'M');
            tempSettings.highContrastModeType = (HighContrastModeType)(code[4] - 'D');

            UpdateAccessSettings(tempSettings);

        }
        catch (Exception ex)
        {
            Debug.LogError($"Error decoding settings: {ex.Message}");
        }
    }

    public void UpdateSettingsFromCode()
    {
        if (string.IsNullOrEmpty(stagedAccessSummaryCode))
        {
            Debug.LogWarning("Access Summary Code is empty. Please enter a valid code.");
            return;
        }

        DecodeSettings(stagedAccessSummaryCode);
        currentAccessSummaryCode = stagedAccessSummaryCode;
        Debug.Log($"Updated settings from code: {stagedAccessSummaryCode}");
        stagedAccessSummaryCode = null;        
    }

    public void UpdateCodeFromSettings()
    {
        currentAccessSummaryCode = EncodeSettings();
    }

    // Called automatically when a value in the Inspector changes
    private void OnValidate()
    {
        UpdateCodeFromSettings();
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

