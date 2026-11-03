using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private const string path = "saveData.json";

    public void SaveGame()
    {
        SaveData data = new();
        data.gold = GoldManager.Instance.Gold;
        data.hp = HPManager.Instance.HP;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public void LoadGame()
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GoldManager.Instance.SetGold(data.gold);
        HPManager.Instance.SetHP(data.hp);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
