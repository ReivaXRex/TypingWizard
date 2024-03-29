using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCasting : MonoBehaviour
{
    [SerializeField]
    PlayerSpellInventory playerSpellInventory;

    [SerializeField]
    private Animator animator;

    //public GameObject spellPrefab;
    public Transform spawnPoint;

    public int currentSpellIndex;
  

    // Start is called before the first frame update
    void Start()
    {
        playerSpellInventory = GetComponent<PlayerSpellInventory>();
       // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpellIndex = playerSpellInventory.currentSpellIndex;
    }

    
    /*
    public void SpawnSpell()
    {
        animator.SetTrigger("CastSpell");

        
        playerSpellInventory.spells[currentSpellIndex].CastSpell(spawnPoint);
        //Instantiate(spellPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    */
    

    public void StartSpellCastingAnimation()
    {
        animator.SetTrigger("CastSpell");
    }

    // This method is called from the Animation Event in the Animator
    public void SpawnProjectile()
    {
        StartCoroutine(DelayedSpawn());
    }

    IEnumerator DelayedSpawn()
    {
        // Adjust the delay time as needed (in seconds)
        float delay = 0.35f; // Example: 1 second delay
        yield return new WaitForSeconds(delay);

        // Spawn the projectile after the delay
        playerSpellInventory.spells[currentSpellIndex].CastSpell(spawnPoint);
    }
}
