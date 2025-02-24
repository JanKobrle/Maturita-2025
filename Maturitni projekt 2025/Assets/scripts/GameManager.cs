using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); }
        else { instance = this; }
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
