using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    // HEALTHBAR NEBO SRDICKA DODELAT

    private float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Debug.Log("player is dead");
            GameManager.instance.GoToMenu();
            //animace
            //vypnout
        }


    }
}
