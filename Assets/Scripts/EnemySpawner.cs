using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRange;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDifference;
    [SerializeField] private GameObject ennemyRedSpawn;
    [SerializeField] private GameObject ennemyBlackSpawn;
    [SerializeField] private Transform target;
    
    private float spawnRateCountDown;
    
    public bool canSpawn = true;
    public List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnRateCountDown = spawnRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSpawn)
        {
            if (spawnRateCountDown >= 0)
            {
                spawnRateCountDown -= Time.fixedDeltaTime;
            }
            else
            {
                SpawnEnnemies();
                spawnRateCountDown = spawnRate;
            }
        }
    }

    private void SpawnEnnemies()
    {
        Vector3 spawnPos = Random.insideUnitCircle.normalized * spawnRange;
        bool randType = Random.Range(0, 2) == 1;

        GameObject spawnedEnemy = Instantiate(randType ? ennemyBlackSpawn : ennemyRedSpawn, spawnPos, Quaternion.identity);
        enemyList.Add(spawnedEnemy);

        EnemyBehaviour enemy = spawnedEnemy.GetComponent<EnemyBehaviour>();
        enemy.SetType(randType ? RessourcesEnum.BlackMunition : RessourcesEnum.RedMunition);
        enemy.SetTarget(target.position);
    }
}
