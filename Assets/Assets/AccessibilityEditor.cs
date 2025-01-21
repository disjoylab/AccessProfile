using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Accessibility))]
public class NewBehaviourScript : Editor
{
    Accessibility accessibility;
    static bool HighContrastEditMode;

#if UNITY_EDITOR
    void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }
        accessibility = (Accessibility)target; 
        accessibility.Display(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
        CheckObjectsHaveBeenCaptured();
    }

    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
        {
            return;
        }
        CheckObjectsHaveBeenCaptured();
        bool repaint = false;
       

        //IMAGE
        if (accessibility.image != null)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Original Image");
            accessibility.OriginalSprite =
           (Sprite)EditorGUILayout.ObjectField(accessibility.OriginalSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            
                accessibility.OriginalSpriteColor = EditorGUILayout.ColorField("Original Color: ", accessibility.OriginalSpriteColor);
               
            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Accessable Image");
            accessibility.AccessibleSprite =
           (Sprite)EditorGUILayout.ObjectField(accessibility.AccessibleSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
             
                accessibility.AccessibleSpriteColor = EditorGUILayout.ColorField("Accessible Color: ", accessibility.AccessibleSpriteColor);
              
            

            GUILayout.Space(10);


            EditorGUILayout.BeginHorizontal();
            if (AccessibityManager.instance != null)
            {
                if (GUILayout.Button("ORIG [BLACK]", GUILayout.Width(150), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.black;
                    accessibility.AccessibleSprite = accessibility.OriginalSprite;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_BLACK;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_WHITE;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_BLACK_GRADIENT;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_WHITE_GRADIENT;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_BLACK_WHITE_BORDER;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleSprite = AccessibityManager.instance.AccessibleImage_WHITE_BLACK_BORDER;
                    repaint = true;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        //RAW IMAGE
        if ( accessibility.rawImage != null)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Original Texture");
            accessibility.OrigninalTexture =
           (Texture)EditorGUILayout.ObjectField(accessibility.OrigninalTexture, typeof(Texture), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            accessibility.OriginalSpriteColor = EditorGUILayout.ColorField("Original Color: ", accessibility.OriginalSpriteColor);

            GUILayout.Space(25);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Accessable Texture");
            accessibility.AccessibleTexture =
           (Texture)EditorGUILayout.ObjectField(accessibility.AccessibleTexture, typeof(Texture), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            accessibility.AccessibleSpriteColor = EditorGUILayout.ColorField("Accessible Color: ", accessibility.AccessibleSpriteColor);



            GUILayout.Space(10);


            EditorGUILayout.BeginHorizontal();
            if (AccessibityManager.instance != null)
            {
                if (GUILayout.Button("ORIG [BLACK]", GUILayout.Width(150), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.black;
                    accessibility.AccessibleTexture = accessibility.OrigninalTexture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_BLACK.texture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_WHITE.texture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_BLACK_GRADIENT.texture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_WHITE_GRADIENT.texture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture;
                    repaint = true;
                }
                if (GUILayout.Button(AccessibityManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    accessibility.AccessibleSpriteColor = Color.white;
                    accessibility.AccessibleTexture = AccessibityManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture;
                    repaint = true;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        //TEXT
        if (accessibility.myText != null)
        {
            EditorGUILayout.LabelField("Text");
            GUILayout.Space(5);
            accessibility.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", accessibility.OriginalTextColor);
            GUILayout.Space(5);
            accessibility.OriginalFont =
           (Font)EditorGUILayout.ObjectField(accessibility.OriginalFont, typeof(Font), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            accessibility.OriginalTextSize = EditorGUILayout.IntField("Original Size: ", accessibility.OriginalTextSize);

            GUILayout.Space(15);
            accessibility.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", accessibility.AccessibleTextColor);
            GUILayout.Space(5);
            accessibility.AccessibleFont =
           (Font)EditorGUILayout.ObjectField(accessibility.AccessibleFont, typeof(Font), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            GUILayout.Space(5);
            accessibility.AccessibleTextSize = EditorGUILayout.IntField("Accessible Size: ", accessibility.AccessibleTextSize);
        }
         if (accessibility.myTMP != null)
        {
            EditorGUILayout.LabelField("TextMeshPro");
            GUILayout.Space(5);
            accessibility.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", accessibility.OriginalTextColor);
            GUILayout.Space(5);
            accessibility.OriginalTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(accessibility.OriginalTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            accessibility.OriginalTMPSize = EditorGUILayout.FloatField("Original Size: ", accessibility.OriginalTMPSize);

            GUILayout.Space(15);
            accessibility.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", accessibility.AccessibleTextColor);
            GUILayout.Space(5);
            accessibility.AccessibleTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(accessibility.AccessibleTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            accessibility.AccessibleTMPSize = EditorGUILayout.FloatField("Accessible Size: ", accessibility.AccessibleTMPSize);
        }
        //TMPUGUI
         if (accessibility.myTMPuGui != null)
        {
            EditorGUILayout.LabelField("TextMeshProUGUI");
            GUILayout.Space(5);
            accessibility.OriginalTextColor = EditorGUILayout.ColorField("Original Color: ", accessibility.OriginalTextColor);
            GUILayout.Space(5);
            accessibility.OriginalTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(accessibility.OriginalTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            accessibility.OriginalTMPSize = EditorGUILayout.FloatField("Original Size: ", accessibility.OriginalTMPSize);

            GUILayout.Space(15);
            accessibility.AccessibleTextColor = EditorGUILayout.ColorField("Accessible Color: ", accessibility.AccessibleTextColor);
            GUILayout.Space(5);
            accessibility.AccessibleTMPFont =
            (TMP_FontAsset)EditorGUILayout.ObjectField(accessibility.AccessibleTMPFont, typeof(TMP_FontAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            accessibility.AccessibleTMPSize = EditorGUILayout.FloatField("Accessible Size: ", accessibility.AccessibleTMPSize);
        }


        GUILayout.Space(20);
        EditorGUILayout.LabelField("VIEW");
        GUILayout.Space(5);
        if (GUILayout.Button((HighContrastEditMode ? "ACCESSIBLE" : "ORIGINAL")))
        {
            HighContrastEditMode = !HighContrastEditMode;
            SettingsManager.HighContrastModeChanged?.Invoke(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
            repaint = true;
        }

        GUILayout.Space(20);
        accessibility.Display(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
        GUILayout.Space(10);
        if (GUILayout.Button("REPAINT"))
        {
            repaint = true;
        }

        if (repaint)
        {
            EditorUtility.SetDirty(accessibility.gameObject);
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();
        }
    }

    private void CheckObjectsHaveBeenCaptured()
    {
        if (!accessibility.ObjectsCaptured)
        {
            Debug.Log("Capturing Objects");
            accessibility.CaptureObjects();
            accessibility.CaptureOriginals();
            accessibility.SetAccessbileDefaults();
        }
    }
#endif
}
