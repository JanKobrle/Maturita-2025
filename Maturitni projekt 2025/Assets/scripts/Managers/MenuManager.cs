using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }

    //[SerializeField] DataPersistenceManager DPManager;
    private int shards;

    private int damageLevel;
    private int healthLevel;
    //DODELAT pripadne attak speed

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

        damageLevel = PlayerPrefs.GetInt("DamageLevel");
        healthLevel = PlayerPrefs.GetInt("HealthLevel");

        shardsText.text = "Your shards: " + shards.ToString();

        damageLevelText.text = PlayerPrefs.GetInt("DamageLevel").ToString();
        healthLevelText.text = PlayerPrefs.GetInt("HealthLevel").ToString();

    }

    public void SpendShards(int amount)
    {
        shards -= amount;
        shardsText.text = "Your shards: " + shards.ToString();
        PlayerPrefs.SetInt("ShardAmmount", shards);
    }

    public void Play()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit(); //ted nefacha, ale as se exportuje, tak by melo
    }

    public void EnhanceWeaponDamage()
    {
        if (shards >= 5)
        {
            SpendShards(5);
            damageLevel++;
            PlayerPrefs.SetInt("DamageLevel", damageLevel);

            var temp = PlayerPrefs.GetInt("Damage");
            temp++; //muze mit slozitejsi vypocet eventuelne
            PlayerPrefs.SetInt("Damage", temp);
            damageLevelText.text = temp.ToString();
        }
    }
    public void EnhanceHealth()
    {
        if (shards >= 5)
        {
            SpendShards(5);
            healthLevel++;
            PlayerPrefs.SetInt("HealthLevel", healthLevel);

            var temp = PlayerPrefs.GetInt("MaxHealth");
            temp++; //just adds one hp
            PlayerPrefs.SetInt("MaxHealth", temp);
            healthLevelText.text = temp.ToString();
        }
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(1);

    }
}
