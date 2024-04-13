using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactWithPlayer : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with Player (Trigger)");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
