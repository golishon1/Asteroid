using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsView : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text asteroidPassedText;
    [SerializeField] private Text secondsText;
    private TimeSpan timeSpan;

    private void Update()
    {
        scoreText.text = $"Score: {(int) GameController.instance.score}";
        timeSpan = TimeSpan.FromSeconds(GameController.instance.seconds);
        secondsText.text = $"Time {timeSpan:mm\\:ss}";
        asteroidPassedText.text = $"Asteroid passed {GameController.instance.asteroidPassed}";
        highScoreText.text = $"High score {(int) GameController.instance.highScore}";
    }
}