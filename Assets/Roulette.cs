using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    
    [SerializeField] private int _MaxNbTurn;
    [SerializeField] private float _rotationTime;
    private float _rotationTimeCounter;
    private float _targetAngle = 0;
    private float _weight;
    //[HideInInspector]
    public float result;
    public AnimationCurve animationCurve;
    public List<Vector2> chances; //(multiplicateur, proba)
    public int bet;

    public void SpinRoulette(){
        int n  = WeightedSpin();
        //int n = Random.Range(0,chances.Count); //en cas d'equiprobabilité

        _targetAngle = 360*n/chances.Count + _MaxNbTurn*360;
        result = Mathf.Floor(bet * chances[n].x);

        StartCoroutine(SpinAnimation(_targetAngle));
    }

    IEnumerator SpinAnimation(float maxAngle){
        float startAngle = transform.eulerAngles.z;
        maxAngle -= startAngle;
        _rotationTimeCounter = 0;
        while (_rotationTimeCounter < _rotationTime){
            float angle = maxAngle * animationCurve.Evaluate(_rotationTimeCounter/_rotationTime);
            _rotationTimeCounter += Time.deltaTime;
            transform.eulerAngles = new Vector3(0f,0f,angle+startAngle);
            yield return 0;
        }
        transform.eulerAngles = new Vector3(0f,0f,maxAngle+startAngle);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            SpinRoulette();
        }   
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

}
