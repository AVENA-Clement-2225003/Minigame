using UnityEngine.SceneManagement;  // To allow scene reloading
using UnityEngine;
using TMPro;

namespace BUT
{
    public class CollisionManager : MonoBehaviour
    {
        [SerializeField] private AudioClip audioCollecteCoin = null;
        [SerializeField] private AudioClip audioCollecteTrophee = null;
        [SerializeField] private AudioClip audioCollecteKey = null;
        [SerializeField] private float volume = 1.0f; // Ajout d'une variable pour le volume
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private PlayerMovement movementScript;
        [SerializeField] private TextMeshProUGUI counterText; // Reference to the TextMeshProUGUI component
        [SerializeField] private TextMeshProUGUI deathText;

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
                counterText.text = counter + "/6 coins"; // Update the text
                audioSource.PlayOneShot(audioCollecteCoin, volume); // Plays part collection sound
                Destroy(other.gameObject); // Destroy the prefab
            }
            else if (other.gameObject.name.Contains("trophee")) // Replace with your prefab's name
            {
                audioSource.PlayOneShot(audioCollecteTrophee, volume); // Plays part collection sound
                Destroy(other.gameObject); // Destroy the prefab
            }
            else if (other.gameObject.name.Contains("key")) // Replace with your prefab's name
            {
                audioSource.PlayOneShot(audioCollecteKey, volume); // Plays part collection sound
                Destroy(other.gameObject); // Destroy the prefab
            }
            else if (other.gameObject.name.Contains("Water")) // Replace with your prefab's name
            {
                //audioSource.PlayOneShot(audioCollecteKey, volume); // Plays part collection sound
                // Destroy the prefab
                deathText.text = "Vous �tes mort"; // Update the text
                if (movementScript != null)
                {
                    movementScript.enabled = false;
                }
                Invoke(nameof(Respawn), 3f);
            }
        }

        void Respawn()
        {
            Debug.Log("Respawning player!");

            // Reset player position
            transform.position = respawnPoint.position;
            deathText.text = "";
            // Re-enable the movement script
            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}