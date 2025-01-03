using UnityEngine;
using TMPro;

public class CollisionCounterTMP : MonoBehaviour
{
    [SerializeField] private AudioClip audioCollecteCoin = null;
    [SerializeField] private float volume = 1.0f; // Ajout d'une variable pour le volume

    public TextMeshProUGUI counterText; // Reference to the TextMeshProUGUI component
    private int counter = 0; // Initialize the counter
    private AudioSource audioSource; // Init AudioSource

    void Start()
    {
        // Attach an AudioSource component if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = volume; // Initialize sound volume
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision detected with: {other.gameObject.name}"); // Debugging line

        if (other.gameObject.name.Contains("Gold")) // Replace with your prefab's name
        {
            counter++; // Increment the counter
            counterText.text = counter + "/6 coins" ; // Update the text
            audioSource.PlayOneShot(audioCollecteCoin, volume); // Plays part collection sound
            Destroy(other.gameObject); // Destroy the prefab
        }
    }
}