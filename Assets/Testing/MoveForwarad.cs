using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwarad : MonoBehaviour
{
    Transform transform;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        transform.position += transform.forward * speed *Time.deltaTime;

    }
}
