using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Runtime.CompilerServices;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI ScoreText;
    public int TotalScore = 1000;
    
    void Start()
    {
        DisplayScore();
    }


    public void AddScore(int numberToAdd)
    {
        TotalScore += numberToAdd;
        TotalScore = Mathf.Min(TotalScore, 99999);
        DisplayScore();
    }

    public void MultiplicateScore(float multiplicator)
    {
        TotalScore = (int)(TotalScore * multiplicator);
        DisplayScore();
    }

    public void EnemyDead(int point)
    {
        AddScore(point);
    }

    public void DisplayScore()
    {
        ScoreText.text = RessourcesManagement.Instance.GetDisplayNumber(TotalScore.ToString());
    }

}
