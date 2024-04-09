using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions inputActions;

    NavMeshAgent agent;
    
    [SerializeField] Animator animator;

    [Header("Movement")]
    [SerializeField] LayerMask clickableLayer;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        inputActions = new CustomActions();
        AssignInputs();
    }

    void AssignInputs()
    {
        inputActions.Main.Move.performed += ctx => ClickToMove();

    }

    void ClickToMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayer))
        {
            agent.destination = hit.point;
            
        }
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        //FaceTarget();
        SetAnimation();
    }

    /*
    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }*/

    void SetAnimation()
    {
        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool(IDLE, false);
            animator.SetBool(WALK, true);
        }
        else
        {
            animator.SetBool(IDLE, true);
            animator.SetBool(WALK, false);
        }
    }
}
