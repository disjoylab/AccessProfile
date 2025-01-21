using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HighContrastManager : MonoBehaviour
{
    public static HighContrastManager instance;
    public Sprite AccessibleImage_BLACK;
    public Sprite AccessibleImage_WHITE;
    public Sprite AccessibleImage_BLACK_GRADIENT;
    public Sprite AccessibleImage_WHITE_GRADIENT;
    public Sprite AccessibleImage_BLACK_WHITE_BORDER;
    public Sprite AccessibleImage_WHITE_BLACK_BORDER;
    private void Awake()
    {
        instance = this;
    }
    
}
