using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyName;
    public int health;
    public int maxHealth;
    public float speed;
    public int damageAmount;
    public float attackSpeed;
    public float attackRange;
    public float aggroRange;
    public float attackCooldown;

    public float projectileSpeed;

    public ElementalWeakness elementalWeakness;
    public AttackType attackType;

    public enum ElementalWeakness
    {
        Fire,
        Ice,
        Poison,
        Lightning,

    }

    public enum AttackType
    {
        Melee,
        Ranged,
        Magic
    }
}

