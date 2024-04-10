using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactWithSpell : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with Spell (Trigger)");
        if (other.gameObject.CompareTag("Spell"))
        {
            Destroy(this.gameObject);
        }
    }
}
