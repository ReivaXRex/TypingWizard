using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationHitbox : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;

    private void Awake()
    {
        attackCollider.enabled = false;
    }

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
        StartCoroutine(DisableColliderAfterDelay());
    }

    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); 
        attackCollider.enabled = false;
    }
}
