using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IDamageable
{
    const string DAMAGE = "Damage";

    //Animator animator;
    public PlayerScriptableObject playerData;


    private void Awake ()
    {
        playerData.health = playerData.maxHealth;
        playerData.mana = playerData.maxMana;

        UIManager.Instance.UpdateHealthText(playerData.health, playerData.maxHealth);
        UIManager.Instance.UpdateManaText((int)playerData.mana, (int)playerData.maxMana);


    }

    
    public void TakeDamage(int damageAmount)
    {
        playerData.health -= damageAmount;
        UIManager.Instance.UpdateHealthText((int)playerData.health, playerData.maxHealth);
        //animator.SetTrigger(DAMAGE);

        if (playerData.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        Destroy(gameObject);
    }
}

