using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Animator animator;
    // HEALTHBAR NEBO SRDICKA DODELAT
    [SerializeField] TextMeshProUGUI hpText;

    private float currentHealth;
    void Start()
    {
        maxHealth += PlayerPrefs.GetInt("MaxHealth");
        currentHealth = maxHealth;
        hpText.text = "Hp: " + currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        hpText.text = "Hp: " + currentHealth.ToString();
        //healthbar
        if (currentHealth <= 0)
        {
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
