using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyScriptableObject enemyData;

    [SerializeField]
    private int damageAmount;



    // Start is called before the first frame update
    private void Awake()
    {
        enemyData.health = enemyData.maxHealth;
        UIManager.Instance.UpdateEnemyHealthText(0, enemyData.health, enemyData.maxHealth);
    }
    void Start()
    {
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.enemyData != null)
        {
            damageAmount = enemy.enemyData.damageAmount;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " Collided with: " + other.gameObject.name);
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            /*
            if (other.gameObject.tag != "Indicator")
                Destroy(gameObject, 0.5f);*/
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyData.health -= damageAmount;
        UIManager.Instance.UpdateEnemyHealthText(0, enemyData.health, enemyData.maxHealth);
        //UIManager.Instance.UpdateHealthText((int)playerData.health);

        if (enemyData.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        Destroy(gameObject);
    }
}
