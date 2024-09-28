using UnityEngine;
using TMPro;
public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused;
    public TextMeshProUGUI pauseText;
    
    private void Start()
    {
        pauseText.text = "";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseText.text = "PAUSED";
        }
        else 
        {
            Time.timeScale = 1;
            pauseText.text = "";
        }
    }
}