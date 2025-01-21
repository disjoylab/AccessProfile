using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

#if UNITY_EDITOR
[CustomEditor(typeof(AccessProfileManager))]
public class AccessProfileManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw the default Inspector fields

        AccessProfileManager manager = (AccessProfileManager)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Access Summary Code (ASC) Tools", EditorStyles.boldLabel);

        // Text entry box for the code
        //manager.currentAccessSummaryCode = EditorGUILayout.TextField("Current Access Summary Code", manager.currentAccessSummaryCode);

        // Button to apply the code
        if (GUILayout.Button("Update Settings From Staged Access Summary Code"))
        {
            manager.UpdateSettingsFromCode();
        }
    }
}
#endif