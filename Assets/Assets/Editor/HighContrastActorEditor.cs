using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(HighContrastActor))]
public class NewBehaviourScript : Editor
{
    HighContrastActor hcActor;
    bool HighContrastActive;

#if UNITY_EDITOR
    void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }
        hcActor = (HighContrastActor)target;
        hcActor.Display();
        CheckObjectsHaveBeenCaptured();
    }

    public override void OnInspectorGUI()
    {
        if (HighContrastManager.instance == null)
        {
            HighContrastManager.instance = GameObject.FindObjectOfType<HighContrastManager>();
        }

        if (Application.isPlaying)
        {
            return;
        }
        CheckObjectsHaveBeenCaptured();
        bool repaint = false;
       

        //IMAGE
        if (hcActor.image != null)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Original Image");
            hcActor.OriginalSprite =
           (Sprite)EditorGUILayout.ObjectField(hcActor.OriginalSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            hcActor.OriginalSpriteColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalSpriteColor);
               
            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Accessable Image");
            hcActor.AccessibleSprite =
           (Sprite)EditorGUILayout.ObjectField(hcActor.AccessibleSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
             
            hcActor.AccessibleSpriteColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleSpriteColor);
              
           
        }
        //RAW IMAGE
        if ( hcActor.rawImage != null)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Original Texture");
            hcActor.OrigninalTexture =
           (Texture)EditorGUILayout.ObjectField(hcActor.OrigninalTexture, typeof(Texture), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            hcActor.OriginalSpriteColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalSpriteColor);

            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Accessable Texture");
            hcActor.AccessibleTexture =
           (Texture)EditorGUILayout.ObjectField(hcActor.AccessibleTexture, typeof(Texture), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            hcActor.AccessibleSpriteColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleSpriteColor);

        }

        //TEXT
        if (hcActor.myText != null)
        {
            EditorGUILayout.LabelField("Text");
            GUILayout.Space(5);
            hcActor.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalTextColor);
            GUILayout.Space(5);
            hcActor.OriginalFont =
           (Font)EditorGUILayout.ObjectField(hcActor.OriginalFont, typeof(Font), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            hcActor.OriginalTextSize = EditorGUILayout.IntField("Original Size: ", hcActor.OriginalTextSize);

            GUILayout.Space(15);
            hcActor.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleTextColor);
            GUILayout.Space(5);
            hcActor.AccessibleFont =
           (Font)EditorGUILayout.ObjectField(hcActor.AccessibleFont, typeof(Font), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            GUILayout.Space(5);
            hcActor.AccessibleTextSize = EditorGUILayout.IntField("Accessible Size: ", hcActor.AccessibleTextSize);
        }
         if (hcActor.myTMP != null)
        {
            EditorGUILayout.LabelField("TextMeshPro");
            GUILayout.Space(5);
            hcActor.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalTextColor);
            GUILayout.Space(5);
            hcActor.OriginalTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(hcActor.OriginalTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            hcActor.OriginalTMPSize = EditorGUILayout.FloatField("Original Size: ", hcActor.OriginalTMPSize);

            GUILayout.Space(15);
            hcActor.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleTextColor);
            GUILayout.Space(5);
            hcActor.AccessibleTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(hcActor.AccessibleTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            hcActor.AccessibleTMPSize = EditorGUILayout.FloatField("Accessible Size: ", hcActor.AccessibleTMPSize);
        }
        //TMPUGUI
         if (hcActor.myTMPuGui != null)
        {
            EditorGUILayout.LabelField("TextMeshProUGUI");
            GUILayout.Space(5);
            hcActor.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalTextColor);
            GUILayout.Space(5);
            hcActor.OriginalTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(hcActor.OriginalTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            hcActor.OriginalTMPSize = EditorGUILayout.FloatField("Original Size: ", hcActor.OriginalTMPSize);

            GUILayout.Space(15);
            hcActor.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleTextColor);
            GUILayout.Space(5);
            hcActor.AccessibleTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(hcActor.AccessibleTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            hcActor.AccessibleTMPSize = EditorGUILayout.FloatField("Accessible Size: ", hcActor.AccessibleTMPSize);
        }

        //Sprite Renderer
        if (hcActor.mySpriteRenderer != null)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Original Image");
            hcActor.OriginalSprite =
           (Sprite)EditorGUILayout.ObjectField(hcActor.OriginalSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            hcActor.OriginalSpriteColor = EditorGUILayout.ColorField("Original Color: ", hcActor.OriginalSpriteColor);

            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Accessable Image");
            hcActor.AccessibleSprite =
           (Sprite)EditorGUILayout.ObjectField(hcActor.AccessibleSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            hcActor.AccessibleSpriteColor = EditorGUILayout.ColorField("Accessible Color: ", hcActor.AccessibleSpriteColor);
        }

        GUILayout.Space(20);
        GUILayout.Toggle(HighContrastManager.hcActive,"Is High Contrast Mode Active?");

        // //Code to control High contrast mode from Actor
        //GUILayout.Space(20);
        //EditorGUILayout.LabelField("VIEW");
        //GUILayout.Space(5);
        //if (GUILayout.Button((HighContrastEditMode ? "ACCESSIBLE" : "ORIGINAL")))
        //{
        //   HighContrastEditMode = !HighContrastEditMode;
        //AccessProfileManager.NotifyAccessibilitySettingsChanged?.Invoke();
        //SettingsManager.HighContrastModeChanged?.Invoke(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
        //   repaint = true;
        //}

        //GUILayout.Space(20);
        //accessibility.Display(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
        //GUILayout.Space(10);


        if (GUILayout.Button("REPAINT"))
        {
            repaint = true;
        }

        if (repaint)
        {
            EditorUtility.SetDirty(hcActor.gameObject);
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();
        }

        
    }

    private void CheckObjectsHaveBeenCaptured()
    {
        if (!hcActor.ObjectsCaptured)
        {
            hcActor.CaptureObjects();
            hcActor.CaptureOriginals();
            hcActor.SetAccessbileDefaults();
        }
    }
#endif
}
