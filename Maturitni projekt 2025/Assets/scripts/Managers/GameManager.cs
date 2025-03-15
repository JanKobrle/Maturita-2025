using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }   //singleton

    public int shardAmmount = 0;

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
        shardAmmount += ammount;
    }


}
