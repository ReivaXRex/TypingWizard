using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    const string IDLE = "IsIdle";
    const string WALK = "IsWalking";
    const string ATTACK = "Attack";
    const string SHOOT = "Shoot";

    [Header("AI")]
    public NavMeshAgent agent;

    [Header("Transforms")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayer, playerLayer;
    
    [Header("Transforms")]
    public GameObject projectile;

    [Header("Animator")]
    [SerializeField] Animator animator;


    private SphereCollider attackCollider;
    private Enemy enemy;

    private Vector3 walkPoint;
    private bool walkPointSet;
    
    private float walkPointRange;

    [SerializeField] private float aggroRange, attackRange, projectileSpeed, attackSpeed;
    private bool playerInSightRange, playerInAttackRange, alreadyAttacked;


    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.enemyData != null)
        {
            agent.speed = enemy.enemyData.speed;
            aggroRange = enemy.enemyData.aggroRange;
            attackRange = enemy.enemyData.attackRange;
            attackSpeed = enemy.enemyData.attackSpeed;
            projectileSpeed = enemy.enemyData.projectileSpeed;
        }
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        attackCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (enemy.isDead)
            return;

        SetWalkingAnimation();

        playerInSightRange = Physics.CheckSphere(transform.position, aggroRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (enemy.enemyData.attackType == EnemyScriptableObject.AttackType.Ranged)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackSpeed);
                Debug.Log("Ranged attack");
                PlayRangedAttackAnimation();
            }
            else if (enemy.enemyData.attackType == EnemyScriptableObject.AttackType.Melee)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackSpeed);
                Debug.Log("Melee attack");
                PlayMeleeAttackAnimation();
            }
            else
            {
                // Magic attack
            }
          
       
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void PlayRangedAttackAnimation()
    {
        animator.SetTrigger(SHOOT);
    }


    private void SpawnProjectile()
    {

        Rigidbody rb = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Debug.Log("SpawnProjectile");
        rb.velocity = SetProjectileDirection() * projectileSpeed;

        Destroy(rb.gameObject, 1.0f);
    }

    private Vector3 SetProjectileDirection()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        return direction;
    }

    private void PlayMeleeAttackAnimation()
    {
        animator.SetTrigger(ATTACK);

    }


    private void SetWalkingAnimation()
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
