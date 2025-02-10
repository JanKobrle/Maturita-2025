using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Animator animator;


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
            animator.SetBool("IsDead", true);
            Destroy(gameObject);
        }
    }
}
