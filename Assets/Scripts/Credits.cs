using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI ScoreCredits;
    public int TotalCredits;

    //animation score
    [SerializeField] private float _animTime;
    private float _animTimeCounter;
    private int _lastCredits;
    [SerializeField] private AnimationCurve animationCurve;

    //Popup
    public TextMeshProUGUI CreditPopup;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCredits();
    }

    public void AddCredits(int creditsToAdd)
    {
        _lastCredits = TotalCredits;
        TotalCredits += creditsToAdd;
        TotalCredits = Mathf.Min(TotalCredits, 99999);
        if(creditsToAdd>0){
            CreditPopup.text = "+" + creditsToAdd.ToString();
        }
        else{
            CreditPopup.text = creditsToAdd.ToString();
        }
        CreditPopup.enabled = true;
        DisplayCredits();
    }

    public void DisplayCredits()
    {
        StartCoroutine(AnimateScore());
        //ScoreCredits.text = RessourcesManagement.Instance.GetDisplayNumber(TotalCredits.ToString());
    }

    
    IEnumerator AnimateScore(){
        _animTimeCounter = 0;
        int startScore = _lastCredits;
        TotalCredits -= startScore;
        while(_animTimeCounter < _animTime){
            int score = Mathf.FloorToInt(TotalCredits*animationCurve.Evaluate(_animTimeCounter/_animTime));
            _animTimeCounter += Time.deltaTime;
            ScoreCredits.text = RessourcesManagement.Instance.GetDisplayNumber((score+startScore).ToString());
            yield return 0;
        }
        ScoreCredits.text = RessourcesManagement.Instance.GetDisplayNumber((TotalCredits+startScore).ToString());
        TotalCredits += startScore;
        CreditPopup.enabled = false;
    }
}
