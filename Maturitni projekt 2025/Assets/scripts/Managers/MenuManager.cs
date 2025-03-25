using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }

    private int shards;
    //[SerializeField] private int shards; na testování
    private int damage;
    private int maxHealth;
    //private int damageLevel;
    //private int healthLevel;

    [SerializeField] private TextMeshProUGUI shardsText;

    [SerializeField] private TextMeshProUGUI damageLevelText;
    [SerializeField] private TextMeshProUGUI healthLevelText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // znici impostory
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        shards = PlayerPrefs.GetInt("ShardAmmount");

        //damageLevel = PlayerPrefs.GetInt("DamageLevel");
        //healthLevel = PlayerPrefs.GetInt("HealthLevel");

        shardsText.text = "Your shards: " + shards.ToString();

        // if (PlayerPrefs.GetInt("DamageLevel") <= 0) { PlayerPrefs.SetInt("DamageLevel", 1); } //to aby ten level nemohl b7t 0
        // if (PlayerPrefs.GetInt("HealthLevel") <= 0) { PlayerPrefs.SetInt("HealthLevel", 1); }

        //damageLevelText.text = PlayerPrefs.GetInt("DamageLevel").ToString();
        //healthLevelText.text = PlayerPrefs.GetInt("HealthLevel").ToString();
        damage = PlayerPrefs.GetInt("Damage");
        damageLevelText.text = damage.ToString();

        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        healthLevelText.text = maxHealth.ToString();
    }

    public void SpendShards(int amount)
    {
        shards -= amount;
        shardsText.text = "Your shards: " + shards.ToString();
        PlayerPrefs.SetInt("ShardAmmount", shards);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }


    public void Quit()
    {
        Application.Quit(); //funguje as se exportuje
    }

    public void EnhanceWeaponDamage()
    {
        if (shards >= 5)
        {
            SpendShards(5);
            //damageLevel++;
            //PlayerPrefs.SetInt("DamageLevel", damageLevel);
            //damageLevelText.text = damageLevel.ToString();
            //var temp = PlayerPrefs.GetInt("Damage");
            //temp++; //muze mit slozitejsi vypocet eventuelne
            //PlayerPrefs.SetInt("Damage", temp);
            //damageLevelText.text = temp.ToString();

            damage++;
            PlayerPrefs.SetInt("Damage", damage);
            damageLevelText.text = damage.ToString();
        }
    }
    public void EnhanceHealth()
    {
        if (shards >= 5)
        {
            SpendShards(5);
            //healthLevel++;
            //PlayerPrefs.SetInt("HealthLevel", healthLevel);
            //var temp = PlayerPrefs.GetInt("MaxHealth");
            //temp++; //prida jen jeden, ale mohlo by byt sloziejsi
            //PlayerPrefs.SetInt("MaxHealth", temp);
            //healthLevelText.text = temp.ToString();

            maxHealth++;
            PlayerPrefs.SetInt("MaxHealth", maxHealth);
            healthLevelText.text = maxHealth.ToString();
        }
    }
    public void ResetDataAndPlay()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(1);
    }
}
