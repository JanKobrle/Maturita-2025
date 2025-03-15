using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance { get; private set; }   //singleton...

    [SerializeField] private int size;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject[] mapPrefabs;

    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); }
        else { instance = this; }
    }
    private void Start()
    {
        Instantiate(spawnPrefab, Vector3.zero, Quaternion.identity, transform);     //...

        foreach (Vector3 j in GenerateCoordinates(size))
        {
            Instantiate(DeterminePiece((int)j.y - 1), new Vector3(j.x * 50f, 0, j.z * 50f), Quaternion.identity, transform);
        }
        StartCoroutine(BuildNavMesh());
    }

    private IEnumerator BuildNavMesh()
    {
        yield return new WaitForEndOfFrame();
        GameObject.FindGameObjectWithTag("NavMeshSurface").GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public List<Vector3> GenerateCoordinates(int input)
    {
        List<Vector3> coordinates = new List<Vector3>();

        for (int i = 1; i <= input; i++)
        {
            for (int x = -i; x <= i; x++)        //i je vzdalenost od stredu
            {
                int y = i - Mathf.Abs(x);       //vypocet ycka
                if (y != 0)
                {
                    coordinates.Add(new Vector3(x, i, y));
                    coordinates.Add(new Vector3(x, i, -y));
                }
                else
                {
                    coordinates.Add(new Vector3(x, i, y));
                }
            }
        }

        return coordinates;
    }

    private GameObject DeterminePiece(int distanceFromSpawn)   //0 - jedna distance od spawu, 1 - dva distance od spawnu ...
    {
        List<float> probList = new List<float>();
        foreach (GameObject i in mapPrefabs)
        {
            if (distanceFromSpawn >= i.GetComponent<DungeonMapPrefabStats>().probability.Length) { probList.Add(0f); }
            else { probList.Add(i.GetComponent<DungeonMapPrefabStats>().probability[distanceFromSpawn]); }
        }

        List<float> cumulativeProb = new List<float> { probList[0] };    
        for (int i = 1; i < probList.Count; i++)
        {
            cumulativeProb.Add(cumulativeProb[i - 1] + probList[i]);
        }
        float totalSum = cumulativeProb.Last();
        float randomValue = Random.Range(0f, totalSum);
        for (int i = 0; i < cumulativeProb.Count; i++)
        {
            if (randomValue < cumulativeProb[i])
            {
                return mapPrefabs[i];
            }
        }

        return mapPrefabs.Last();   //kdyz maj vsechny roomky 0 prop tak to tam hodi tu posledni
        //return spawnPrefab;   //pro testovani

    }
}
