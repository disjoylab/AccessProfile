using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AccessProfileManager;

[ExecuteInEditMode]
public class HighContrastManager : MonoBehaviour
{
    public static HighContrastManager instance;
    public static event Action<bool> OnHighContrastAccessSettingChanged;
    [HideInInspector]
    public bool _hcActive; // Backing field for hcActive

    public bool hcActive 
    {
        get => _hcActive;
        private set
        {
            if (_hcActive != value)
            {
                _hcActive = value; // Use the backing field here
                hcActive = value;
                // Notify all subscribers
                OnHighContrastAccessSettingChanged?.Invoke(hcActive);
            }
        }
    }


    private void OnEnable()
    {
        AccessProfileManager.OnAccessibilitySettingsChanged += OnHighContrastModeChanged; //Subscribe to Accessibility Settings Being Changed
    }
    private void OnDisable()
    {
        AccessProfileManager.OnAccessibilitySettingsChanged -= OnHighContrastModeChanged; //Unsubscribe to Accessibility Settings Being Changed
    }
    private void Awake()
    {
        instance = this;
        //Check to see if instance of manager exists, if not, create it
        if (AccessProfileManager.Instance == null)
        {
            AccessProfileManager.Instance = GameObject.FindObjectOfType<AccessProfileManager>();
        }

        //Set high contrast mode status based on access profile
        if (AccessProfileManager.Instance.currentAccessProfileSettings.highContrastModeType != AccessProfileManager.HighContrastModeType.Off)
        {
            hcActive = true;
        }
        else
        {
            hcActive = false;
        }
    }

    private void OnHighContrastModeChanged(AccessProfileManager.AccessibilitySettings newSettings)
    {

        // Update HCActive based on the high contrast mode type
        hcActive = (newSettings.highContrastModeType != HighContrastModeType.Off);
    }

}
