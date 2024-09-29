using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Reference to the character prefab
    public Transform spawnPoint; // Where the character will spawn
    public float speed = 2f; // Speed at which the character moves

    private GameObject currentCharacter; // Reference to the spawned character

    private void Start()
    {
        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        // Instantiate the character at the spawn point
        currentCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveCharacterToTarget());
    }

    private System.Collections.IEnumerator MoveCharacterToTarget()
    {
        Vector3 targetPosition = transform.position; // The target position (this object's position)

        // Move towards the target position
        while (currentCharacter != null && Vector3.Distance(currentCharacter.transform.position, targetPosition) > 1f)
        {
            // Calculate the direction to the target
            Vector3 direction = (targetPosition - currentCharacter.transform.position).normalized;

            // Rotate the character towards the target
            if (direction != Vector3.zero) // Ensure we don't divide by zero
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                currentCharacter.transform.rotation = Quaternion.Slerp(currentCharacter.transform.rotation, lookRotation, Time.deltaTime * 5f); // Adjust the speed of rotation
            }

            // Move the character towards the target point
            currentCharacter.transform.position = Vector3.MoveTowards(currentCharacter.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Once the character reaches the target, make it disappear
        Destroy(currentCharacter);
        SpawnCharacter(); // Optionally spawn another character
    }
}
