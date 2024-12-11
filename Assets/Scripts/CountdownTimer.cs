using UnityEngine;
using UnityEngine.UI; // If using TextMeshPro, use TMPro;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // If using TextMeshPro, use TMP_Text
    public GameObject gameOverPanel;
    private float timeRemaining = 60.0f;
    private bool isTimerRunning = true;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;
    public TextMeshProUGUI gameOverMessageText;
    private bool isGameOver = false;
    
    void Start()
    {
        gameOverPanel.SetActive(false); // Initially hide GameOverPanel
    }

    void Update()
    {
        if (!isGameOver && isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateGameOverMessage(string message)
    {
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = message;
        }
    }

    void OnTimerEnd()
    {
        isGameOver = true; // Set the game over flag
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Score: " + Coin.coinsCollected.ToString();
        gameOverHighScoreText.text = "High Score: " + Coin.highScore.ToString(); // Display high score
        GameObject inGameUI = GameObject.Find("InGameUI");
        if (inGameUI != null)
        {
            inGameUI.SetActive(false);
        }
        Time.timeScale = 0f;
        UpdateGameOverMessage("You ran out of time!");
    }
}