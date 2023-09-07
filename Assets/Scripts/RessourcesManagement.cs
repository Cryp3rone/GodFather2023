using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RessourcesEnum
{
    Credits = 0,
    BlackMunition,
    RedMunition,
    Score
}

public class RessourcesManagement : MonoBehaviour
{
    public static RessourcesManagement Instance { get; private set; }
    public Score Score;
    public Credits Credits;
    public Munition Munition;

    [Header("Start values")]
    public int StartMunitions;
    public int StartCredits;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Score.TotalScore = 0;
        Credits.TotalCredits = StartCredits;
        Munition.BlackMunition = StartMunitions;
        Munition.RedMunition = StartMunitions;

        DisplayAllRessources();
    }

    public void AddQuantity(RessourcesEnum ressource, int quantity)
    {
        switch (ressource)
        {
            case RessourcesEnum.BlackMunition:
                Munition.AddMunition(RessourcesEnum.BlackMunition, quantity);
                break;
            case RessourcesEnum.RedMunition:
                Munition.AddMunition(RessourcesEnum.RedMunition, quantity);
                break;
            case RessourcesEnum.Score:
                Score.AddScore(quantity);
                break;
            case RessourcesEnum.Credits:
                Credits.AddCredits(quantity);
                break;
        }
    }

    public int GetQuantity(RessourcesEnum ressource)
    {
        
        switch (ressource)
        {
            case RessourcesEnum.BlackMunition:
                return Munition.BlackMunition;
            case RessourcesEnum.RedMunition:
                return Munition.RedMunition;
            case RessourcesEnum.Score:
                return Score.TotalScore;
            case RessourcesEnum.Credits:
                return Credits.TotalCredits;
            default:
                return 0;
        }
    }

    public void DisplayAllRessources()
    {
        Credits.DisplayCredits();
        Score.DisplayScore();
        Munition.DisplayMunition();
    }

    public string GetDisplayNumber(string number)
    {
        int length = 5 - number.Length;
        string displayedNumber = number;
        for (int i = 0; i < length; i++)
        {
            displayedNumber = "0" + displayedNumber;
        }
        return displayedNumber;
    }

}
