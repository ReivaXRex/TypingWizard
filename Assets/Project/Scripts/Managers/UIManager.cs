using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    Debug.LogWarning("UIManager instance not found in the scene. Creating a new one.");
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<GameObject> enemyHealthTextGameObjectList;
    [SerializeField] private List<TextMeshProUGUI> enemyHealthTextTMPList = new List<TextMeshProUGUI>();
    [SerializeField] private Text spellText;


    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }

    }


    private void Start()
    {

        SpawnManager.OnEnemySpawnedEvent += () => AddEnemyToList(SpawnManager.Instance.GetLastSpawnedEnemy());
        SpawnManager.OnEnemySpawnedEvent += () => AddEnemyHealthTextList(SpawnManager.Instance.GetLastSpawnedEnemyHealthText());
        SpawnManager.OnEnemySpawnedEvent += () => AddEnemyHealthTextTMPList(SpawnManager.Instance.GetLastSpawnedEnemyHealthTextTMP());

        /*
        Enemy.OnEnemyDeathEvent += () => RemoveEnemyFromList(Enemy.spawnIndex);
        Enemy.OnEnemyDeathEvent += () => RemoveEnemyHealthTextList(Enemy.spawnIndex);
        Enemy.OnEnemyDeathEvent += () => RemoveEnemyHealthTextTMPList(Enemy.spawnIndex);

        Enemy.OnEnemyDeathEvent += RemoveEnemy;
        */

    }

    public void AddEnemyToList(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void AddEnemyHealthTextList(GameObject enemyHealthText)
    {
        enemyHealthTextGameObjectList.Add(enemyHealthText);
    }

    public void AddEnemyHealthTextTMPList(TextMeshProUGUI enemyHealthTextTMP)
    {
        enemyHealthTextTMPList.Add(enemyHealthTextTMP);
    }

    public void RemoveEnemyFromList(int index)
    {
        if (index >= 0 && index < enemyList.Count)
        {
            enemyList.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Attempting to remove enemy from list with invalid index.");
        }
    }

    public void RemoveEnemyHealthTextList(int index)
    {
        if (index >= 0 && index < enemyHealthTextGameObjectList.Count)
        {
            enemyHealthTextGameObjectList.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Attempting to remove enemy health text from list with invalid index.");
        }
    }

    public void RemoveEnemyHealthTextTMPList(int index)
    {
        enemyHealthTextTMPList.RemoveAt(index);
    }

    /*
    public void RemoveEnemy()
    {
        int spawnIndex = SpawnManager.Instance.GetSpawnIndexForEnemy(Enemy.gameObject);
        RemoveEnemyFromList(spawnIndex);
        RemoveEnemyHealthTextList(spawnIndex);
        RemoveEnemyHealthTextTMPList(spawnIndex);
    }*/



    public void UpdateHealthText(int health, int maxHealth)
    {

        healthText.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
    }

    public void UpdateManaText(int mana, int maxMana)
    {
        manaText.text = "Mana: " + mana.ToString()+ "/" + maxMana.ToString();
    }

    public void UpdateSpellText(string spellName)
    {
        spellText.text = spellName;
    }
    
    public void UpdateEnemyHealthText(TextMeshProUGUI tmp, int health, int maxHealth)
    {
        tmp.text = "HP: " + health.ToString() + "/" + maxHealth.ToString();
    }

    private void OnDestroy()
    {
        SpawnManager.OnEnemySpawnedEvent -= () => AddEnemyToList(SpawnManager.Instance.GetLastSpawnedEnemy());
        SpawnManager.OnEnemySpawnedEvent -= () => AddEnemyHealthTextList(SpawnManager.Instance.GetLastSpawnedEnemyHealthText());
        SpawnManager.OnEnemySpawnedEvent -= () => AddEnemyHealthTextTMPList(SpawnManager.Instance.GetLastSpawnedEnemyHealthTextTMP());

        /*
        Enemy.OnEnemyDeathEvent -= () => RemoveEnemyFromList(Enemy.SpawnIndex);
        Enemy.OnEnemyDeathEvent -= () => RemoveEnemyHealthTextList(Enemy.spawnIndex);
        Enemy.OnEnemyDeathEvent -= () => RemoveEnemyHealthTextTMPList(Enemy.spawnIndex);

        Enemy.OnEnemyDeathEvent -= RemoveEnemy;
        */
    }

   


}
