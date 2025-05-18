using UnityEngine;
using UnityEngine.SceneManagement;


namespace OD.Manager
{


    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }   //[19]

        public int shardAmount = 0;
        [SerializeField] private Canvas PauseCanvas;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.Log("found more than one GameManager in the scene");
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

        }

        public void AddShardsAndSave(int ammount)
        {
            shardAmount += ammount;
        }

        public void PauseMenuPauseClick()
        {
            PauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        public void PauseMenuContinueClick()
        {
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        public void PauseMenuQuitClick()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

    }

}