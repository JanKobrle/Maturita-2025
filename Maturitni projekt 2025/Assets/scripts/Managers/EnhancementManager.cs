using TMPro;
using UnityEngine;

namespace OD.Manager
{
    public class EnhancementManager : MonoBehaviour
    {
        private int shards;
        private int damage;
        private int maxHealth;

        [SerializeField] private TextMeshProUGUI shardsText;
        [SerializeField] private TextMeshProUGUI damageLevelText;
        [SerializeField] private TextMeshProUGUI healthLevelText;

        private void Start()
        {
            shards = PlayerPrefs.GetInt("ShardAmmount");
            shardsText.text = "Your shards: " + shards.ToString();

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
        //button functions:
        public void EnhanceWeaponDamage()
        {
            if (shards >= 5)
            {
                SpendShards(5);
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
                maxHealth++;
                PlayerPrefs.SetInt("MaxHealth", maxHealth);
                healthLevelText.text = maxHealth.ToString();
            }
        }

    }
}

