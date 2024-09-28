using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAnimationAndLoadScene : MonoBehaviour
{
    public Animator animator;     
    public string exitAnimationTrigger = "Exit";  
    public string sceneToLoad = "PiScene"; 

    private bool hasTriggeredExit = false; 

    void Update()
    {
        if (Input.anyKeyDown && !hasTriggeredExit)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
