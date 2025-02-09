using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HighContrastManager : MonoBehaviour
{
    public static HighContrastManager instance;
    public static event Action<bool> OnHighContrastSettingChanged;
    public static bool hcActive;


    private void OnEnable()
    {
        LocalAccessManager.OnLocalAccessSettingsChanged += OnHighContrastModeChanged;
    }
    private void OnDisable()
    {
        LocalAccessManager.OnLocalAccessSettingsChanged += OnHighContrastModeChanged;
    }
    private void Awake()
    {
        instance = this;
        //Check to see if instance of manager exists, if not, create it
        if (LocalAccessManager.Instance == null)
        {
            LocalAccessManager.Instance = GameObject.FindObjectOfType<LocalAccessManager>();
        }

        //Set high contrast mode status based on access profile
        //NOTE: In this simple example, the highContrastModeType is either on or off with no granual settings, but this could easily be expanded here
        if (LocalAccessManager.Instance.currentLocalAccessSettings.highContrastModeType != false)
        {
            hcActive = true;
        }
        else
        {
            hcActive = false;
        }
    }

    private void OnHighContrastModeChanged(LocalAccessManager.LocalAccessibilitySettings newSettings)
    {
        // Update HCActive based on the high contrast mode type
        hcActive = newSettings.highContrastModeType;
        OnHighContrastSettingChanged?.Invoke(hcActive);
    }

}
