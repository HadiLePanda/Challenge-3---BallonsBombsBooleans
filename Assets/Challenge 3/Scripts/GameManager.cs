using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private float difficultyIncreaseInterval = 5f;
    [SerializeField] private float speedBaseMultiplier = 1.0f;
    [SerializeField] private float speedMultiplierIncrementPerLevel = 0.2f;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;

    private int currentDifficultyLevel = 1;

    public bool IsGameOver { get; private set; }
    public int Score { get; private set; }
    public int CurrentDifficultyLevel
    {
        get => currentDifficultyLevel;
        private set => currentDifficultyLevel = Mathf.Max(1, value);
    }
    public float TimeSinceStart { get; private set; }
    public float SessionFinishTime { get; private set; }

    public Action onGameOver;

    public static GameManager singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        // increase the difficulty over time
        InvokeRepeating(nameof(IncreaseDifficulty), difficultyIncreaseInterval, difficultyIncreaseInterval);

        TimeSinceStart = 0f;
        SessionFinishTime = 0f;
    }

    private void Update()
    {
        TimeSinceStart += Time.deltaTime;
    }

    public void GameOver()
    {
        IsGameOver = true;
        SessionFinishTime = TimeSinceStart;

        // play game over sound
        audioSource.PlayOneShot(deathSound, 1.0f);

        Debug.Log("Game Over!");
        onGameOver?.Invoke();
    }

    public void AddScore(int amount)
    {
        Score = Mathf.Max(0, Score + amount);
    }

    // increase game difficulty
    public void IncreaseDifficulty()
    {
        if (!IsGameOver)
        {
            CurrentDifficultyLevel += 1;
            Debug.Log($"IncreaseDifficulty {CurrentDifficultyLevel} (Multiplier: {GetDifficultySpeedMultiplier(CurrentDifficultyLevel)})");
        }
    }

    public float GetDifficultySpeedMultiplier(int difficultyLevel)
    {
        // for level 1,
        // the speed should be at base multiplier
        if (difficultyLevel == 1)
            return speedBaseMultiplier;

        // for difficulty levels after level 1,
        // flat increase of x% every level
        return speedBaseMultiplier + (speedMultiplierIncrementPerLevel * (difficultyLevel - 1));
    }
}
