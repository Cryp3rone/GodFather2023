using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Munition : MonoBehaviour
{
    public TextMeshProUGUI TextMunitionBlack;
    public TextMeshProUGUI TextMunitionRed;
    public int BlackMunition = 0;
    public int RedMunition = 0;

    //animation score
    [SerializeField] private float _animTimeBlack;
    private float _animTimeCounterBlack;
    private int _lastScoreBlack;
    [SerializeField] private float _animTimeRed;
    private float _animTimeCounterRed;
    private int _lastScoreRed;
    [SerializeField] private AnimationCurve animationCurve;

    //popup
    public TextMeshProUGUI MunitionBlackPopup;
    public TextMeshProUGUI MunitionRedPopup;


    private void Start()
    {
        DisplayMunition();
    }

    public void AddMunition(RessourcesEnum munition, int ammoToAdd)
    {
        if(munition == RessourcesEnum.BlackMunition)
        {
            _lastScoreBlack = BlackMunition;
            BlackMunition += ammoToAdd;
            BlackMunition = Mathf.Min(BlackMunition, 99999);
            if(ammoToAdd>0){
                MunitionBlackPopup.text = "+" + ammoToAdd.ToString();
            }
            else{
                MunitionBlackPopup.text = ammoToAdd.ToString();
            }
            MunitionBlackPopup.enabled = true;
            StartCoroutine(AnimateBlack());
        }

        if (munition == RessourcesEnum.RedMunition)
        {
            _lastScoreRed = RedMunition;
            RedMunition += ammoToAdd;
            RedMunition = Mathf.Min(RedMunition, 99999);
            if(ammoToAdd>0){
                MunitionRedPopup.text = "+" + ammoToAdd.ToString();
            }
            else{
                MunitionRedPopup.text = ammoToAdd.ToString();
            }
            MunitionRedPopup.enabled = true;
            StartCoroutine(AnimateRed());
        }
    }

    public void DisplayMunition()
    {
        StartCoroutine(AnimateBlack());
        StartCoroutine(AnimateRed());
        //TextMunitionBlack.text = RessourcesManagement.Instance.GetDisplayNumber(BlackMunition.ToString());
        //TextMunitionRed.text = RessourcesManagement.Instance.GetDisplayNumber(RedMunition.ToString());

    }

    IEnumerator AnimateBlack(){
        _animTimeCounterBlack = 0;
        int startScore = _lastScoreBlack;
        int blackMunToDisplay = BlackMunition;
        blackMunToDisplay -= startScore;
        while(_animTimeCounterBlack < _animTimeBlack){
            int score = Mathf.FloorToInt(blackMunToDisplay * animationCurve.Evaluate(_animTimeCounterBlack/_animTimeBlack));
            _animTimeCounterBlack += Time.deltaTime;
            TextMunitionBlack.text = RessourcesManagement.Instance.GetDisplayNumber((score+startScore).ToString());
            yield return 0;
        }
        TextMunitionBlack.text = RessourcesManagement.Instance.GetDisplayNumber((blackMunToDisplay +startScore).ToString());
        blackMunToDisplay += startScore;
        MunitionBlackPopup.enabled = false;
    }

    IEnumerator AnimateRed(){
        _animTimeCounterRed = 0;
        int startScore = _lastScoreRed;
        int redMunToDisplay = RedMunition;
        redMunToDisplay -= startScore;
        while(_animTimeCounterRed < _animTimeRed){
            int score = Mathf.FloorToInt(redMunToDisplay * animationCurve.Evaluate(_animTimeCounterRed/_animTimeRed));
            _animTimeCounterRed += Time.deltaTime;
            TextMunitionRed.text = RessourcesManagement.Instance.GetDisplayNumber((score+startScore).ToString());
            yield return 0;
        }
        TextMunitionRed.text = RessourcesManagement.Instance.GetDisplayNumber((redMunToDisplay + startScore).ToString());
        redMunToDisplay += startScore;
        MunitionRedPopup.enabled = false;
    }
}
