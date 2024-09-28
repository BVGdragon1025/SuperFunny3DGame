using UnityEngine;

public class FloodScript : MonoBehaviour
{
    public float floodHeight = 1f; // Total height to flood
    public float floodSpeed = 0.5f; // Speed of the flooding
    public bool flooding = false;

    private float initialYPosition; // To store the initial Y position of the object
    private float targetYPosition; // Target Y position after flooding

    void Start()
    {
        // Store the initial Y position
        initialYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (flooding)
        {
            targetYPosition = initialYPosition + floodHeight;
            // Smoothly move towards the target height
            float newYPosition = Mathf.MoveTowards(transform.position.y, targetYPosition, floodSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            // Stop flooding if we reach the target height
            if (Mathf.Abs(newYPosition - targetYPosition) < 0.01f)
            {
                flooding = false; // Stop flooding once the target is reached
                initialYPosition = transform.position.y;
            }
        }
    }
}
