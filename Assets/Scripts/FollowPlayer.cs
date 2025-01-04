using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // The player object to follow
    private Transform player;

    // Speed at which the object follows the player
    public float followSpeed = 5f;

    // Offset position (optional, if you want to maintain a fixed distance from the player)
    public Vector3 offset = new Vector3(0, 0);

    // Rigidbody for collision and physics interaction
    private Rigidbody rb;

    void Start()
    {
        // Find the player object by name and get its Transform
        player = GameObject.Find("Player").transform;

        // Get the Rigidbody component attached to the enemy
        rb = GetComponent<Rigidbody>();

        // Make sure Rigidbody is kinematic so we can manually control movement
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // Check if player is found
        if (player != null)
        {
            // Calculate the desired position to follow the player (with the offset)
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);

            // Move the object towards the player's position at the constant speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
