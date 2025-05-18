using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;


namespace OD.Dungeon
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator instance { get; private set; }                //[19]

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
            Instantiate(spawnPrefab, Vector3.zero, Quaternion.identity, transform);            //jako prvni se udìlá spawn

            foreach (Vector3 j in GenerateCoordinates(size))                                                             //pro každou vygenerovanou souøadnici vybere a vytvoøí chunk
            {
                Instantiate(DeterminePiece((int)j.y - 1), new Vector3(j.x * 50f, 0, j.z * 50f), Quaternion.identity, transform);   //souøadnice chunkù se násobí pevnì danou hodnotou (jejich hranou)
            }
            StartCoroutine(BuildNavMesh());                                                        // vytvoøí NavMesh na celé mapì
        }

        private IEnumerator BuildNavMesh()                                    //jakmile je všechno vygenerované, vytvoøí se NavMesh na právì utvoøených chuncích
        {
            yield return new WaitForEndOfFrame();
            GameObject.FindGameObjectWithTag("NavMeshSurface")
            .GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        public List<Vector3> GenerateCoordinates(int input)                                        // vygeneruje seznam souøadnic v celých èíslech na základì velikosti mapy (poètu chunkù)
        {
            List<Vector3> coordinates = new List<Vector3>();
            //tento výpoèet navrhnul ChatGPT
            for (int i = 1; i <= input; i++)                        // podle chtìné velikosti mapy zaène v nebližším prstenci generovat souøadnice 
            {
                for (int x = -i; x <= i; x++)                   // v každém prstenci se zopakuje podle poètu chunkù 
                {
                    int y = i - Mathf.Abs(x);
                    if (y != 0)                                                          //pro každý chunk vrátí jeho souøadnice (oba listy mají stejné indexy)
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
            //zde konèí sekce, ktero mi pomohl navrhnout ChatGPT
            return coordinates;
        }

        private GameObject DeterminePiece(int distanceFromSpawn)                                                                 //0 - jedna distance od spawu, 1 - dva distance od spawnu ...
        {
            List<float> probList = new List<float>();
            foreach (GameObject i in mapPrefabs)                                   // vytvoøí seznam pravdìpodobností, že se daný chunk vytvoøí na tdané vzdálenosti od støedu (indexi v obou seznamech jsou stejné)
            {
                if (distanceFromSpawn >= i.GetComponent<DungeonMapPrefabStats>().probability.Length) { probList.Add(0f); }                             //ošetøení výjmky, pokud je mimo meze tak se nastaví, jako 0   
                else { probList.Add(i.GetComponent<DungeonMapPrefabStats>().probability[distanceFromSpawn]); }                    // pøidá hodnotu, do seznamu
            }

            List<float> cumulativeProb = new List<float> { probList[0] };                          //seznam kumulativních pravdìpodobností
            for (int i = 1; i < probList.Count; i++)
            {
                cumulativeProb.Add(cumulativeProb[i - 1] + probList[i]);                        // vytvoøí seznam, kdy každá další pozice se rovná souètu s bezprostøednì pøedchozí pozicí 1,2,3,4... 1,3,6,10...
            }
            float totalSum = cumulativeProb.Last();                                          // celkový souèet, resp poslední pozice
            float randomValue = Random.Range(0f, totalSum);                            // vybere náhodnou hodnotu mezi 0 a celkovým souètem
            for (int i = 0; i < cumulativeProb.Count; i++)                   // postupnì porovná náhodnou hodnotu s hodnotami v cumulativeProb[i] dokud nenajde odpovídající index
            {
                if (randomValue < cumulativeProb[i])
                {
                    return mapPrefabs[i];                    // vrátí korespondující index prefabu
                }
            }

            return mapPrefabs.Last();                      //kdyz maj vsechny pref. 0 prob. tak to tam posle tu posledni, aby to kdyžtak vyšlo

        }
    }
}
