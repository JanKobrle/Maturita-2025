using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float playerSpotRange;

    [SerializeField] private Animator animator;


    private NavMeshAgent agent;
    private EnemyAttack enemyAttack;
    private Transform playerTransform;
    private bool canMove;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyAttack = gameObject.GetComponent<EnemyAttack>();
        canMove = true;
    }

    void Update()
    {
        if (canMove) HandleMovement();
    }


    public void DisableMovement()
    {
        canMove = false;
        agent.destination = transform.position;
    }
    public void EnableMovement()
    {
        canMove = true;
    }
    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            agent.destination = transform.position;
            enemyAttack.StartAttack(playerTransform);
            animator.SetBool("IsRunning", false); 

        }
        else if (Vector3.Distance(transform.position, playerTransform.position) <= playerSpotRange)
        {
            enemyAttack.CancelRotate();
            agent.destination = playerTransform.position;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            agent.destination = transform.position;
            animator.SetBool("IsRunning", false);
        }

            
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerSpotRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

}

