using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float rotationSpeed = 1f; // Speed of rotation in degrees per second
    private Transform sunlightTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Find the object named "Sunlight"
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

    // Update is called once per frame
    void Update()
    {
        if (sunlightTransform != null)
        {
            // Rotate the sunlight around the y-axis
            sunlightTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
