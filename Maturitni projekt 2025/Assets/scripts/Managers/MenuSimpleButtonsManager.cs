using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OD.Manager
{
    public class MenuSimpleButtonsManager : MonoBehaviour
    {
        public static MenuSimpleButtonsManager instance { get; private set; }                                   //[19]

        [SerializeField] private TextMeshProUGUI shardsText;
        [SerializeField] private TextMeshProUGUI damageLevelText;
        [SerializeField] private TextMeshProUGUI healthLevelText;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        public void Play()
        {
            SceneManager.LoadSceneAsync(1);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void ResetDataAndPlay()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadSceneAsync(1);
        }
    }
}
