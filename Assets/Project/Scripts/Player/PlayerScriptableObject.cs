using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerScriptableObject : ScriptableObject
{

    public string playerName;
    public int health;
    public int maxHealth;
    public float mana;
    public float maxMana;
    public float manaRechargeRate;
    public float manaRechargeDelay;
    //public float speed;




}
