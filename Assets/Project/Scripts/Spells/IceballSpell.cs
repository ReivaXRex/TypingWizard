using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceballSpell : Spell
{
    IceballSpell()
    {
        spellName = "iceball";
        projectileSpeed = 4;
        projectileLifetime = 7;
    }

    public override void CastSpell(Transform spawnPoint)
    {

        // Calculate direction based on player's facing direction
        Vector3 direction = spawnPoint.forward;

        // Spawn the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(direction));

        // Apply velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = direction * projectileSpeed;

        // Destroy the projectile after its lifetime
        Destroy(projectile, projectileLifetime);
    }
}
