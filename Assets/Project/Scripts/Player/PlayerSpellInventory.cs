using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellInventory : MonoBehaviour
{
    public SpellScriptableObject[] spellData;

    public Spell[] spells;
    public string[] SpellWordBank = new string[3];
    public int currentSpellIndex = 0;

    public delegate void SpellSwitched();
    public static event SpellSwitched OnSpellSwitched;

    private void Start()
    {
        spells = new Spell[spellData.Length];
        for (int i = 0; i < spellData.Length; i++)
        {
            spells[i] = spellData[i].spellPrefab.GetComponent<Spell>();
        }
        FillSpellWordBank();
    }

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
        //return spellData[currentSpellIndex].name;
        return spells[currentSpellIndex].spellData.spellName;
    }


    public void SetCurrentSpell(int index)
    {
        if (index < spells.Length)
        {
            currentSpellIndex = index;
            OnSpellSwitched?.Invoke();
        }
    }


    public void FillSpellWordBank()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            SpellWordBank[i] = spellData[i].spellName;
            //SpellWordBank[i] = spells[i].spellData.spellName;
        }

        /*
        foreach (string spell in spellBank)
        {
            Debug.Log(spell);
        }
        */
    }

    public Spell GetSpellFromWord(string word)
    {
        for (int i = 0; i < spellData.Length; i++)
        {
            /*
            if (spellData[i].spellName == word)
            {
                return spells[i];
            }*/
            
            if (spells[i].spellData.spellName == word)
            {
                return spells[i];
            }
        }

        return null;
    }

    public Spell currentSpellToCast // Corrected as a property
    {
        get { return spells[currentSpellIndex]; }
        // get { return spells[currentSpellIndex]; }
    }


}
