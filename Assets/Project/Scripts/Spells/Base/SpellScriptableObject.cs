using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class SpellScriptableObject : ScriptableObject
{
    public string spellName;
    public float manaCost;
    public float manaCooldown;
    public float lifeTime;
    public float speed;
    public int damageAmount;
    public float areaOfEffectRadius;

    public GameObject spellPrefab;
    //public GameObject spellAuraPrefab;

    public DebuffType debuffType;
    public SpellType spellType;
    public ElementalType elementalType;
    //public SpellType spellType;
    //public ElementalType elementalType;

   
    public enum DebuffType
    {
        None,
        Burning,
        Poison,
        Drenched,
        Frozen,
        Shocked,
        Stunned
    }
    public enum SpellType
    {
        Projectile,
        AreaOfEffect,
        Buff,
        Debuff
    }

    public enum ElementalType
    {
        Fire,
        Ice,
        Poison,
        Lightning,
        Defense
       
    }
}
