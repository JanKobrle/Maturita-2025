using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private float maxHealth;
    // HEALTHBAR NEBO SRDICKA DODELAT

    private float currentHealth;
    void Start()
    {
        var temp = maxHealth;
        maxHealth += PlayerPrefs.GetInt("MaxHealth");
        if (maxHealth < temp) maxHealth = temp;

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //healthbar
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("MainMenu");
            //animace
            //vypnout vsechno
        }
    }

}
