using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [Header("References")]
    public GameObject[] objectPrefabs;

    [Header("Settings")]
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnDelay, spawnInterval);
    }

    // spawn obstacles
    private void SpawnObjects()
    {
        // set random spawn location and random object index
        Vector3 spawnLocation = new(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // if game is still active, spawn new object
        if (!GameManager.singleton.IsGameOver)
        {
            // spawn new object
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
