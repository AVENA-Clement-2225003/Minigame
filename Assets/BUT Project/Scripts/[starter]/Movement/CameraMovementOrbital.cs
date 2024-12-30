using UnityEngine;
using UnityEngine.InputSystem;

namespace BUT
{
    /* Handle Camera's rotation */
    public class CameraMovementOrbital : MonoBehaviour
    {
        public Transform player;        // Reference to the player's transform
        public float distance = 5f;     // Initial distance from the player
        public float rotationSpeed = 5f; // Speed at which the camera rotates around the player
        public float height = 2f;       // Height of the camera above the player
        public float zoomSpeed = 2f;    // Speed of zooming (scrolling)
        public float minDistance = 2f;  // Minimum zoom distance
        public float maxDistance = 10f; // Maximum zoom distance
        public float verticalSpeed = 2f; // Speed at which the camera looks up and down
        public float maxVerticalAngle = 80f; // Max angle for looking up/down
        public float minVerticalAngle = -80f; // Min angle for looking up/down

        private float currentRotation = 0f;  // Horizontal rotation around the player
        private float currentPitch = 0f;     // Vertical angle to look up/down

        void LateUpdate()
        {
            // Zoom in and out with mouse scroll
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            distance -= scrollInput * zoomSpeed; // Change the distance based on scroll input
            distance = Mathf.Clamp(distance, minDistance, maxDistance); // Clamp the distance within min and max limits

            // Get the mouse input to rotate the camera horizontally
            float horizontalInput = Input.GetAxis("Mouse X");
            currentRotation += horizontalInput * rotationSpeed; // Rotate horizontally around the player

            // Get the mouse input to rotate the camera vertically (look up/down)
            float verticalInput = Input.GetAxis("Mouse Y");
            currentPitch -= verticalInput * verticalSpeed; // Rotate vertically (look up/down)

            // Clamp the vertical angle to avoid the camera flipping upside down
            currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);

            // Calculate the position of the camera based on the distance, horizontal rotation, and height
            Vector3 direction = new Vector3(0, 0, -distance); // The offset behind the player
            Quaternion rotation = Quaternion.Euler(currentPitch, currentRotation, 0); // Apply both horizontal and vertical rotation

            // Apply the rotation to the camera position
            Vector3 cameraPosition = player.position + rotation * direction + new Vector3(0, height, 0);
            transform.position = cameraPosition;

            // Make the camera always look at the player
            transform.LookAt(player);
        }
    }
}
