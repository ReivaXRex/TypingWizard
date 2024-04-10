using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions inputActions;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Movement")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask clickableLayer;
    [SerializeField] float rotationSpeed = 10.0f;

    [Header("Movement Indicator")]
    [SerializeField] GameObject movementIndicator;

    void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        inputActions = new CustomActions();
        AssignInputs();
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
        FaceTarget();
        SetAnimation();
    }

    void FaceTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep the same height to avoid tilting

            Vector3 lookDirection = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

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
            GameObject indicator = Instantiate(movementIndicator, hit.point, Quaternion.identity);

        }
    }
}
