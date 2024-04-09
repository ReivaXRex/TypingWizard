using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypeCasting;

public class PlayerSpellSystem : MonoBehaviour
{
    [SerializeField] private Spell spellToCast;

    [SerializeField] private float maxMana = 100.0f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRegenRate = 2.0f;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] PlayerSpellInventory playerSpellInventory;
    [SerializeField] private Animator animator;
    [SerializeField] private TypeCasting playerTypeCasting;

    [SerializeField] private GameObject fireSpellAura, iceSpellAura, poisonSpellAura, 
        lightningSpellAura, defenseSpellAura;

    //TO REMOVE:
    //[SerializeField] private float spellCooldown = .25f;
   // [SerializeField] private float currentCastTimer;

    //public GameObject spellPrefab;
    [SerializeField] private bool isCasting = false;

   // private CustomActions playerControls;

    public int currentSpellIndex;
  

    // Start is called before the first frame update
    void Start()
    {
        playerSpellInventory = GetComponent<PlayerSpellInventory>();
        playerTypeCasting = GetComponent<TypeCasting>();
        // playerControls = new CustomActions();
        TypeCasting.OnSpellCompleted += ReceiveSpellToCast;
        // animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
      //  playerControls.Enable();
    }

    private void OnDisable()
    {
      //  playerControls.Disable();
    }   

    // Update is called once per frame
    void Update()
    {
        currentSpellIndex = playerSpellInventory.currentSpellIndex;
        if (Input.GetKeyDown(KeyCode.Space) && !isCasting && !playerTypeCasting.spellCasted) // Change the key to whatever you want
        {
            CastSpell();
            playerTypeCasting.spellCasted = true;
           // isCasting = false;
        }

        //playerTypeCasting.spellCasted = false;
        //isCasting = false;
    }

    private void OnDestroy()
    {
        TypeCasting.OnSpellCompleted -= ReceiveSpellToCast;
    }


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
        Vector3 direction = spawnPoint.forward;
        GameObject projectile = Instantiate(playerSpellInventory.spells[currentSpellIndex].spellData.spellPrefab, spawnPoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = direction * playerSpellInventory.spells[currentSpellIndex].spellData.speed;
        
        Destroy(projectile, playerSpellInventory.spells[currentSpellIndex].spellData.lifeTime);
        //playerSpellInventory.spells[currentSpellIndex].CastSpell(spawnPoint);
    }


    /*
    void OnWordCompleted()
    {
        Debug.Log("OnWordCompleted event received");
        if(!isCasting)
        {
            isCasting = true;
            currentCastTimer = 0;
            StartSpellCastingAnimation();
            SpawnProjectile();
        }
        if(isCasting)
        {
            currentCastTimer += Time.deltaTime;

            if(currentCastTimer > spellCooldown)
            {
                isCasting = false;
               
            }
        }
        //playerSpellCasting.SpawnSpell();
    }*/

    public void CastSpell()
    {
        Debug.Log("CastSpell event received");
        if (!isCasting)
        {
            isCasting = true;
            // currentCastTimer = 0;
            DisplaySpellAura();
            StartSpellCastingAnimation();
            SpawnProjectile();
            StartCoroutine(CastReset());
            StartCoroutine(HideSpellAura());
        }
    }

    IEnumerator CastReset()
    { 
        // Adjust the delay time as needed (in seconds)
        float delay = 0.35f; // Example: 1 second delay
        yield return new WaitForSeconds(delay);

        // Spawn the projectile after the delay
        isCasting = false;
    }

    IEnumerator HideSpellAura()
    {
        // Adjust the delay time as needed (in seconds)
        float delay = 2.5f; // Example: 1 second delay
        yield return new WaitForSeconds(delay);

        // Spawn the projectile after the delay

        // check to see if the aura is active
        if (fireSpellAura.activeSelf)
        {
            fireSpellAura.SetActive(false);
        }
        else if (iceSpellAura.activeSelf)
        {
            iceSpellAura.SetActive(false);
        }
        else if (lightningSpellAura.activeSelf)
        {
            lightningSpellAura.SetActive(false);
        }
        else if (poisonSpellAura.activeSelf)
        {
            poisonSpellAura.SetActive(false);
        }
        else if (defenseSpellAura.activeSelf)
        {
            defenseSpellAura.SetActive(false);
        }
    }

    public void ReceiveSpellToCast(Spell spell)
    {
        spell = playerSpellInventory.currentSpellToCast;
    }

    public void DisplaySpellAura()
    {
        switch (playerSpellInventory.spells[currentSpellIndex].spellData.elementalType)
        {
            case SpellScriptableObject.ElementalType.Fire:
                fireSpellAura.SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Ice:
                iceSpellAura.SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Lightning:
                lightningSpellAura.SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Poison:
                poisonSpellAura.SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Earth:
                defenseSpellAura.SetActive(true);
                break;
            default:
                break;
        }
    }
}
