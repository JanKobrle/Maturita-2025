using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Animator animator;
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
            //animace
            //vypnout vsechno
            deathPanel.SetActive(true);
            animator.SetTrigger("IsDead");
            StartCoroutine(LoadMenu());
        }
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
