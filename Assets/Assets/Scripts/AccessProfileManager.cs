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

    // Base36 encoding utility functions (0-9, A-Z)
    private const string BASE36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Event triggered when accessibility settings change
    public static event Action<AccessProfileSettings> OnAccessProfileChanged;

    [Tooltip("Current Access Summary Code that summarizes the current settings")]
    public string currentAccessSummaryCode;

    // Enums for accessibility settings
    public enum CaptionTextSize { Small, Medium, Large }
    public enum CaptionFont { SansSerif, Serif }
    public enum CaptionBackground { None, SemiOpaque, Opaque }
    public enum HighContrastModeType { Off, On }    

    [Tooltip("The Access Summary Code that is staged. Settings will update to match this code when you click the button below")]
    public string stagedAccessSummaryCode;

    // Accessibility settings class
    [Serializable]
    public class AccessProfileSettings
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
   public AccessProfileSettings currentAccessProfileSettings = new AccessProfileSettings();

    private void OnEnable()
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

    public AccessProfileSettings GetAccessSettings()
    {
        return currentAccessProfileSettings;
    }


    public void UpdateAccessProfileSettings(AccessProfileSettings newSettings)
    {
        currentAccessProfileSettings = newSettings;
        UpdateCodeFromSettings();
        NotifyAccessibilitySettingsChanged();
    }

    private void NotifyAccessibilitySettingsChanged()
    {
        OnAccessProfileChanged?.Invoke(currentAccessProfileSettings);
    }

    public string EncodeSettings()
    {
        //Encode settings into 2-digit compressed preference code
        int value = 0;

        value |= (currentAccessProfileSettings.captionsEnabled ? 1 : 0) << 6;
        value |= ((int)currentAccessProfileSettings.captionTextSize & 0b11) << 4;
        value |= ((int)currentAccessProfileSettings.captionFont & 0b1) << 3;
        value |= ((int)currentAccessProfileSettings.captionBackground & 0b11) << 1;
        value |= (currentAccessProfileSettings.highContrastModeType == HighContrastModeType.On ? 1 : 0);

        // Convert to Base36 (0-9, A-Z)
        return Base36Encode(value);
    }

    public void DecodeSettings(string code)
    {
        if (code.Length != 2)
        {
            Debug.LogError("Invalid code. It must be exactly 2 characters long.");
            return;
        }

        try
        {
            int value = Base36Decode(code);
            AccessProfileSettings tempSettings = new AccessProfileSettings();

            tempSettings.captionsEnabled = (value & (1 << 6)) != 0;
            tempSettings.captionTextSize = (CaptionTextSize)((value >> 4) & 0b11);
            tempSettings.captionFont = (CaptionFont)((value >> 3) & 0b1);
            tempSettings.captionBackground = (CaptionBackground)((value >> 1) & 0b11);
            tempSettings.highContrastModeType = (HighContrastModeType)(value & 0b1);

            UpdateAccessProfileSettings(tempSettings);
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
        NotifyAccessibilitySettingsChanged();
    }

    private void OnDestroy()
    {
        // Clean up the singleton instance if this object is destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private string Base36Encode(int value)
    {
        char first = BASE36[value / 36];
        char second = BASE36[value % 36];
        return $"{first}{second}";
    }

    private int Base36Decode(string encoded)
    {
        return (BASE36.IndexOf(encoded[0]) * 36) + BASE36.IndexOf(encoded[1]);
    }

}

