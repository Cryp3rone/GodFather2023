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
    
    private static EnemySpawner instance = null;
    private float spawnRateCountDown; 
    public static EnemySpawner Instance => instance;
    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

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
        enemyList.Add(spawnedEnemy);

        EnemyBehaviour enemy = spawnedEnemy.GetComponent<EnemyBehaviour>();

        bool randType = Random.Range(0, 2) == 1;
        enemy.SetType(randType ? RessourcesEnum.BlackMunition : RessourcesEnum.RedMunition);
        enemy.SetTarget(target.position);
    }
}
