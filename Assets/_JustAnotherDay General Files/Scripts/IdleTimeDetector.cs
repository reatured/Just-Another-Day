using UnityEngine;

public class IdleTimeDetector : MonoBehaviour
{
    public float idleThreshold = 5f; // Idle threshold in seconds
    private float idleTimer = 0f;

    private void Update()
    {

        // Check if there is any mouse movement or input
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.anyKeyDown)
        {
            // Reset the idle timer
            idleTimer = 0f;
        }
        else
        {
            // Increment the idle timer
            idleTimer += Time.deltaTime;

            // Check if idle time exceeds the threshold
            if (idleTimer >= idleThreshold)
            {
                // Perform desired actions when idle time is detected
                Debug.Log("Idle time detected!");
                // Add your code here to handle the idle state
            }
        }
    }
}