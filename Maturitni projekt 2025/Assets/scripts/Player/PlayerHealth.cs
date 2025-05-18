using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OD.Player
{
    public class PlayerHealth : MonoBehaviour
    {

        [SerializeField] private float maxHealth;
        [SerializeField] private GameObject deathPanel;
        [SerializeField] private Animator animator;
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
            if (currentHealth <= 0)
            {
                deathPanel.SetActive(true);
                animator.SetTrigger("IsDead");
                StartCoroutine(LoadMenu());
            }
        }

        IEnumerator LoadMenu() //[20]
        {
            yield return new WaitForSeconds(2f); //èeká, až se pøehraje "you died"
            SceneManager.LoadScene("MainMenu");
        }
    }
}
