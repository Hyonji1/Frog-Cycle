using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public static int coinsCollected = 0;
    public static int highScore = 0; 
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highScoreText; 
    public GameObject groundPlatform;
    public LayerMask obstacleLayer; // Layer for obstacles
    public LayerMask coinLayer; // Layer for coins
    public float respawnMargin = 3f;
    public float minDistanceBetweenCoins = 2.0f; // Minimum distance between coins

    private void Start()
    {
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
            float margin = respawnMargin;
            Vector3 newPosition;

            int maxAttempts = 100; // Limit to prevent infinite loop
            int attempts = 0;

            do
            {
                float randomX = Random.Range(-groundSize.x / 2 + margin, groundSize.x / 2 - margin);
                float randomZ = Random.Range(-groundSize.z / 2 + margin, groundSize.z / 2 - margin);
                newPosition = new Vector3(randomX, transform.position.y, randomZ);

                Collider[] obstacles = Physics.OverlapSphere(newPosition, 0.5f, obstacleLayer);
                Collider[] otherCoins = Physics.OverlapSphere(newPosition, minDistanceBetweenCoins, coinLayer);

                if (obstacles.Length == 0 && otherCoins.Length == 0)
                {
                    transform.position = newPosition;
                    break;
                }

                attempts++;
            } while (attempts < maxAttempts);

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Unable to find a valid position for the coin after multiple attempts.");
            }
        }
    }
}