using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI ScoreCredits;
    public int TotalCredits;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCredits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCredits(int creditsToAdd)
    {
        TotalCredits += creditsToAdd;
        DisplayCredits();
    }

    public void DisplayCredits()
    {
        ScoreCredits.text = TotalCredits.ToString();
        /*int numberDisplayed = int.Parse(ScoreCredits.text);
        int numberToChange = numberDisplayed - TotalCredits;

        for (int i = 0; i < numberToChange; i++)
        {
            ScoreCredits.text= numberDisplayed--.ToString();
            numberDisplayed--; 
        }*/

    }
}
