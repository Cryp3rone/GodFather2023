using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using System.Resources;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float shootingRange;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private GameObject bullet;

    private float shootingCountDown;
    // Start is called before the first frame update
    void Start()
    {
        shootingCountDown = shootingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingCountDown >= 0)
        {
            shootingCountDown -= Time.deltaTime;
        }
        else
        {
            Shoot();
            shootingCountDown = shootingSpeed;
        }
    }

    private void Shoot()
    {
        List<GameObject> enemyList = EnemySpawner.Instance.enemyList;
        if(RessourcesManagement.Instance.GetQuantity(RessourcesEnum.BlackMunition) <= 0 && RessourcesManagement.Instance.GetQuantity(RessourcesEnum.RedMunition) <= 0)
        for(int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetComponent<EnemyBehaviour>().type == RessourcesEnum.BlackMunition && RessourcesManagement.Instance.GetQuantity(RessourcesEnum.BlackMunition) > 0)
            {
                RessourcesManagement.Instance.AddQuantity(RessourcesEnum.BlackMunition, -1);
                Destroy(enemyList[i]);
                break;
            }
            else if (enemyList[i].GetComponent<EnemyBehaviour>().type == RessourcesEnum.RedMunition && RessourcesManagement.Instance.GetQuantity(RessourcesEnum.RedMunition) > 0)
            {
                RessourcesManagement.Instance.AddQuantity(RessourcesEnum.RedMunition, -1);
                Destroy(enemyList[i]);
                break;
            }
        }
    }
}
