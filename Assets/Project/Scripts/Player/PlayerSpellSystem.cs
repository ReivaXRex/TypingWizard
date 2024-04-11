using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpellSystem : MonoBehaviour
{
    private CustomActions inputActions;
    private Quaternion preSpellRotation;
    Transform playerTransform;
    private Vector3 targetIndicatorOffset = new Vector3(0, 0.5f, 0);
    
    [Header("Player Animator")]
    [SerializeField] private Animator animator;

    [Header("Player Data")]
    [SerializeField] private PlayerScriptableObject playerData;

    [Header("Casting")]
    [SerializeField] private Spell spellToCast;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] PlayerSpellInventory playerSpellInventory;
    [SerializeField] private TypeCasting playerTypeCasting;
    [SerializeField] GameObject targetIndicator;
    [SerializeField] LayerMask clickableLayer;
    [SerializeField] private bool isCasting = false;
    public int currentSpellIndex;

    [Header("Auras")]
    [SerializeField] private GameObject[] spellAuras; // 0 = fire, 1 = ice, 2 = poison, 3 = lightning, 4 = defense
    [SerializeField] private GameObject currentSpellAura;
    [SerializeField] private int spellAuraIndex;

   
    void Awake()
    {
        inputActions = new CustomActions();
        AssignInputs(); 
    }

    void Start()
    {

        playerTransform = GetComponent<Transform>();

        TypeCasting.OnSpellCompleted += ReceiveSpellToCast;

    }

    private void Update()
    {
        if (!isCasting)
            Invoke("RechargeMana", playerData.manaRechargeDelay);
    }

    void AssignInputs()
    {
        inputActions.Main.SpellCast.performed += ctx => CastSpell();

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }   

    private void OnDestroy()
    {
        TypeCasting.OnSpellCompleted -= ReceiveSpellToCast;
    }


    public void StartSpellCastingAnimation()
    {
        animator.SetTrigger("CastSpell");
    }


    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(playerSpellInventory.spells[currentSpellIndex].spellData.spellPrefab, spawnPoint.position, Quaternion.LookRotation(SetSpellDirection()));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = SetSpellDirection() * playerSpellInventory.spells[currentSpellIndex].spellData.speed;
        Destroy(projectile, playerSpellInventory.spells[currentSpellIndex].spellData.lifeTime);

        playerTypeCasting.spellCasted = true;
        isCasting = false;
    }

    public Vector3 SetSpellDirection()
    {
        Vector3 direction = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
        {
            SpawnTargetIndicator(hit);
            direction = (hit.point - spawnPoint.position).normalized;

        }

        return direction;
    }

    public void SpawnTargetIndicator(RaycastHit hit)
    {
    
            GameObject indicator = Instantiate(targetIndicator, hit.point + targetIndicatorOffset, Quaternion.identity);
        
    }

    public void CastSpell()
    {
        //Debug.Log("CastSpell event received");
        currentSpellIndex = playerSpellInventory.currentSpellIndex;

        if (playerSpellInventory.spells[currentSpellIndex].spellData.manaCost > playerData.mana)
        {
            Debug.Log("Not enough mana to cast spell");
            return;
        }

        if (!isCasting && !playerTypeCasting.spellCasted)
        {
            preSpellRotation = transform.rotation;

            isCasting = true;
           

            DisplaySpellAura();

            StartSpellCastingAnimation();
            StartCoroutine(CastReset());
            StartCoroutine(HideSpellAura());

            playerData.mana -= playerSpellInventory.spells[currentSpellIndex].spellData.manaCost;
            UIManager.Instance.UpdateManaText((int)playerData.mana, (int)playerData.maxMana);

        }
    }

    IEnumerator CastReset()
    { 
        float delay = 0.35f; 
        yield return new WaitForSeconds(delay);

        isCasting = false;
    }

    IEnumerator HideSpellAura()
    {
        float delay = 2.5f; 
        yield return new WaitForSeconds(delay);

        foreach (GameObject aura in spellAuras)
        {
            if (aura.activeSelf)
            {
                aura.SetActive(false);
            }
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
                currentSpellAura = spellAuras[0];
                spellAuras[0].SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Ice:
                currentSpellAura = spellAuras[1];
                spellAuras[1].SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Poison:
                currentSpellAura = spellAuras[2];
                spellAuras[2].SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Lightning:
                currentSpellAura = spellAuras[3];
                spellAuras[3].SetActive(true);
                break;
            case SpellScriptableObject.ElementalType.Defense:
                currentSpellAura = spellAuras[4];
                spellAuras[4].SetActive(true);
                break;
            default:
                break;
        }
    }

    private void RechargeMana()
    {
        playerData.mana += playerData.manaRechargeRate * Time.deltaTime;
        playerData.mana = Mathf.Clamp(playerData.mana, 0, playerData.maxMana);
        UIManager.Instance.UpdateManaText((int)playerData.mana, (int)playerData.maxMana);

    }

}
