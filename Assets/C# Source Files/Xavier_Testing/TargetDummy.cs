using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamageable
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    private Healthbar healthbar;

    // public SpellManager spellManager;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar = GetComponentInChildren<Healthbar>();
        healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        Destroy(gameObject);
    }


}
