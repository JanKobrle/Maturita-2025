using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    [SerializeField] ShardCounter SCounter;

    [SerializeField] private float maxHealth;
    // HEALTHBAR NEBO SRDICKA DODELAT

    private float currentHealth;
    private int deathCount;
    [SerializeField] DataPresistenceManager DPManager;

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
            SCounter.shardCount = 0;
            deathCount++;
            DPManager.SaveGame();
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
