using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShardCounter : MonoBehaviour, IDataPersistence
{
    public int shardCount;
    [SerializeField] private TextMeshProUGUI shardCounter;
    void Update()
    {
        shardCounter.text = $"Shards colected: {shardCount}";
    }
    public void LoadData(GameData data)
    {
        shardCount = data.shardCount;
    }
    public void SaveData(ref GameData data)
    {
        data.shardCount = shardCount;
    }
}
