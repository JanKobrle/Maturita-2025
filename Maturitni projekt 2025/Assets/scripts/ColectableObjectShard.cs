using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableObjectShard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("still nothing");
        if (collision.collider.gameObject.CompareTag("Player"))
        {
          collision.collider.gameObject.GetComponent<ShardCounter>().shardCount++;
          //Debug.Log("item colected");
          Destroy(gameObject);
        }
    }
}
