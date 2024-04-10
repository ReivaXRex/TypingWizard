using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationHitbox : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;

    private void Awake()
    {
        // Ensure attack collider is initially disabled
        attackCollider.enabled = false;
    }

    // Called by animation event when attack should hit
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
        StartCoroutine(DisableColliderAfterDelay());
    }

    // Coroutine to disable collider after a delay
    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Adjust this delay as needed
        attackCollider.enabled = false;
    }
}
