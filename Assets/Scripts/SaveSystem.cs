using UnityEngine;
using System.IO;

public class SaveSystem 
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public GameSaveData GameData;
        public CardManagerSaveData CardManagerData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }
    private static void HandleSaveData()
    {
        GameManager.instance.Save(ref _saveData.GameData);
        GameManager.instance.cardManager.Save(ref _saveData.CardManagerData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GameManager.instance.Load(_saveData.GameData);
        GameManager.instance.cardManager.Load(_saveData.CardManagerData);
    }
}