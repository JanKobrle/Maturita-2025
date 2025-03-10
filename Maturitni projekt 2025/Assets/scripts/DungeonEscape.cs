using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEscape : MonoBehaviour
{
    [SerializeField] DataPresistenceManager DPManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenu");
            DPManager.SaveGame();
        }
    }
}
