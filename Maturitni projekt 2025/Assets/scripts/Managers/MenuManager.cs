using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public static MenuManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // znici impostory
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // neznici se to pri prechodu

    }
    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
        DataPresistenceManager.instance.SaveGame();
    }

    public void Quit()
    {
        Application.Quit(); //ted nefacha, ale as se exportuje, tak by melo
    }

    public void EnhanceWeaponDamage()
    {

    }//DODELAT
    public void EnhanceWeaponAttackSpeed()
    {

    }//DODELAT
    public void EnhanceHealth()
    {
        //PlayerHealth.instance.EnhancePlayerMaxHealthBy(1);
        //healthIncrement++;
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.EnhancePlayerMaxHealthBy(1);
    }
}
    //private int healthIncrement;
    //private void Awake()
    //{
    //    //if (instance != null && instance != this)
    //    //{
    //    //    Destroy(gameObject);
    //    //    Debug.LogError("found more than one MenuManager in the scene");
    //    //}
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("I found more than one MenuManager in the scene so I am killing myself");
    //    }
    //    if(instance == null) 
    //    {
    //         instance = this;
    //    }
       
    //    DontDestroyOnLoad(gameObject);
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Scene loaded: " + scene.name);
    //    PlayerHealth.instance.EnhancePlayerMaxHealthBy(healthIncrement);
    //    //DODELAT WEAPON DMG a ATSP
    //    if (SceneManager.GetSceneByName("SampleScene") == scene)
    //    {
    //      Destroy(gameObject);
    //    }

    //}
    //private void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}