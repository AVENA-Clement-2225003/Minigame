using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;  // Import the Input System

namespace BUT
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform teleportDestination; // The empty GameObject where the player will be teleported to
        [SerializeField] private GameObject player;

        private void OnTriggerEnter(Collider colision)
        {
            UnityEngine.Debug.LogWarning("OnTriggerEnter called");
            if (colision.gameObject.activeSelf) TeleportPlayer();
        }

        private void TeleportPlayer()
        {
            UnityEngine.Debug.LogWarning("TeleportPlayer called");

            if (player != null && teleportDestination != null)
            {
                // Assuming player uses CharacterController
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    // Move the player to the teleport destination using CharacterController's Move
                    Vector3 teleportPosition = teleportDestination.position;
                    characterController.enabled = false; // Temporarily disable the CharacterController to directly move the player
                    player.transform.position = teleportPosition; // Directly set the position
                    characterController.enabled = true;  // Re-enable the CharacterController

                    UnityEngine.Debug.LogWarning("Player teleported to: " + teleportPosition);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Player does not have a CharacterController component.");
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("Teleportation failed: Missing player or teleport destination.");
            }
        }
    }
}
