using UnityEngine;
using UnityEngine.UI; // Add this to use UI Image
public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused;
    public RawImage pauseOverlay; // Reference to the raw image component
    
    void Start()
    {
        // Ensure the overlay is hidden at the start
         pauseOverlay.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape clicked.");
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Debug.Log("Paused.");
            pauseOverlay.gameObject.SetActive(true); // Show the overlay
            Time.timeScale = 0;
            
        }
        else
        {
            Debug.Log("Unpaused.");
            pauseOverlay.gameObject.SetActive(false); // Hide the overlay
            Time.timeScale = 1;
            
        }
    }
}
