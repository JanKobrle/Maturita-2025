using UnityEngine;
using OD.Manager;
using OD.Dungeon;
using OD.Player;

namespace OD.Dungeon
{
    public class ColectableObjectShard : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("Player"))
            {
                GameManager.instance.AddShardsAndSave(1);
                Destroy(gameObject);
            }
        }
    }
}
