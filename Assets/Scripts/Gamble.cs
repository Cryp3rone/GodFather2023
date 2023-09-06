using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble : MonoBehaviour
{
    private RessourcesEnum wheelResult;
    private bool _timeToChooseQuantity = false;
    private bool _timeToChooseType = false;
    private RessourcesEnum typeChosen;
    public Roulette roulette;
    private float _multiplicator = 1;
    private int _quantityGambled;

    // Update is called once per frame
    void Update()
    {

        if (_timeToChooseType)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                typeChosen = RessourcesEnum.Credits;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                typeChosen = RessourcesEnum.Score;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                typeChosen = RessourcesEnum.RedMunition;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                typeChosen = RessourcesEnum.BlackMunition;
                TypeChosen();
            }
        }

        if (_timeToChooseQuantity)
        {
            int quantityGambled = 0;
            // Wait Inputs
            if (Input.GetKeyDown(KeyCode.G))
            {
                quantityGambled = 100;
                _timeToChooseQuantity = false;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                quantityGambled = 200;
                _timeToChooseQuantity = false;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                quantityGambled = 300;
                _timeToChooseQuantity = false;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                quantityGambled = 400;
                _timeToChooseQuantity = false;
                QuantityGambled(quantityGambled);
            }

        }
    }


    //simulation
    public void TimeToGamble()
    {
        StartWheel();
    }

    public void StartWheel()
    {
        int typeToBet = Random.Range(0, 4);
        wheelResult = (RessourcesEnum)typeToBet;
        Debug.Log("WheelResult:" + wheelResult);
        _timeToChooseType = true;
    }

    public void TypeChosen()
    {
        Debug.Log("Type to Receive:" + typeChosen);
        _timeToChooseType = false;
        _timeToChooseQuantity = true;
    }

    public void QuantityGambled(int quantity)
    {
        int ressourceQuantityChosen = RessourcesManagement.Instance.GetQuantity(wheelResult);


        if(ressourceQuantityChosen < quantity)
        {
            Debug.Log("Ressources insuffisantes");
            _timeToChooseQuantity = true;
            return;
        }
        _quantityGambled = quantity;

        Debug.Log("Ressource gambled:"+ wheelResult + " - Quantity:"+ quantity);
        RessourcesManagement.Instance.AddQuantity(wheelResult, -quantity);
        _multiplicator = roulette.SpinRoulette();
        resetWheelResult();
    }


    public void ReceiveWheelResult()
    {

    }

    public void EndSpinResult()
    {
        int quantityReceived = (int)(_quantityGambled * _multiplicator);
        Debug.Log("Quantity received:" + quantityReceived);
        RessourcesManagement.Instance.AddQuantity(typeChosen, quantityReceived);
    }

    private void resetWheelResult()
    {
        _timeToChooseQuantity = false;
    }
}
