using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble : MonoBehaviour
{
    private RessourcesEnum wheelResult;
    private bool _timeToChooseQuantity = false;
    private bool _timeToChooseType = false;
    private RessourcesEnum typeChosen;

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
    public void TimeToGamble(bool value)
    {
        ReceiveWheelResult(Random.Range(0, 4));
    }

    public void ReceiveWheelResult(int wheelNumber)
    {
        wheelResult = (RessourcesEnum)wheelNumber;
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

        Debug.Log("Ressource gambled:"+ wheelResult + " - Quantity:"+ quantity);
        RessourcesManagement.Instance.AddQuantity(wheelResult, -quantity);

        StartCoroutine(AnimWheel(quantity));
        resetWheelResult();
    }

    private IEnumerator AnimWheel(int quantityGambled)
    {
        float multiplicator = 1.5f; //placeholder
        int quantityReceived = (int)(quantityGambled * multiplicator);
        yield return new WaitForSeconds(3f);
        Debug.Log("Quantity received:" + quantityReceived);
        RessourcesManagement.Instance.AddQuantity(typeChosen, quantityReceived);
    }

    private void resetWheelResult()
    {
        _timeToChooseQuantity = false;
    }
}
