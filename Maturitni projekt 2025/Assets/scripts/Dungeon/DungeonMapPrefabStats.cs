using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OD.Dungeon
{
    public class DungeonMapPrefabStats : MonoBehaviour
    {
        [field: SerializeField] public float[] probability { get; private set; }
    }
}

