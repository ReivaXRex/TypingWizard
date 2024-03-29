using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public string spellName;

    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float projectileLifetime;

    public abstract void CastSpell(Transform spawnPoint);
}
