using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    public int bestScore;
    
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.BestScoreText.SetActive(false);
        CheckScore();
        MainManager.Instance.SaveName();
        LoadScore();
        MainManager.Instance.bestScore = bestScore;
        bestScoreText.text = $"Best Score : {MainManager.Instance.bestPlayer} : {bestScore} ";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        MainManager.Instance.score = m_Points;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        MainManager.Instance.SaveName();
        m_GameOver = true;
        CheckScore();
        SaveScore();
        MainManager.Instance.bestScoreText.text = bestScoreText.text;
        MainManager.Instance.SaveBestScoreText();
        GameOverText.SetActive(true);
    }
    public void CheckScore()
    {
        int score = MainManager.Instance.score;

        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestPlayer = MainManager.Instance.playerName;
            bestScore = score;
            bestScoreText.text = $"Best Score : {MainManager.Instance.bestPlayer} : {bestScore}";
        }
    }
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile2.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
        }
    }
}
