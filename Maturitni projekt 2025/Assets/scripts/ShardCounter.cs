using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShardCounter : MonoBehaviour
{
    public int shardCount;
    [SerializeField] private TextMeshProUGUI shardCounter;
    void Update()
    {
        shardCounter.text = $"Shards colected: {shardCount}";
    }
    
}
