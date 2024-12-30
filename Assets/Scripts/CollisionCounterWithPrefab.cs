using UnityEngine;
using TMPro;

public class CollisionCounterTMP : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Reference to the TextMeshProUGUI component
    private int counter = 0; // Initialize the counter

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision detected with: {other.gameObject.name}"); // Debugging line

        if (other.gameObject.name.Contains("Gold")) // Replace with your prefab's name
        {
            counter++; // Increment the counter
            counterText.text = counter + "/6 coins" ; // Update the text
            Destroy(other.gameObject); // Destroy the prefab
        }
    }
}
