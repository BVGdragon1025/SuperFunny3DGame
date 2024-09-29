using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float rotationSpeed = 1f; // Speed of rotation in degrees per second
    private Transform sunlightTransform;
    private Transform clockTransform; // Reference to the clock transform

    private int dayNumber = 1;
    private DayCounter dayCounter;

    private float totalRotation = 180f; // Track total rotation

    private void Start()
    {
        // Find the object named "Sunlight"
        dayCounter = DayCounter.Instance;
        GameObject sunlight = GameObject.Find("Sunlight");
        GameObject clock = GameObject.Find("Clock");

        if (sunlight != null)
        {
            sunlightTransform = sunlight.transform;
        }
        else
        {
            Debug.LogWarning("Sunlight object not found!");
        }

        if (clock != null)
        {
            clockTransform = clock.transform; // Get the Transform component of the clock
        }
        else
        {
            Debug.LogWarning("Clock object not found!");
        }
    }

    private void Update()
    {
        if (sunlightTransform != null)
        {
            // Rotate the sunlight around the Y-axis
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            sunlightTransform.Rotate(Vector3.up, rotationThisFrame);

            // If you want to rotate the clock as well:
            if (clockTransform != null)
            {
                clockTransform.Rotate(Vector3.forward, rotationThisFrame); // Rotate clock around Z-axis
            }

            totalRotation += rotationThisFrame;

            // Check if the total rotation has reached 360 degrees
            if (totalRotation >= 360f)
            {
                dayNumber++;
                dayCounter.UpdateDay(dayNumber);
                
                // Reset the total rotation to avoid overflow
                totalRotation -= 360f;
            }
        }
    }
}
