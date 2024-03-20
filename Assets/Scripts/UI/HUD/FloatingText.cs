using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    Transform mainCamera;
    Transform target;
    Transform worldSpaceCanvas;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        target = transform.parent;
        worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform;

        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        transform.position = target.position + offset;
    }
}
