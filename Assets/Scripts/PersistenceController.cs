using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PersistenceController : Singleton<PersistenceController>
{
    private static string DataFilePath
    {
        get { return Application.persistentDataPath + "/save.dat"; }
    }

    public static void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if ((File.Exists(DataFilePath)))
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Create);
            PersistentData persistentData = new PersistentData();
            persistentData.Currency = CurrencyController.Instance.Currency;
            persistentData.HighScore = ScoreController.Instance.HighScore;
            persistentData.MaxTimeSurvived = ScoreController.Instance.MaxTimeSurvived;
            persistentData.MissilesDestroyed = ScoreController.Instance.MissilesDestroyed;
            //persistentData.CharacterSelectionIndex = CharacterSelection.Instance.SelectionIndex;
            binaryFormatter.Serialize(fileStream, persistentData);
            Debug.Log("Data saved.");
            Debug.Log(DataFilePath);
            fileStream.Close();
        }
        else
        {
            FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat");
            PersistentData persistentData = new PersistentData();
            binaryFormatter.Serialize(fileStream, persistentData);
            fileStream.Close();
            Debug.Log(DataFilePath);
        }
    }

    public static void Load()
    {
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (File.Exists(DataFilePath))
            {
                FileStream fileStream = File.Open(DataFilePath, FileMode.Open);
                PersistentData saveData = (PersistentData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();

                CurrencyController.Instance.Currency = saveData.Currency;
                ScoreController.Instance.HighScore = saveData.HighScore;
                ScoreController.Instance.MaxTimeSurvived = saveData.MaxTimeSurvived;

                Debug.Log("Currency loaded: " + saveData.Currency.ToString());
                Debug.Log("Highscore loaded: " + saveData.HighScore.ToString() + "\n" + "TimeSurvived loaded: " + saveData.MaxTimeSurvived.ToString());
                Debug.Log(DataFilePath);
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    public static void Delete()
    {
        try
        {
            File.Delete(DataFilePath);
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
        }

    }

    [Serializable]
    public class PersistentData
    {
        public Character[] Characters = null;
        public int CharacterSelectionIndex = 0;

        public int Currency = 0;

        public int HighScore = 0;
        public int MissilesDestroyed = 0;
        public float MaxTimeSurvived = 0;
    }
}
