using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeCasting : MonoBehaviour
{

    [SerializeField] PlayerSpellInventory playerSpellInventory;

    private string remainingWord = string.Empty;

    public bool spellCasted = true;

    public delegate void SpellCompleted(Spell spell);
    public static event SpellCompleted OnSpellCompleted;

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


    public void SetCurrentWord()
    {
        int spellIndex = playerSpellInventory.currentSpellIndex;
        SetRemainingWord(playerSpellInventory.SpellWordBank[spellIndex]);
    }

    private void SetRemainingWord(string word)
    {
        remainingWord = word;
        UIManager.Instance.UpdateSpellText(remainingWord);
   
    }

    private void UpdateWord()
    {
        SetCurrentWord();
    }


    public void CheckInput()
    {
        if (Input.anyKeyDown )
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }

            if (IsWordComplete())
            {
                OnSpellCompleted?.Invoke(playerSpellInventory.GetSpellFromWord(remainingWord));
                spellCasted = false; 
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
                spellCasted = false;
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
