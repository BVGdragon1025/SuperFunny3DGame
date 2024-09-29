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
        // Move towards the target position
        while (currentCharacter != null && Vector3.Distance(currentCharacter.transform.position, transform.position) > 0.1f)
        {
            // Move the character towards the target point
            currentCharacter.transform.position = Vector3.MoveTowards(currentCharacter.transform.position, transform.position, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Once the character reaches the target, make it disappear
        Destroy(currentCharacter);
        SpawnCharacter();
    }
}
