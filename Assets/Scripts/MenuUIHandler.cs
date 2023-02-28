using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    
    public InputField inputField;
    public Text bestScoreText;
    public string scoreText;


    // Start is called before the first frame update
    void Start()
    {
        MainManager.Instance.LoadName();
        bestScoreText.text = $"Best Score : {MainManager.Instance.bestPlayer} : {MainManager.Instance.bestScore}";
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        MainManager.Instance.SaveName();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
