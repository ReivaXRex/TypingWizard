using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private enum DamageOrigin
    {
        Enemy,
        Spell
    }

    [SerializeField]
    private DamageOrigin damageOrigin;


    [SerializeField]
    private int damageAmount;
    private void Awake()
    {
        if(this.gameObject.tag == "EnemySpell" || this.gameObject.tag == "Enemy")
        {
            damageOrigin = DamageOrigin.Enemy;
        }
        else if(this.gameObject.tag == "Spell")
        {
            damageOrigin = DamageOrigin.Spell;
        }
        else
        {
            Debug.LogError("Damage Source does not have a valid tag: " + this.gameObject.tag);
        }
    }

    private void Start()
    {
        if(damageOrigin == DamageOrigin.Enemy)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null && enemy.enemyData != null)
            {
                damageAmount = enemy.enemyData.damageAmount;
            }
           
        }
        else if(damageOrigin == DamageOrigin.Spell)
        {
            Spell spell = GetComponent<Spell>();
            if (spell != null && spell.spellData != null)
            {
                damageAmount = spell.spellData.damageAmount;
            }

        }
      
     
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " Collided with: " + other.gameObject.name + " (Trigger)");
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
