using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float moveSpeed = 20f;

    public float zoomSpeed = 5f;
    public float minZoom = 5f; // Minimum zoom distance
    public float maxZoom = 50f; // Maximum zoom distance

    private float currentZoom = 10f; // Initial zoom level

    void Update()
    {
        // Get input from the arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

        // Create a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // Move the camera
        transform.Translate(movement);
    

    float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Check if the zoom can be adjusted
        if ((scroll > 0f && currentZoom <= maxZoom) || (scroll < 0f && currentZoom >= minZoom))
        {
            // Adjust zoom based on scroll input
            currentZoom -= scroll * zoomSpeed; 
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // Clamp zoom values
            
            // Move the camera
            Vector3 zooming = new Vector3(0, currentZoom, 0);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, zooming.y, Camera.main.transform.position.z);
        }

}
}
