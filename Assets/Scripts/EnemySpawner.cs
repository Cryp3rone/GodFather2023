using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDifference;
    [SerializeField] private GameObject ennemyToSpawn;
    [SerializeField] private Transform target;
    
    private float spawnRateCountDown; 

    // Start is called before the first frame update
    void Start()
    {
        spawnRateCountDown = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRateCountDown >= 0)
        {
            spawnRateCountDown -= Time.deltaTime;
        }
        else
        {
            SpawnEnnemies();
            spawnRateCountDown = spawnRate;
        }
    }

    private void SpawnEnnemies()
    {
        Vector3 spawnPos = Random.insideUnitCircle.normalized * spawnRange;
        GameObject spawnedEnemy = Instantiate(ennemyToSpawn, spawnPos, Quaternion.identity);

        EnemyBehaviour enemy = spawnedEnemy.GetComponent<EnemyBehaviour>();

        bool randType = Random.Range(0, 2) == 1;
        enemy.SetType(randType ? RessourcesEnum.BlackMunition : RessourcesEnum.RedMunition);
        enemy.SetTarget(target.position);
    }
}
