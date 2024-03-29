using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public int damageAmount;

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
