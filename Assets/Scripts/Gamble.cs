using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamble : MonoBehaviour
{
    public Roulette roulette;
    private RessourcesEnum _randomRessource;
    private RessourcesEnum _typeChosen;
    private bool _timeToChooseQuantity = false;
    private bool _timeToChooseType = false;
    private bool _timeToSpin = false;
    private float _multiplicator = 1;
    private int _quantityGambled = 0;
    public List<int> quantityGambledList = new List<int>
    {
        100,
        200,
        500,
        1000
    };
    public Image imageBetRessource;
    public Image imageWonRessource;

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
            if (Input.GetKeyDown(KeyCode.Y))
            {
                QuantityGambled(quantityGambledList[0]);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                QuantityGambled(quantityGambledList[1]);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                QuantityGambled(quantityGambledList[2]);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                QuantityGambled(quantityGambledList[3]);
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
        if (CheckRessources()) StartWheel();
        else
        {
            Debug.Log("Pas assez de ressources");
            StartCoroutine(WaitTimeToSpin());
        }
    }

    IEnumerator WaitTimeToSpin()
    {
        yield return new WaitForSeconds(0.2f);
        TimeToGamble();
    }

    private bool CheckRessources()
    {
        if(RessourcesManagement.Instance.GetQuantity(RessourcesEnum.Credits) < quantityGambledList[0] &&
            RessourcesManagement.Instance.GetQuantity(RessourcesEnum.Score) < quantityGambledList[0] &&
            RessourcesManagement.Instance.GetQuantity(RessourcesEnum.RedMunition) < quantityGambledList[0] &&
            RessourcesManagement.Instance.GetQuantity(RessourcesEnum.BlackMunition) < quantityGambledList[0])
        {
            return false;
        }
        return true;
    }

    public void StartWheel()
    {
        int typeToBet = Random.Range(0, 4);
        _randomRessource = (RessourcesEnum)typeToBet;
        Debug.Log("Random Ressource:" + _randomRessource);

        if(RessourcesManagement.Instance.GetQuantity(_randomRessource) < quantityGambledList[0])
        {
            Debug.Log("Pas assez de ressources");
            StartCoroutine(WaitTimeToSpin());
            return;
        }

        imageBetRessource.GetComponent<ChangementSymboles>().ChangementSymbole(typeToBet);
        _timeToChooseType = true;
    }

    public void TypeChosen()
    {
        Debug.Log("Type to Receive:" + _typeChosen);
        imageWonRessource.GetComponent<ChangementSymboles>().ChangementSymbole((int)_typeChosen);
        _timeToChooseType = false;
        _timeToChooseQuantity = true;
    }

    public void QuantityGambled(int quantity)
    {
        int ressourceQuantityChosen = RessourcesManagement.Instance.GetQuantity(_randomRessource);

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
        Debug.Log("Ressource gambled:" + _randomRessource + " - Ressource received:" + _typeChosen + " - Quantity:" + _quantityGambled);
        RessourcesManagement.Instance.AddQuantity(_randomRessource, -_quantityGambled);
        _multiplicator = roulette.SpinRoulette();
    }

    public void EndSpinResult()
    {
        int quantityReceived = (int)(_quantityGambled * _multiplicator);
        Debug.Log("Quantity received:" + quantityReceived);
        RessourcesManagement.Instance.AddQuantity(_typeChosen, quantityReceived);
        imageWonRessource.GetComponent<ChangementSymboles>().DisplayImage(false);

        _quantityGambled = 0;
        TimeToGamble(); 
    }

}
