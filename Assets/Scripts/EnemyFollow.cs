using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float initialSpeed = 1.0f; // Starting speed of the enemy
    public float maxSpeed = 5.0f; // Maximum speed the enemy can reach
    public float speedIncreaseRate = 0.1f; // Rate at which the speed increases
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverHighScoreText; // Reference to display high score on Game Over Panel
    public TextMeshProUGUI gameOverScoreText;

    private float currentSpeed;
    private float obstacleAvoidanceDistance = 1.0f; // Distance to start avoiding obstacles
    private float turnSpeed = 10.0f; // Snappier turn speed
    
    public TextMeshProUGUI gameOverMessageText;
    private bool isGameOver = false;

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        if (!isGameOver && player != null)
        {
            // Gradually increase speed over time
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += speedIncreaseRate * Time.deltaTime;
            }

            Vector3 direction = (player.position - transform.position).normalized;

            // Check for obstacles and adjust direction
            if (Physics.Raycast(transform.position, transform.forward, obstacleAvoidanceDistance))
            {
                // Rotate the direction slightly to the right to avoid the obstacle
                direction = Quaternion.Euler(0, 45, 0) * direction;
            }

            // Move the enemy
            transform.position += direction * currentSpeed * Time.deltaTime;

            // Make the enemy face the player with snappier turn rate
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isGameOver && collision.gameObject.CompareTag("Player"))
        {
            isGameOver = true; // Set the game over flag
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            gameOverHighScoreText.text = "High Score: " + Coin.highScore.ToString();
            gameOverScoreText.text = "Score: " + Coin.coinsCollected.ToString();
            GameObject inGameUI = GameObject.Find("InGameUI");
            if (inGameUI != null)
            {
                inGameUI.SetActive(false);
            }

            UpdateGameOverMessage("The worm got you!");
        }
    }

    void UpdateGameOverMessage(string message)
    {
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = message;
        }
    }

    public void RestartGame()
    {
        Coin.coinsCollected = 0; // Reset coins collected
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}