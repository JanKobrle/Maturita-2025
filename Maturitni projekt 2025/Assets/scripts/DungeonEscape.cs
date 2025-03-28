using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
