using UnityEngine;

namespace OD.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [field: SerializeField] public float damage { get; private set; }
        [field: SerializeField] public float range { get; private set; }
        [field: SerializeField] public float angle { get; private set; }
        [field: SerializeField] public float duration { get; private set; } //mus� ladit s animac�
        private void Start()
        {
            damage += PlayerPrefs.GetInt("Damage");
        }
    }
}