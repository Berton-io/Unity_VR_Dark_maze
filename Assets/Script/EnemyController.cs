using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Detection")]
    public float detectRadius = 12f;
    public float attackDistance = 1.5f;

    [Header("Movement")]
    public float walkSpeed = 1.5f;
    public float chaseSpeed = 3.5f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    public float damage = 15f;

    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer;
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;
    }

   void Update()
{
    if (player == null || !agent.isOnNavMesh) return;

    float distance = Vector3.Distance(transform.position, player.position);
    attackTimer -= Time.deltaTime;

    isChasing = distance <= detectRadius;

    if (isChasing)
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", true);

        if (distance <= attackDistance && attackTimer <= 0f)
        {
            agent.isStopped = true;
            AttackPlayer();
        }
    }
    else
    {
        agent.isStopped = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", false);
    }
}


    void AttackPlayer()
    {
        attackTimer = attackCooldown;

        PlayerHealth hp = player.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(damage); // -15 HP
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}

