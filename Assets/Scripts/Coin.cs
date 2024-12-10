using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public static int coinsCollected = 0;
    public static int highScore = 0; // Static variable to track high score
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highScoreText; // Reference to display high score
    public GameObject groundPlatform;

    private void Start()
    {
        // Display initial coin count and high score
        coinText.text = "Coins: " + coinsCollected.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinsCollected++;
            coinText.text = "Coins: " + coinsCollected.ToString();

            if (coinsCollected > highScore)
            {
                highScore = coinsCollected;
                highScoreText.text = "High Score: " + highScore.ToString();
            }

            Debug.Log("Coins Collected: " + coinsCollected);
            RespawnCoin();
        }
    }

    private void RespawnCoin()
    {
        if (groundPlatform != null)
        {
            Vector3 groundSize = groundPlatform.GetComponent<Collider>().bounds.size;
            float margin = 3f;
            float randomX = Random.Range(-groundSize.x / 2 + margin, groundSize.x / 2 - margin);
            float randomZ = Random.Range(-groundSize.z / 2 + margin, groundSize.z / 2 - margin);
            transform.position = new Vector3(randomX, transform.position.y, randomZ);
        }
    }
}