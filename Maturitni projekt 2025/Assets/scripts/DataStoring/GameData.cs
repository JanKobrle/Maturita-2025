using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int shardCount;
    public int deathCount;
    public float maxHealth;
    //public float WeaponDamage;
    //public float WeaponAttackSpeed;


    public GameData()
    {
        shardCount = 0;             //defaultni nastaveni
        deathCount = 0;
        maxHealth = 1;
        //WeaponAttackSpeed = 1
        //WeaponDamage = 1
    }
}
