using UnityEngine;
using UnityEngine.SceneManagement;
using OD.Manager;

namespace OD.Dungeon
{
    public class DungeonEscape : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("Player"))
            {
                PlayerPrefs.SetInt("ShardAmmount", PlayerPrefs.GetInt("ShardAmmount") + GameManager.instance.shardAmount);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}