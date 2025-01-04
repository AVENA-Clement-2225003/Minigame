using System.Collections;
using UnityEngine;


namespace BUT
{
    public class WaterController : MonoBehaviour
    {
        [SerializeField] private float riseSpeed = 1f; // Speed of water rising
        [SerializeField] private float maxHeight = 10f; // Maximum height the water can reach
        [SerializeField] private float waitBeforeRise = 2f; // Time to wait before water starts rising
        public Transform waterPlane; // Reference to the water plane
        private bool isRising = false;

        private void Start()
        {
            // Start the water rising process after waiting
            StartCoroutine(StartWaterRiseAfterDelay());
        }

        private void Update()
        {
            if (isRising && waterPlane.position.y < maxHeight)
            {
                // Move the water upwards
                waterPlane.position += Vector3.up * riseSpeed * Time.deltaTime;

                // Stop rising if the water reaches the max height
                if (waterPlane.position.y >= maxHeight)
                {
                    isRising = false;
                }
            }
        }

        private IEnumerator StartWaterRiseAfterDelay()
        {
            // Wait for the specified duration
            yield return new WaitForSeconds(waitBeforeRise);
            isRising = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // When an object touches the water, lift it upwards
            if (other.CompareTag("LiftTable"))
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    StartCoroutine(LiftObject(rb));
                }
            }
        }

        private IEnumerator LiftObject(Rigidbody rb)
        {
            while (isRising && rb.transform.position.y < maxHeight)
            {
                rb.MovePosition(rb.transform.position + Vector3.up * riseSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void ResetWaterRise()
        {
            // Stop any water rise that was in progress
            isRising = false;

            // Reset water position to its starting point (optional)
            waterPlane.position = new Vector3(waterPlane.position.x, 0f, waterPlane.position.z); // Adjust this Y value to your starting height

            // Restart the coroutine to begin the rise after a delay
            StartCoroutine(StartWaterRiseAfterDelay());
        }
    }
}