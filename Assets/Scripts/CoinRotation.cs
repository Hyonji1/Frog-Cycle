using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Adjust rotation speed as necessary

    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}