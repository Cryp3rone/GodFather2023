using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ColorMunition
{
    BlackMunition = 0,
    RedMunition
}

public class Munition : MonoBehaviour
{
    public TextMeshProUGUI TextMunitionBlack;
    public TextMeshProUGUI TextMunitionRed;
    public int BlackMunition = 0;
    public int RedMunition = 0;

    private void Start()
    {
        DisplayMunition();
    }

    public void AddMunition(ColorMunition munition, int ammoToAdd)
    {
        if(munition == ColorMunition.BlackMunition)
        {
            BlackMunition += ammoToAdd;
        }

        if (munition == ColorMunition.RedMunition)
        {
            RedMunition += ammoToAdd;
        }
        DisplayMunition();
    }

    public void DisplayMunition()
    {
        TextMunitionBlack.text = BlackMunition.ToString();
        TextMunitionRed.text = RedMunition.ToString();

    }
}
