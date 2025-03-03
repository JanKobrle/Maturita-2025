//using System.Diagnostics;
using System;
using System.IO;
using UnityEngine;


public class FileDataHandeler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandeler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //nacte serializovana data ze souboru
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //deserializuje data z Jsonu
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("error when loading data ffrom file:" + fullPath + e);
            }
            
        }
      return loadedData;
    }   
        public void Save(GameData data)
        {
            //string fullPath = dataDirPath + "/" + dataFileName; podelava se na mobilu
            string fullPath = Path.Combine(dataDirPath, dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); //dela novy soubor kdyz neni

                string dataToStore = JsonUtility.ToJson(data, true); //serializace C# dat pro Json

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore); //zapise do souboru
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("error:" + e);
            }
        }
   
}
