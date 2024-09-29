using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
