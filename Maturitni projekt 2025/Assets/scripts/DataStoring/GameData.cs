using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int shardCount;
    public int deathCount;

    public GameData()
    {
        shardCount = 0;             //defaultni nastaveni
        deathCount = 0;
    }
}
