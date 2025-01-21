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
    static bool HighContrastEditMode;

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
              
            

            GUILayout.Space(10);


            EditorGUILayout.BeginHorizontal();
            if (HighContrastManager.instance != null)
            {
                if (GUILayout.Button("ORIG [BLACK]", GUILayout.Width(150), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.black;
                    hcActor.AccessibleSprite = hcActor.OriginalSprite;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_BLACK;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_WHITE;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_BLACK_GRADIENT;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_WHITE_GRADIENT;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_BLACK_WHITE_BORDER;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleSprite = HighContrastManager.instance.AccessibleImage_WHITE_BLACK_BORDER;
                    repaint = true;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
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



            GUILayout.Space(10);


            EditorGUILayout.BeginHorizontal();
            if (HighContrastManager.instance != null)
            {
                if (GUILayout.Button("ORIG [BLACK]", GUILayout.Width(150), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.black;
                    hcActor.AccessibleTexture = hcActor.OrigninalTexture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_BLACK.texture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_WHITE.texture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_BLACK_GRADIENT.texture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE_GRADIENT.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_WHITE_GRADIENT.texture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_BLACK_WHITE_BORDER.texture;
                    repaint = true;
                }
                if (GUILayout.Button(HighContrastManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture, GUILayout.Width(40), GUILayout.Height(40)))
                {
                    hcActor.AccessibleSpriteColor = Color.white;
                    hcActor.AccessibleTexture = HighContrastManager.instance.AccessibleImage_WHITE_BLACK_BORDER.texture;
                    repaint = true;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
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


        GUILayout.Space(20);
        EditorGUILayout.LabelField("VIEW");
        GUILayout.Space(5);
        if (GUILayout.Button((HighContrastEditMode ? "ACCESSIBLE" : "ORIGINAL")))
        {
            HighContrastEditMode = !HighContrastEditMode;
            //AccessProfileManager.NotifyAccessibilitySettingsChanged?.Invoke();
            //SettingsManager.HighContrastModeChanged?.Invoke(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
            repaint = true;
        }

        GUILayout.Space(20);
        //accessibility.Display(HighContrastEditMode ? HighContrastSettings.ON : HighContrastSettings.OFF);
        GUILayout.Space(10);
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
            Debug.Log("Capturing Objects");
            hcActor.CaptureObjects();
            hcActor.CaptureOriginals();
            hcActor.SetAccessbileDefaults();
        }
    }
#endif
}
