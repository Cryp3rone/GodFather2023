using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangementSymboles : MonoBehaviour
{
    public Image image;
    public Sprite[] spritesSymboles = new Sprite[4];
    
    public void ChangementSymbole(int i)
    {
        image.sprite = spritesSymboles[i];
        DisplayImage(true);
    }
    
    public void DisplayImage(bool display)
    {
        image.enabled = display;
    }
}
