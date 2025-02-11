using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] components;


    //private HealthBar healthBar;
    private float currentHealth;

    void Start()
    {
        //healthBar = GetComponentInChildren<HealthBar>();        //get reference to HealthBar script
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);          //call its functions
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            animator.SetTrigger("IsDead");
            gameObject.GetComponent<EnemyMovement>().enabled = false; //pri smrti se vypnou vsechny ostatni komponenty na enemy
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<EnemyHealth>().enabled = false;

        }
    }
}
