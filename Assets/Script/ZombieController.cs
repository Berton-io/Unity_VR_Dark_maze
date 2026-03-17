using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public float ch;

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
    private bool isDead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = walkSpeed;
        agent.stoppingDistance = attackDistance;
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.isStopped = false;

        animator.applyRootMotion = false;
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;
        if (!agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, player.position);
        attackTimer -= Time.deltaTime;
        ch=distance;
        Debug.Log("Velocity: " + agent.velocity.magnitude);
        agent.Move(agent.desiredVelocity * Time.deltaTime);

        // DETECT PLAYER
        if (distance <= detectRadius)
        {
            ChasePlayer(distance);
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer(float distance)
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", true);

        if (distance <= attackDistance)
        {
            agent.isStopped = true;

            animator.SetBool("IsChasing", false);

            if (attackTimer <= 0f)
            {
                AttackPlayer();
            }
        }
    }

    void Idle()
    {
        agent.isStopped = true;

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsChasing", false);
    }

    void AttackPlayer()
    {
        attackTimer = attackCooldown;
        animator.SetTrigger("Attack");

        PlayerHealth hp = player.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }
    }

    // OPTIONAL: dipanggil dari animasi death (Animation Event)
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("Death", true);
        Destroy(gameObject, 3f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}

