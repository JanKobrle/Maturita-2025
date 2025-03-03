using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{

    [SerializeField] private float maxHealth;
    // HEALTHBAR NEBO SRDICKA DODELAT

    private float currentHealth;
    private int deathCount;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            //Debug.Log("player is dead");
            GameManager.instance.GoToMainMenu();
            //animace
            //vypnout
            deathCount++;
        }
        //dodelat isdead vraci bool funkce
    }
    public void LoadData(GameData data)
    {
        deathCount = data.deathCount;
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = deathCount;
    }
}
