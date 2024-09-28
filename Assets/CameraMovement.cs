using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from the arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

        // Create a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // Move the camera
        transform.Translate(movement);
    }
}
