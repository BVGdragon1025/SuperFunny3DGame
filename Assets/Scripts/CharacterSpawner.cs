using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Reference to the character prefab
    public Transform spawnPoint; // Where the character will spawn
    public float speed = 2f; // Speed at which the character moves
    public float radius = 5f; // Radius within which the character stops moving
    public int lemurCount = 5; // Number of lemurs to spawn

    private void Start()
    {
        StartCoroutine(SpawnCharacters());
    }

    private System.Collections.IEnumerator SpawnCharacters()
    {
        for (int i = 0; i < lemurCount; i++)
        {
            SpawnCharacter();
            yield return new WaitForSeconds(2f); // Wait for 2 seconds between spawns
        }
    }

    void SpawnCharacter()
    {
        // Instantiate the character at the spawn point
        GameObject currentCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveCharacterToTarget(currentCharacter));
    }

    private System.Collections.IEnumerator MoveCharacterToTarget(GameObject currentCharacter)
    {
        Vector3 targetPosition = transform.position; // The target position (this object's position)

        float elapsedTime = 0f; // Timer for checking movement
        float checkInterval = 1f; // Check distance every second
        Vector3 previousPosition = currentCharacter.transform.position;

        // Move towards the target position
        while (currentCharacter != null && Vector3.Distance(currentCharacter.transform.position, targetPosition) > radius)
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
            Vector3 newPosition = Vector3.MoveTowards(currentCharacter.transform.position, targetPosition, speed * Time.deltaTime);
            currentCharacter.transform.position = newPosition;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Check if one second has passed
            if (elapsedTime >= checkInterval)
            {
                // Check distance and see if the character has moved
                if (Vector3.Distance(previousPosition, currentCharacter.transform.position) < 0.5f)
                {
                    break; // Exit the loop if the character hasn't moved
                }

                // Reset the timer and update the previous position
                elapsedTime = 0f;
                previousPosition = currentCharacter.transform.position;
            }

            yield return null; // Wait for the next frame
        }

        // Once the character reaches the target, make it disappear
        Destroy(currentCharacter);
    }

}
 