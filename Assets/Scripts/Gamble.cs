using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamble : MonoBehaviour
{
    public Roulette roulette;
    private RessourcesEnum _randomRessource;
    private RessourcesEnum _typeChosen;
    private bool _hasChosenQuantity = false;
    private bool _hasChosenType = false;
    private bool _timeToGamble = false;
    private bool _timeToSpin = false;
    private float _multiplicator = 1;
    private int _quantityGambled = 0;

    [Header("Bet")]
    public Image imageBetRessource;
    public Image imageBetText;
    public Sprite[] spritesBetRessource = new Sprite[2];

    [Header("Won")]
    public Image imageWonRessource;
    public Image imageWonText;
    public Sprite[] spritesWonRessource = new Sprite[2];

    [Header("Quantity")]
    public List<int> quantityGambledList = new List<int>
    {
        100,
        200,
        500,
        1000
    };
    public Image[] imagesQuantity = new Image[4];
    public Sprite[] spritesQuantity100 = new Sprite[2];
    public Sprite[] spritesQuantity200 = new Sprite[2];
    public Sprite[] spritesQuantity500 = new Sprite[2];
    public Sprite[] spritesQuantity1000 = new Sprite[2];

    [Header("Sound")]
    [SerializeField] private AudioSource bell;
    [SerializeField] private AudioSource beeps;
    [SerializeField] private AudioSource cashIn;

    private void Start()
    {
        TimeToGamble();
    }

    // Update is called once per frame
    void Update()
    {

        if (!_timeToGamble) return;
        
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
        

            if (Input.GetKeyDown(KeyCode.Y))
            {
                imagesQuantity[0].sprite = spritesQuantity100[1];
                imagesQuantity[0].SetNativeSize();
                QuantityGambled(quantityGambledList[0]);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                imagesQuantity[1].sprite = spritesQuantity200[1];
                imagesQuantity[1].SetNativeSize();
                QuantityGambled(quantityGambledList[1]);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                imagesQuantity[2].sprite = spritesQuantity500[1];
                imagesQuantity[2].SetNativeSize();
                QuantityGambled(quantityGambledList[2]);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                imagesQuantity[3].sprite = spritesQuantity1000[1];
                imagesQuantity[3].SetNativeSize();
                QuantityGambled(quantityGambledList[3]);
            }
        

        if (!_hasChosenQuantity && !_hasChosenType) return;

        if (_timeToSpin)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.G))
            {
                StartSpin();
                _timeToSpin = false;
            }
        }
    }

    private void ResetSprites()
    {
        imagesQuantity[0].sprite = spritesQuantity100[0];
        imagesQuantity[0].SetNativeSize();

        imagesQuantity[1].sprite = spritesQuantity200[0];
        imagesQuantity[1].SetNativeSize();

        imagesQuantity[2].sprite = spritesQuantity500[0];
        imagesQuantity[2].SetNativeSize();

        imagesQuantity[3].sprite = spritesQuantity1000[0];
        imagesQuantity[3].SetNativeSize();
        
        imageBetText.sprite = spritesBetRessource[0];
        imageBetText.SetNativeSize();

        imageWonText.sprite = spritesWonRessource[0];
        imageWonText.SetNativeSize();
    }

    //simulation
    public void TimeToGamble()
    {
        if (CheckRessources()){
            bell.Play();
            StartWheel();
            _timeToGamble = true;
        }
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
        imageBetText.sprite = spritesBetRessource[1];
        imageBetText.SetNativeSize();
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
    }

    public void TypeChosen()
    {
        imageWonText.sprite = spritesWonRessource[1];
        imageWonText.SetNativeSize();
        Debug.Log("Type to Receive:" + _typeChosen);
        beeps.Play();
        imageWonRessource.GetComponent<ChangementSymboles>().ChangementSymbole((int)_typeChosen);
        _hasChosenType = true;
    }

    public void QuantityGambled(int quantity)
    {
        int ressourceQuantityChosen = RessourcesManagement.Instance.GetQuantity(_randomRessource);

        if(ressourceQuantityChosen < quantity)
        {
            Debug.Log("Ressources insuffisantes");
            _hasChosenQuantity = false;
            switch(quantity)
            {
                case 100:
                    imagesQuantity[0].sprite = spritesQuantity100[0];
                    imagesQuantity[0].SetNativeSize();
                    break;
                    case 200:
                    imagesQuantity[1].sprite = spritesQuantity200[0];
                    imagesQuantity[1].SetNativeSize();
                    break;
                    case 500:
                    imagesQuantity[2].sprite = spritesQuantity500[0];
                    imagesQuantity[2].SetNativeSize();
                    break;
                    case 1000:
                    imagesQuantity[3].sprite = spritesQuantity1000[0];
                    imagesQuantity[3].SetNativeSize();
                    break;
            }

            return;
        }
        Debug.Log("Quantity chosen: " + quantity);
        _quantityGambled = quantity;
        _hasChosenQuantity = true;
        _timeToSpin = true;
        cashIn.Play();
    }

    private void StartSpin()
    {
        _timeToGamble = false;
        _hasChosenQuantity = false;
        _hasChosenType = false;

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
        ResetSprites();
        _quantityGambled = 0;
        TimeToGamble(); 
    }

}
