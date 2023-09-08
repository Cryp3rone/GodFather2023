using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Roulette : MonoBehaviour
{
    
    [SerializeField] private int _MaxNbTurn;
    [SerializeField] private float _rotationTime;
    private float _rotationTimeCounter;
    private float _targetAngle = 0;
    private bool _isSpinning;
    //[HideInInspector]
    public float result;
    public AnimationCurve animationCurve;
    public List<Vector2> chances; //(multiplicateur, proba)
    public ParticleSystem particleSystemJackpot;

    public UnityEvent TriggerRoulette;

    //Sound
    [SerializeField] private AudioSource roulette;
    [SerializeField] private AudioSource coinDrop;
    [SerializeField] private AudioSource spotlight;
    [SerializeField] private AudioSource cheers;
    [SerializeField] private AudioSource error;

    public float SpinRoulette(){
        _isSpinning = true;
        int n  = WeightedSpin();
        //int n = Random.Range(0,chances.Count); //en cas d'equiprobabilité

        _targetAngle = 360*n/chances.Count + _MaxNbTurn*360;
        result = chances[n].x; 

        StartCoroutine(SpinAnimation(_targetAngle));
        StartCoroutine(PlaySounds());

        return result;
    }

    IEnumerator SpinAnimation(float maxAngle){
        roulette.Play();
        float startAngle = transform.eulerAngles.z;
        maxAngle -= startAngle;
        _rotationTimeCounter = 0;
        while (_rotationTimeCounter < _rotationTime){
            float angle = maxAngle * animationCurve.Evaluate(_rotationTimeCounter/_rotationTime);
            _rotationTimeCounter += Time.deltaTime;
            transform.eulerAngles = new Vector3(0f,0f,angle+startAngle);
            yield return 0;
        }
        TriggerRoulette?.Invoke();
        _isSpinning = false;
        transform.eulerAngles = new Vector3(0f,0f,maxAngle+startAngle);
        roulette.Stop();
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)&&!_isSpinning){
        //    SpinRoulette();
        //}   
    }

    private int WeightedSpin(){
        float r = Random.Range(0f,1f);
        for (int i = 0; i < chances.Count; i++){
            if (r-chances[i].y <= 0){
                return i;
            }
            else{
                r -= chances[i].y;
            }
        }
        return 0;
    }

    IEnumerator PlaySounds(){
        yield return new WaitForSeconds(_rotationTime);
        if(result!=0){
            coinDrop.Play();
            spotlight.Play();
        }
        else{
            error.Play();
        }

        if(result == 7)
        {
            cheers.Play();
            particleSystemJackpot.Play();
        }

    }
}
