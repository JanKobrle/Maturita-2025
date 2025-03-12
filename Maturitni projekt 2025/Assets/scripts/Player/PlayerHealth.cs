using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public static PlayerHealth instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("found more than one PlayerHealth in the scene");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    [SerializeField] public float maxHealth;
    // HEALTHBAR NEBO SRDICKA DODELAT

    private float currentHealth;
    private int deathCount;

    //[SerializeField] DataPresistenceManager DPManager;

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
            ShardCounter.instance.shardCount = 0;
            deathCount++;
            DataPresistenceManager.instance.SaveGame();
        }
        //dodelat isdead vraci bool funkce
    }
    public void LoadData(GameData data)
    {
        deathCount = data.deathCount;
        maxHealth = data.maxHealth;
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = deathCount;
        data.maxHealth = maxHealth;
    }

    public void EnhancePlayerMaxHealthBy(int i)
    {
        maxHealth = maxHealth + i;
        currentHealth = maxHealth;
        Debug.Log($"EnhancePlayerMaxHealthBy({i}) was called");
    }
}
