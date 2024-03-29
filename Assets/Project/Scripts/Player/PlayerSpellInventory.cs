using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellInventory : MonoBehaviour
{
    public Spell[] spells;

    public int currentSpellIndex = 0;

    public delegate void SpellSwitched();
    public static event SpellSwitched OnSpellSwitched;

    // public int maxSpellCount = 3;

    public void Update()
    {
        SwitchSpell();
    }

    public void SwitchSpell()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCurrentSpell(0);
            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurrentSpell(1);
         
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCurrentSpell(2);
        }
    }

    public string GetSpellName()
    {
        return spells[currentSpellIndex].spellName;
    }


    public void SetCurrentSpell(int index)
    {
        if (index < spells.Length)
        {
            currentSpellIndex = index;
            OnSpellSwitched?.Invoke();
        }
    }

}
