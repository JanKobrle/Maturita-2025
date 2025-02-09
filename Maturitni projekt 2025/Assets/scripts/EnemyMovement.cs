using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float playerSpotRange;

    private NavMeshAgent agent;  
    private Transform playerTransform;
    public Transform player;
    private bool playerInSpotRange;
    private bool playerInAttackRange;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
 
    }

    void Update()
    {
        playerInSpotRange = Physics.CheckSphere(transform.position, playerSpotRange);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange);

        if (playerInSpotRange && !playerInAttackRange) { ChasePlayer(); }
        //if (playerInSpotRange && playerInAttackRange) { AttackPlayer(); }
    }

    private void ChasePlayer()
    {

    }

    //private void AttackPlayer()
    //{
    //    agent.SetDestination(transform.position);
    //    transform.LookAt(player);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerSpotRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }


}
