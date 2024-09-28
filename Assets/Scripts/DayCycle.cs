using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float rotationSpeed = 1f; // Speed of rotation in degrees per second
    private Transform sunlightTransform;

    private int dayNumber = 1;
    private DayCounter dayCounter;

    private float totalRotation = 180f; // Track total rotation

    private void Start()
    {
        // Find the object named "Sunlight"
        dayCounter = DayCounter.Instance;
        GameObject sunlight = GameObject.Find("Sunlight");
        if (sunlight != null)
        {
            sunlightTransform = sunlight.transform;
        }
        else
        {
            Debug.LogWarning("Sunlight object not found!");
        }
    }

    private void Update()
    {
        if (sunlightTransform != null)
        {
            // Rotate the sunlight around the y-axis
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            sunlightTransform.Rotate(Vector3.up, rotationThisFrame);
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
