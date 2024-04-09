using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Spell : MonoBehaviour
{
    public SpellScriptableObject spellData;
    //public GameObject spellProjectilePrefab;
   // public float spellSpeed;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    /*
  
   
    public string spellName;

    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float projectileLifetime;

    public abstract void CastSpell(Transform spawnPoint);
    */
    private void Awake()
    {
        /*
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = spellData.areaOfEffectRadius;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        */
    }

    private void Start()
    {

    }
    private void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Apply spell effects
        //Apply damage
        //Apply sound effects

    }

}
