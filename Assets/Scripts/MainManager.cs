using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public string playerName;
    public string bestPlayer;
    public int score;
    public int bestScore;
    public Text bestScoreText;
    public InputField inputField;
    public GameObject BestScoreText;

    public static MainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }
    public void PlayerName()
    {
        playerName = inputField.text;
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string inputField;
        public string bestScoreText;
        public int bestScore;
        public string bestPlayer;
    }
    public void SaveBestScoreText()
    {
        SaveData data = new SaveData();
        data.bestScoreText = bestScoreText.text;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile3.json", json);
    }
    public void LoadBestScoreText()
    {
        string path = Application.persistentDataPath + "/savefile3.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreText.text = data.bestScoreText;
        }
    }
    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.inputField = inputField.text;
        data.bestScore = bestScore;
        data.bestPlayer = bestPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            inputField.text = data.playerName;
            bestScore = data.bestScore;
            bestPlayer = data.bestPlayer;
        }
    }
}
