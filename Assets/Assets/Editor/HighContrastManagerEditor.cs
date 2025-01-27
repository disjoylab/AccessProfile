using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HighContrastManager))]
public class HighContrastManagerEditor : Editor
{
    HighContrastManager hcManager;

#if UNITY_EDITOR

    void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }
        hcManager = (HighContrastManager)target;

        //Check to see if instance of manager exists, if not, create it
        if (AccessProfileManager.Instance == null)
        {
            AccessProfileManager.Instance = GameObject.FindObjectOfType<AccessProfileManager>();
        }
    }

    public override void OnInspectorGUI()
    {
        bool localHCActive;

        if (Application.isPlaying)
        {
            return;
        }

        if (AccessProfileManager.Instance.currentAccessProfileSettings.highContrastModeType != AccessProfileManager.HighContrastModeType.Off)
        {
            localHCActive = true;
        }
        else
        {
            localHCActive = false;
        }

        GUILayout.Space(20);
        GUILayout.Toggle(localHCActive, "Is High Contrast Mode Active?");
    }

#endif
}


