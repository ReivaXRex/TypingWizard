using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies To Spawn")]
    [SerializeField] private List<GameObject> enemyToSpawnList = new List<GameObject>();

    [Header("Current Enemies Spawned")]
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();


    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> occupiedSpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> availableSpawnPoints = new List<Transform>();

    //[SerializeField] private Collider[] spawnPointColliders;

    [Header("Spawn Rate")]
    [SerializeField] private float spawnRate = 2.0f;

    [Header("Spawn Limit")]
    [SerializeField] private int spawnLimit = 5;

    [SerializeField]
    private int currentSpawnCount = 0;

    private int spawnIndex = 0;

    public GameObject lastSpawnedEnemy;
    public GameObject lastSpawnedEnemyHealthText;

    public delegate void OnEnemySpawned();
    public static event OnEnemySpawned OnEnemySpawnedEvent; 

    private static SpawnManager _instance;


    public static SpawnManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {       
        StartCoroutine(StartSpawningEnemies());
    }

    private IEnumerator StartSpawningEnemies()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentSpawnCount < spawnLimit)
        {
            SpawnRandomEnemy();

            currentSpawnCount++;
            spawnIndex++;

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void CheckSpawnPointStatus()
    {
        occupiedSpawnPoints.Clear();
        availableSpawnPoints.Clear();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.GetComponent<SpawnPoint>().IsSpawnPointOccupied)
            {
                occupiedSpawnPoints.Add(spawnPoint.transform);
            }
            else
            {
                availableSpawnPoints.Add(spawnPoint.transform);
            }
        }
    }


    private void SpawnRandomEnemy()
    {
        if (enemyToSpawnList.Count == 0 || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No enemies or spawn points configured.");
            return;
        }

        CheckSpawnPointStatus();
        // Debug.Log("Available spawn points: " + availableSpawnPoints.Count);
        // Debug.Log("Occupied spawn points: " + occupiedSpawnPoints.Count);

        int randomEnemyIndex = Random.Range(0, enemyToSpawnList.Count);
        int randomSpawnPointIndex = Random.Range(0, availableSpawnPoints.Count);

        GameObject spawnedEnemy = Instantiate(enemyToSpawnList[randomEnemyIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().SpawnIndex = spawnIndex;
        // Debug.Log("Spawned enemy: " + spawnedEnemy.name);
        // Debug.Log("Spawned enemy at spawn point: " + spawnPoints[randomSpawnPointIndex].name);

        spawnedEnemies.Add(spawnedEnemy);

        lastSpawnedEnemy = spawnedEnemy;

        lastSpawnedEnemyHealthText = lastSpawnedEnemy.GetComponentInChildren<TextMeshProUGUI>().gameObject;
        // Debug.Log("Last spawned enemy health text: " + lastSpawnedEnemyHealthText.name);

        OnEnemySpawnedEvent?.Invoke();
    }

    public GameObject GetLastSpawnedEnemy()
    {
        return lastSpawnedEnemy;
    }


    public List<GameObject> GetCurrentEnemies()
    {
        return spawnedEnemies;
    }

    public GameObject GetLastSpawnedEnemyHealthText()
    {
        return lastSpawnedEnemyHealthText;
    }

    public TextMeshProUGUI GetLastSpawnedEnemyHealthTextTMP()
    {
        return lastSpawnedEnemyHealthText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void RemoveEnemyFromList(int index)
    {
        if (index >= 0 && index < spawnedEnemies.Count)
        {
            spawnedEnemies.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Attempting to remove enemy from list with invalid index.");
        }
    }

    public int GetSpawnIndexForEnemy(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            return enemyScript.SpawnIndex;
        }
        else
        {
            Debug.LogWarning("Enemy script not found on enemy object.");
            return -1;
        }
    }

    public int GetLastSpawnedEnemyIndex()
    {
        return spawnIndex;
    }

    public void ReduceCurrentSpawnCount()
    {
        currentSpawnCount--;
    }

    public void DecrementEnemiesSpawnedIndex()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                if (enemyScript.SpawnIndex > 0)
                    enemyScript.SpawnIndex--;
            }
        }
    }
    private void OndDestory()
    {
        StopAllCoroutines();
        
    }

}
