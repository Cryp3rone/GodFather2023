using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble : MonoBehaviour
{
    private RessourcesEnum wheelResult;
    private bool _timeToChooseQuantity = false;
    private bool _timeToChooseType = false;
    private bool _timeToSpin = false;
    private RessourcesEnum _typeChosen;
    public Roulette roulette;
    private float _multiplicator = 1;
    private int _quantityGambled;

    private void Start()
    {
        TimeToGamble();
    }

    // Update is called once per frame
    void Update()
    {

        if (_timeToChooseType)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _typeChosen = RessourcesEnum.Credits;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _typeChosen = RessourcesEnum.Score;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _typeChosen = RessourcesEnum.RedMunition;
                TypeChosen();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _typeChosen = RessourcesEnum.BlackMunition;
                TypeChosen();
            }
        }

        if (_timeToChooseQuantity)
        {
            int quantityGambled = 0;
            // Wait Inputs
            if (Input.GetKeyDown(KeyCode.Y))
            {
                quantityGambled = 100;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                quantityGambled = 200;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                quantityGambled = 300;
                QuantityGambled(quantityGambled);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                quantityGambled = 400;
                QuantityGambled(quantityGambled);
            }

        }

        if (_timeToSpin)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.G))
            {
                StartSpin();
                _timeToSpin = false;
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
        Debug.Log("Type to Receive:" + _typeChosen);
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
        Debug.Log("Quantity chosen: " + quantity);
        _quantityGambled = quantity;
        _timeToChooseQuantity = false;
        _timeToSpin = true;
    }

    private void StartSpin()
    {
        Debug.Log("Ressource gambled:" + wheelResult + "Ressource received" + _typeChosen + " - Quantity:" + _quantityGambled);
        RessourcesManagement.Instance.AddQuantity(wheelResult, -_quantityGambled);
        _multiplicator = roulette.SpinRoulette();
    }

    public void EndSpinResult()
    {
        int quantityReceived = (int)(_quantityGambled * _multiplicator);
        Debug.Log("Quantity received:" + quantityReceived);
        RessourcesManagement.Instance.AddQuantity(_typeChosen, quantityReceived);
        _quantityGambled = 0;
        TimeToGamble(); 
    }

}
