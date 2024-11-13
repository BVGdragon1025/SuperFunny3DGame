using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the "Building" or "Lemur" tag
        if (other.CompareTag("Lemur"))
        {
            GameManager.Instance.LemurAmount--;
            Destroy(other.gameObject); // Destroy the building or lemur
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the other object has the "Building" or "Lemur" tag
        if (other.CompareTag("Building"))
        {
            
            Destroy(other.gameObject); // Destroy the building or lemur
        }
    }
}
