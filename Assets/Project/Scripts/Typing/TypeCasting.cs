using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeCasting : MonoBehaviour
{
    //public string[] spellBank = new string[3];
    public Text wordOutput = null;

    PlayerSpellInventory playerSpellInventory;

    private string remainingWord = string.Empty;
    public bool spellCasted = true;

    public delegate void SpellCompleted(Spell spell);
    public static event SpellCompleted OnSpellCompleted;

    /*
    public delegate void WordCompleted();
    public static event WordCompleted OnWordCompleted;
    */

    /*
    public delegate void WordSwitched();
    public static event WordSwitched OnWordSwitched;
    */

    void Awake()
    {
        playerSpellInventory = GetComponent<PlayerSpellInventory>();
    }

    void Start()
    {
        playerSpellInventory.FillSpellWordBank();
        SetCurrentWord();
        PlayerSpellInventory.OnSpellSwitched += UpdateWord;
        spellCasted = true;
    }

    void Update()
    {
        CheckInput();
    }
    /*
    public void FillSpellBank()
    {
        for (int i = 0; i < spellBank.Length; i++)
        {
            spellBank[i] = playerSpellInventory.spells[i].spellName;
        }

        /*
        foreach (string spell in spellBank)
        {
            Debug.Log(spell);
        }
        
    }*/

    public void SetCurrentWord()
    {
        // Debug.Log("Setting current word");
        int spellIndex = playerSpellInventory.currentSpellIndex;
        SetRemainingWord(playerSpellInventory.SpellWordBank[spellIndex]);
    }

    private void SetRemainingWord(string word)
    {
        remainingWord = word;
        wordOutput.text = remainingWord;
    }

    private void UpdateWord()
    {
        SetCurrentWord();
    }


    public void CheckInput()
    {
        if (Input.anyKeyDown )//&& !spellCasted)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }

            if (IsWordComplete())
            {
                OnSpellCompleted?.Invoke(playerSpellInventory.GetSpellFromWord(remainingWord));
                spellCasted = false; // Trigger new event
            }
        }
    }

    public void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord();
                OnSpellCompleted?.Invoke(playerSpellInventory.GetSpellFromWord(remainingWord));
                spellCasted = false;// Trigger new event
            }
        }
    }

    public bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    public void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    public bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    void OnDestroy()
    {
        PlayerSpellInventory.OnSpellSwitched -= UpdateWord; 
    }
}
