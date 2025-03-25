using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance { get; private set; }   //singleton, d� se na to hezky odkazovat, je v�dy jen jeden...

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
        Instantiate(spawnPrefab, Vector3.zero, Quaternion.identity, transform);     //jako prvni se ud�l� spawn

        foreach (Vector3 j in GenerateCoordinates(size)) //pro ka�dou vygenerovanou sou�adnici vybere a vytvo�� chunk
        {
            Instantiate(DeterminePiece((int)j.y - 1), new Vector3(j.x * 50f, 0, j.z * 50f), Quaternion.identity, transform);//sou�adnice chunk� se n�sob� pevn� danou hodnotou (jejich hranou)
        }
        StartCoroutine(BuildNavMesh());// vytvo�� NavMesh na cel� map�
    }

    private IEnumerator BuildNavMesh() //jakmile je v�echno vygenerovan�, vytvo�� se NavMesh na pr�v� utvo�en�ch chunc�ch
    {
        yield return new WaitForEndOfFrame();
        GameObject.FindGameObjectWithTag("NavMeshSurface").GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public List<Vector3> GenerateCoordinates(int input) // vygeneruje seznam sou�dnic v cel�ch ��slech na z�klad� velikosti mapy (po�tu chunk�)
    {
        List<Vector3> coordinates = new List<Vector3>();

        for (int i = 1; i <= input; i++) // podle cht�n� velikosti mapy za�ne v nebli���m prstenci generovat sou�adnice 
        {
            for (int x = -i; x <= i; x++) // v ka�d�m prstenci se zopakuje podle po�tu chunk� 
            {
                int y = i - Mathf.Abs(x);      
                if (y != 0)               //pro ka�d� chunk vr�t� jeho sou�adnice
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
        foreach (GameObject i in mapPrefabs)// vytvo�� seznam pravd�podobnost�, �e se dan� chunk vytvo�� na tdan� vzd�lenosti od st�edu (indexi v obou seznamech jsou stejn�)
        {
            if (distanceFromSpawn >= i.GetComponent<DungeonMapPrefabStats>().probability.Length) { probList.Add(0f); } //o�et�en� v�jmky, pokud je mimo meze tak se nastav�, jako 0   
            else { probList.Add(i.GetComponent<DungeonMapPrefabStats>().probability[distanceFromSpawn]); } // p�id� hodnotu, do seznamu
        }

        List<float> cumulativeProb = new List<float> { probList[0] }; //seznam pravd�podobnostn�ch index�
        for (int i = 1; i < probList.Count; i++)
        {
            cumulativeProb.Add(cumulativeProb[i - 1] + probList[i]); // vytvo�� seznam, kdy ka�d� dal�� pozice se rovn� sou�tu s bezprost�edn� p�edchoz� pozic� 1,2,3,4... 1,3,6,10...
        }
        float totalSum = cumulativeProb.Last(); // celkov� sou�et, resp posledn� pozice
        float randomValue = Random.Range(0f, totalSum); // vybere n�hodnou hodnotu mezi 0 a max
        for (int i = 0; i < cumulativeProb.Count; i++) // postupn� porovn� n�hodnou hodnotu s hodnotami v cumulativeProb[i] dokud nenajde odpov�daj�c� index
        {
            if (randomValue < cumulativeProb[i])
            {
                return mapPrefabs[i];// vr�t� koresponduj�c� index prefabu
            }
        }

        return mapPrefabs.Last();   //kdyz maj vsechny roomky 0 prop tak to tam hodi tu posledni, aby to kdy�tak vy�lo
        //return spawnPrefab;   //pro testovani

    }
}
