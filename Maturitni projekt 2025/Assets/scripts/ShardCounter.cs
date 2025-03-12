using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShardCounter : MonoBehaviour, IDataPersistence
{
    public static ShardCounter instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.LogError("found more than one ShardCounter in the scene");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
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