using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float shootingRange;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private EnemySpawner enemySpawner;

    private float shootingCountDown;
    private GameObject target;

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
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (allTargets == null && RessourcesManagement.Instance.GetQuantity(RessourcesEnum.BlackMunition) <= 0 || RessourcesManagement.Instance.GetQuantity(RessourcesEnum.RedMunition) <= 0)
            return;

        foreach (GameObject tmpTarget in allTargets)
        {
            if (target == null && RessourcesManagement.Instance.GetQuantity(tmpTarget.GetComponent<EnemyBehaviour>().type) > 0)
                target = tmpTarget;

            if (target != null && target != tmpTarget && Vector2.Distance(transform.position, tmpTarget.transform.position) < Vector2.Distance(transform.position, target.transform.position))
            {
                if (RessourcesManagement.Instance.GetQuantity(tmpTarget.GetComponent<EnemyBehaviour>().type) > 0)
                    target = tmpTarget;
            }
        }
        //shoot if the closest is in the fire range
        if (target != null && Vector2.Distance(transform.position, target.transform.position) < shootingRange)
        {
            RessourcesManagement.Instance.AddQuantity(target.GetComponent<EnemyBehaviour>().type, -1);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(target.transform.position - transform.position));
            bullet.transform.DOMove(target.transform.position, 2f).SetEase(Ease.InQuad).OnComplete( () => DestroyTarget(target, bullet));
        }
    }

    private void DestroyTarget(GameObject target, GameObject bullet)
    {
        Destroy(bullet);
        Destroy(target);
        target = null;
    }
}
