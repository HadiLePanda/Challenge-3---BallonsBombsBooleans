using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Debug.Log("Game Over!");
    }
}
