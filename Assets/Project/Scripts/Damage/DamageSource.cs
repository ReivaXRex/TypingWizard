using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount;
    private void Start()
    {
        Spell spell = GetComponent<Spell>();
        if (spell != null && spell.spellData != null)
        {
            damageAmount = spell.spellData.damageAmount;
        }
    }

    private void Update()
    {
        //Debug.Log("Damage Amount: " + damageAmount);
        
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}
