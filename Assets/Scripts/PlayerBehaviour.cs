using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int enemyReward;
    [SerializeField] private float shootingRange;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private Score scoreSystem;

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
            Vector3 dir = target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.transform.DOMove(target.transform.position, bulletSpeed).SetEase(Ease.InQuad).OnComplete( () => DestroyTarget(target, bullet));
        }
    }

    private void DestroyTarget(GameObject target, GameObject bullet)
    {
        scoreSystem.EnemyDead(enemyReward);
        Destroy(bullet);
        Destroy(target);
        target = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
            GetDamage();
    }

    private void GetDamage()
    {
        health--;

        if (health <= 0)
        {
            enemySpawner.canSpawn = false;
            tempWype();
            gameOverScript.Setup(RessourcesManagement.Instance.GetQuantity(RessourcesEnum.Score));
        }
        else
        {
            tempWype();
        }
    }

    private void tempWype()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
