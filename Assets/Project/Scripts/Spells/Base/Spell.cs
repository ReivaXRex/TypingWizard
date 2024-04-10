using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class Spell : MonoBehaviour
{
    public SpellScriptableObject spellData;

    [SerializeField]
    private int damageAmount;

    private void Start()
    {
        Spell spell = GetComponent<Spell>();
        if (spell != null && spell.spellData != null)
        {
            damageAmount = spell.spellData.damageAmount;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " Collided with: " + other.gameObject.name);
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            if(other.gameObject.tag != "Indicator")
            Destroy(gameObject, 0.5f);
        }
    }

}
