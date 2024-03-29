using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSpellCasting playerSpellCasting;

    void Start()
    {
        TypeCasting.OnWordCompleted += OnWordCompleted;
    }
    void OnWordCompleted()
    {
        Debug.Log("OnWordCompleted event received");
        playerSpellCasting.StartSpellCastingAnimation();
        playerSpellCasting.SpawnProjectile();
        //playerSpellCasting.SpawnSpell();
    }

}
