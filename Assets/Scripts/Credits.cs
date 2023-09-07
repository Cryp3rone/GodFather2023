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

    public void AddCredits(int creditsToAdd)
    {
        TotalCredits += creditsToAdd;
        TotalCredits = Mathf.Min(TotalCredits, 99999);
        DisplayCredits();
    }

    public void DisplayCredits()
    {
        ScoreCredits.text = RessourcesManagement.Instance.GetDisplayNumber(TotalCredits.ToString());
    }
}
