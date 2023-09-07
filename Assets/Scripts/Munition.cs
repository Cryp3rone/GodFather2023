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

    private void Start()
    {
        DisplayMunition();
    }

    public void AddMunition(RessourcesEnum munition, int ammoToAdd)
    {
        if(munition == RessourcesEnum.BlackMunition)
        {
            BlackMunition += ammoToAdd;
            BlackMunition = Mathf.Min(BlackMunition, 99999);
        }

        if (munition == RessourcesEnum.RedMunition)
        {
            RedMunition += ammoToAdd;
            RedMunition = Mathf.Min(RedMunition, 99999);
        }
        DisplayMunition();
    }

    public void DisplayMunition()
    {
        TextMunitionBlack.text = RessourcesManagement.Instance.GetDisplayNumber(BlackMunition.ToString());
        TextMunitionRed.text = RessourcesManagement.Instance.GetDisplayNumber(RedMunition.ToString());

    }
}
