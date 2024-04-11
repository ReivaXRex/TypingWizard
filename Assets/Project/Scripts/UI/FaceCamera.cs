using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene.");
        }

        FaceTowardsCamera();
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            FaceTowardsCamera();
        }
    }

    private void FaceTowardsCamera()
    {
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        transform.LookAt(transform.position + directionToCamera, Vector3.up);
    }

    private void LockRotation()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
