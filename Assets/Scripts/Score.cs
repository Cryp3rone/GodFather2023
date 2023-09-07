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

    //animation score
    [SerializeField] private float _animTime;
    private float _animTimeCounter;
    private int _lastScore;
    [SerializeField] private AnimationCurve animationCurve;
    
    void Start()
    {
        DisplayScore();
    }


    public void AddScore(int numberToAdd)
    {
        _lastScore = TotalScore;
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
        StartCoroutine(AnimateScore());
        //ScoreText.text = RessourcesManagement.Instance.GetDisplayNumber(TotalScore.ToString());
    }

    IEnumerator AnimateScore(){
        _animTimeCounter = 0;
        int startScore = _lastScore;
        TotalScore -= startScore;
        while(_animTimeCounter < _animTime){
            int score = Mathf.FloorToInt(TotalScore*animationCurve.Evaluate(_animTimeCounter/_animTime));
            _animTimeCounter += Time.deltaTime;
            ScoreText.text = RessourcesManagement.Instance.GetDisplayNumber((score+startScore).ToString());
            yield return 0;
        }
        ScoreText.text = RessourcesManagement.Instance.GetDisplayNumber((TotalScore+startScore).ToString());
        TotalScore += startScore;
    }

}
