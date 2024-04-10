using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string ATTACK = "Attack";
    const string SHOOT = "Shoot";

    public NavMeshAgent agent;

    public Transform player;
    public Transform spawnPoint;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject projectile;
    private SphereCollider attackCollider;

    private Enemy enemy;

    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public float projectileSpeed;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] Animator animator;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        if (enemy != null && enemy.enemyData != null)
        {
            agent.speed = enemy.enemyData.speed;
            sightRange = enemy.enemyData.aggroRange;
            attackRange = enemy.enemyData.attackRange;
            timeBetweenAttacks = enemy.enemyData.attackSpeed;
            projectileSpeed = enemy.enemyData.projectileSpeed;
        }
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        attackCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        SetWalkingAnimation();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
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
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                RangedAttack();
            }
            else if (enemy.enemyData.attackType == EnemyScriptableObject.AttackType.Melee)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                MeleeAttack();
            }
            else
            {
                // Magic attack
            }
            // Attack code here
       
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

    private void RangedAttack()
    {
        PlayRangedAttackAnimation();

        Vector3 direction = (player.position - transform.position).normalized;
        Rigidbody rb = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.velocity = direction * projectileSpeed;
        Destroy(rb.gameObject, 1.0f);
        //rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        //rb.AddForce(transform.up * 20f, ForceMode.Impulse);
    }

    private void PlayMeleeAttackAnimation()
    {
        animator.SetTrigger(ATTACK);

    }


    private void MeleeAttack()
    {
        PlayMeleeAttackAnimation();
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
