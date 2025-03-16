using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cone;



    // HEALTHBAR NEBO SRDICKA DODELAT
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) //pri smrti se vypnou vsechny ostatni komponenty na enemy
        {
            animator.SetTrigger("IsDead");

            gameObject.GetComponent<EnemyAttack>().StopAttackCoroutine();
            gameObject.GetComponent<EnemyMovement>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<EnemyHealth>().enabled = false;
            gameObject.GetComponent<EnemyAttack>().enabled = false;
            cone.SetActive(false);

        }
    }
}
