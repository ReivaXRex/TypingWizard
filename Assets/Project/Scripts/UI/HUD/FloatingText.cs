using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    Transform mainCamera;
    Transform target;
    Transform worldSpaceCanvas;

    public Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main.transform;
        target = transform.parent;
        worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform;

        transform.SetParent(worldSpaceCanvas);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        transform.position = target.position + offset;
    }
}
