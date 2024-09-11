using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Obstacle[] obstaclePrefabs;

    [Header("Settings")]
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float minSpawnHeight = 3f;
    [SerializeField] private float maxSpawnHeight = 15f;

    private void Start()
    {
        // spawn obstacles over time
        InvokeRepeating(nameof(SpawnObjects), spawnDelay, spawnInterval);
    }

    // spawn obstacles
    private void SpawnObjects()
    {
        // if game is not over, spawn new object
        if (!GameManager.singleton.IsGameOver)
        {
            // set random spawn location and random object index
            Vector3 spawnLocation = new(spawnDistance, Random.Range(minSpawnHeight, maxSpawnHeight), 0);
            int index = Random.Range(0, obstaclePrefabs.Length);

            // spawn new object
            Obstacle obstacleInstance = Instantiate(obstaclePrefabs[index], spawnLocation, obstaclePrefabs[index].transform.rotation);

            // setup obstacle speed based on current difficulty
            float currentDifficultySpeedMultiplier = GameManager.singleton.GetDifficultySpeedMultiplier(GameManager.singleton.CurrentDifficultyLevel);
            obstacleInstance.movement.speed *= currentDifficultySpeedMultiplier;
        }
    }
}
