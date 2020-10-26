using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerController player;
    public GameState gameState;
    public float score = 0;
    public float highScore = 0;
    public int asteroidPassed = 0;
    public float seconds = 0;
    public int multiplyScore;

    [SerializeField] UIViewer uiViewer;

  
    private void Awake()
    {
       
        if (instance == null) instance = this;
    }
    private void Start()
    {
        highScore = GetPrefs("HighScore");
    }
    private void Update()
    {
        if (gameState == GameState.Start)
        {
            if (Input.anyKey)
            {
                EventManager.StartGame();
            }
        }
        if (gameState == GameState.Play)
        {
            score += Time.deltaTime* multiplyScore;
            if(score>highScore)
            {
                highScore = score;
            }
            seconds += Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        EventManager.OnEndGame += GameOver;
    }
    private void OnDisable()
    {
        EventManager.OnEndGame += GameOver;
    }
    public float GetPrefs(string name)
    {
        return PlayerPrefs.GetFloat(name, 0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void GameOver()
    {
        SaveNewPrefs(score, "HighScore");
        gameState = GameState.End;
    }
    private void SaveNewPrefs(float value, string name)
    {
        if (value > PlayerPrefs.GetFloat(name, 0))
        {
            PlayerPrefs.SetFloat(name, value);
            highScore = score;

        }
    }
   
}
[Serializable]
public enum GameState
{
    Start,
    Play,
    End
}
