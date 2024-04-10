using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    private static UIManager _instance;

    // Property to access the singleton instance
    public static UIManager Instance
    {
        get
        {
            // If the instance doesn't exist, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                // If it's still not found, log a warning
                if (_instance == null)
                {
                    Debug.LogWarning("UIManager instance not found in the scene. Creating a new one.");
                    // Create a new GameObject and add UIManager component to it
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    // Your UIManager methods and properties go here

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private List<Text> enemyHealthText;
    [SerializeField] private Text spellText;

    private void Awake()
    {
        // Ensure there's only one instance of UIManager
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the UIManager persists between scenes
        }
    }

    
    public void UpdateHealthText(int health, int maxHealth)
    {

        healthText.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
    }

    public void UpdateManaText(int mana, int maxMana)
    {
        manaText.text = "Mana: " + mana.ToString()+ "/" + maxMana.ToString();
    }

    public void UpdateSpellText(string spellName)
    {
        spellText.text = spellName;
    }

    public void UpdateEnemyHealthText(int index, int health, int maxHealth)
    {
        enemyHealthText[index].text = "HP: " + health.ToString() + "/" + maxHealth.ToString(); ;
    }


}
