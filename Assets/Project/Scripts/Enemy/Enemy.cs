using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    const string DEAD = "IsDead";
    const string DAMAGE = "Damage";

    public EnemyScriptableObject enemyData;

    [SerializeField] private Animator animator;

    [SerializeField] private int damageAmount;

    [SerializeField] private GameObject enemyDeathVFXPrefab;

    [SerializeField] TextMeshProUGUI healthText;

    
    public delegate void OnEnemyDeath();
    public static event OnEnemyDeath OnEnemyDeathEvent;
    

    public bool isDead;

    [SerializeField]
    private int spawnIndex;

    public int SpawnIndex
    {
        get { return spawnIndex; }
        set { spawnIndex = value; }
    }


    [SerializeField] private float deathTime;

    private void Awake()
    {
        enemyData.health = enemyData.maxHealth;
        isDead = false;

    }
    void Start()
    {

       UIManager.Instance.UpdateEnemyHealthText(healthText, enemyData.health, enemyData.maxHealth);

        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.enemyData != null)
        {
            damageAmount = enemy.enemyData.damageAmount;
        }

        Debug.Log("START >>> " + gameObject.name + "'s Index is: " + spawnIndex);

    }

    void Update()
    {
       Invoke("Log", 2.0f);
    }

    private void Log()
    {
        Debug.Log(gameObject.name + "'s Index is: " + spawnIndex);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " Collided with: " + other.gameObject.name);
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyData.health -= damageAmount;
        if (enemyData.health < 0)
            enemyData.health = 0;

        UIManager.Instance.UpdateEnemyHealthText(healthText, enemyData.health, enemyData.maxHealth);
        animator.SetTrigger(DAMAGE);

        if (enemyData.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SpawnManager.Instance.ReduceCurrentSpawnCount();

        
        SpawnManager.Instance.RemoveEnemyFromList(spawnIndex);


        UIManager.Instance.RemoveEnemyFromList(spawnIndex);
        UIManager.Instance.RemoveEnemyHealthTextList(spawnIndex);
        UIManager.Instance.RemoveEnemyHealthTextTMPList(spawnIndex);

        isDead = true;

        LockRotation();
        StartCoroutine(PlayDeathVFX());

        animator.SetBool(DEAD, true);
        animator.SetBool("IsIdle", false);

        SpawnManager.Instance.DecrementEnemiesSpawnedIndex();
        //OnEnemyDeathEvent?.Invoke();
        Destroy(gameObject, deathTime);
    }

    IEnumerator PlayDeathVFX()
    {
        yield return new WaitForSeconds(2.0f);
        enemyDeathVFXPrefab.SetActive(true);
    }

    private void LockRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }



   


}
