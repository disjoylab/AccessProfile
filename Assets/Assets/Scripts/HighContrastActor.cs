using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class HighContrastActor : MonoBehaviour
{
    //NEED EDITOR BACKING, SETS CURRENT SIZE AND SUCH AND ASSIGNES A DEFAULT TO THE ACCESIBLE SIZES AND SUCH ON ENABLE
    //HAVE A TEST BUTTON THAT TESTS ALL HIGH CONTRASTS IN EDIT MODE
    //NEEDS RECT TRANSFORM

    public Image image;
    public SpriteRenderer mySpriteRenderer;

    public Sprite OriginalSprite;
    public Sprite AccessibleSprite;

    public RawImage rawImage;
    public Texture OrigninalTexture;
    public Texture AccessibleTexture;

    public Color OriginalSpriteColor = Color.white;
    public Color AccessibleSpriteColor = Color.white;

    public Text myText;
    public TextMeshPro myTMP;
    public TextMeshProUGUI myTMPuGui;


    public Font OriginalFont;
    public Font AccessibleFont;
    public int OriginalTextSize;
    public int AccessibleTextSize;
    public Color OriginalTextColor;
    public Color AccessibleTextColor = Color.white;

    public TMP_FontAsset OriginalTMPFont;
    public TMP_FontAsset AccessibleTMPFont;
    public float OriginalTMPSize;
    public float AccessibleTMPSize;
    public bool OriginalTMP_ColorGradient;

    [HideInInspector]
    public bool ObjectsCaptured;

    private void OnEnable()
    {
        HighContrastManager.OnHighContrastSettingChanged += OnHighContrastModeChanged; //Subscribe to Accessibility Settings Being Changed
    }
    private void OnDisable()
    {
        HighContrastManager.OnHighContrastSettingChanged -= OnHighContrastModeChanged; //Subscribe to Accessibility Settings Being Changed
    }
    private void Start()
    {           
        Display();
    }

    public void CaptureObjects()
    {
        if (!ObjectsCaptured)
        {
            ObjectsCaptured = true;
            rawImage = gameObject.GetComponent<RawImage>();
            image = gameObject.GetComponent<Image>();
            myText = gameObject.GetComponent<Text>();
            myTMP = gameObject.GetComponent<TextMeshPro>();
            myTMPuGui = gameObject.GetComponent<TextMeshProUGUI>();
            mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }        
    }

    public void CaptureOriginals()
    {
        if (image != null)
        {
            OriginalSprite = image.sprite;
            OriginalSpriteColor = image.color;           
        }
        if (myText != null)
        {
            OriginalTextColor = myText.color;
            OriginalTextSize = myText.fontSize;
            OriginalFont = myText.font;
        }
        if (myTMP != null)
        {
            OriginalTMPFont = myTMP.font;
            OriginalTMPSize = myTMP.fontSize;
            OriginalTextColor = myTMP.color;
            
        }
        if (myTMPuGui != null)
        {
            OriginalTMPFont = myTMPuGui.font;
            OriginalTMPSize = myTMPuGui.fontSize;
            OriginalTextColor = myTMPuGui.color;
            OriginalTMP_ColorGradient = myTMPuGui.enableVertexGradient;
        }
        if (rawImage !=null)
        {
            OrigninalTexture = rawImage.texture;
        }
        if (mySpriteRenderer != null)
        {
            OriginalSprite = mySpriteRenderer.sprite;
            OriginalSpriteColor = mySpriteRenderer.color;
        }
    }
    public void SetAccessbileDefaults()
    {
        if (image != null)
        {
            AccessibleSprite = image.sprite;
        }
        if (myText != null)
        {
            AccessibleTextSize = myText.fontSize;
            AccessibleFont = myText.font;
        }
        if (myTMP != null)
        {
            AccessibleTMPFont = myTMP.font;
            AccessibleTMPSize = myTMP.fontSize; 
        }
        if (myTMPuGui != null)
        {
            AccessibleTMPFont = myTMPuGui.font;
            AccessibleTMPSize = myTMPuGui.fontSize; 
        }
        if (rawImage != null)
        {
            AccessibleTexture = rawImage.texture;
        }
        if (mySpriteRenderer != null)
        {
            AccessibleSprite = mySpriteRenderer.sprite;
        }
    }
    private void OnHighContrastModeChanged(bool isActive)
    {
        Display();
    }

    public void Display()
    {
        bool HC = HighContrastManager.hcActive;

        if (myText != null)
        {
            myText.color = HC ? AccessibleTextColor : OriginalTextColor;
            myText.fontSize = HC ? AccessibleTextSize : OriginalTextSize;
            myText.font = HC ? AccessibleFont : OriginalFont;
        }
         if (myTMP != null)
        {
            myTMP.font = HC ? AccessibleTMPFont : OriginalTMPFont;
            myTMP.fontSize = HC ? AccessibleTMPSize : OriginalTMPSize;
            myTMP.color = HC ? AccessibleTextColor : OriginalTextColor;
        }
         if (myTMPuGui != null)
        {
            myTMPuGui.font = HC ? AccessibleTMPFont : OriginalTMPFont;
            myTMPuGui.fontSize = HC ? AccessibleTMPSize : OriginalTMPSize;
            myTMPuGui.color = HC ? AccessibleTextColor : OriginalTextColor;
            myTMPuGui.enableVertexGradient = HC ? false : OriginalTMP_ColorGradient; 
        }
         if (image != null)
        {
            var SpriteToSet = HC ? AccessibleSprite : OriginalSprite;            
            if (SpriteToSet != image.sprite && SpriteToSet!=null)
            {
                image.sprite = SpriteToSet;
                bool HasBorder = SpriteToSet.border.magnitude > 0;
                image.type = HasBorder? Image.Type.Sliced:Image.Type.Simple;
            }
            image.color = HC ? AccessibleSpriteColor : OriginalSpriteColor;           
        }
        if (rawImage!=null)
        {
            var textureToSet = HC ? AccessibleTexture : OrigninalTexture;
            if (textureToSet != rawImage.texture && textureToSet != null)
            {
                rawImage.texture = textureToSet;
               // bool HasBorder = SpriteToSet.border.magnitude > 0;
             //   image.type = HasBorder ? Image.Type.Sliced : Image.Type.Simple;
            }
            rawImage.color = HC ? AccessibleSpriteColor : OriginalSpriteColor;
        }
        if (mySpriteRenderer != null)
        {
            var SpriteToSet = HC ? AccessibleSprite : OriginalSprite;
            if (SpriteToSet != mySpriteRenderer.sprite && SpriteToSet != null)
            {
                mySpriteRenderer.sprite = SpriteToSet;
            }
            mySpriteRenderer.color = HC ? AccessibleSpriteColor : OriginalSpriteColor;
        }
    }
}
