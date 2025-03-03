using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;


public class DataPresistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    private GameData gameData;
    public static DataPresistenceManager instance { get; private set; }
    private List<IDataPersistence> dataPresistanceObjects;

    private FileDataHandeler dataHandeler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("found more than one DataManager in the scene");
        }
    }

    private void Start()
    {
        dataHandeler = new FileDataHandeler(Application.persistentDataPath, fileName);
        //Debug.Log("aspon start funguje");
        dataPresistanceObjects = FindAlldataPresistancesObjects();
        LoadGame();
    }

    public void NewGame()
    {
        //Debug.Log("1 new game");
        gameData = new GameData();
    }
    public void LoadGame()
    {

        gameData = dataHandeler.Load();//nacte jakykoliv data ze souboru pomoci data handleru
        if (gameData == null) //kdyz nejsou data loudne to novou hru
        {
            //Debug.Log("No data found - loading default");
            NewGame();
        }
        foreach (IDataPersistence dataPresistanceObj in dataPresistanceObjects) // nalouduje data zhandleru
        {
            dataPresistanceObj.LoadData(gameData);
            //Debug.Log("2 load game foreach");
        }
    }
    public void SaveGame()
    {
        Debug.Log($"Found {dataPresistanceObjects.Count} persistence objects.");
        foreach (IDataPersistence dataPresistanceObj in dataPresistanceObjects)
        {
            dataPresistanceObj.SaveData(ref gameData);
        }
        Debug.Log($"saved shards: {gameData.shardCount}");
        Debug.Log($"saved shards: {gameData.deathCount}");

        dataHandeler.Save(gameData); //ulozi data do souboru pomoci datahandleru

    }

    private void OnApplicationQuit()
    {
        SaveGame(); //nefunguje, ale melo by po exportu
    }
    private List<IDataPersistence> FindAlldataPresistancesObjects()
    {
        //Debug.Log("4 idata presistance");

        IEnumerable<IDataPersistence> dataPresistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPresistanceObjects);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveGame();
        }
    }

}
