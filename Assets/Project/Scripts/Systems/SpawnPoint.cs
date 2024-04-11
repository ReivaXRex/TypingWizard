using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] bool isSpawnPointOccupied = false;

    public bool IsSpawnPointOccupied
    {
        get { return isSpawnPointOccupied; }
        set { isSpawnPointOccupied = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isSpawnPointOccupied = true;
            Debug.Log("Spawn Point Occupied " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isSpawnPointOccupied = false;
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isSpawnPointOccupied = true;
            Debug.Log("Spawn Point Occupied " + gameObject.name);
        }
    }*/
}
