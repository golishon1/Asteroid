using UnityEngine;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    [SerializeField] private Text startText;
    [SerializeField] private Text newRecordText;

    [SerializeField] private GameObject gameStatsPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        EventManager.OnEndGame += GameOverPanelShow;
        EventManager.OnStartGame += StartGame;
    }
    private void OnDisable()
    {
        EventManager.OnEndGame -= GameOverPanelShow;
        EventManager.OnStartGame -= StartGame;
    }

    private void StartGame()
    {
        startText.text = "";
        gameStatsPanel.SetActive(true);
    }
    private void GameOverPanelShow()
    {
        gameStatsPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        if(GameController.instance.score< GameController.instance.highScore)
        {
            newRecordText.text = "";
        }
    }

}
