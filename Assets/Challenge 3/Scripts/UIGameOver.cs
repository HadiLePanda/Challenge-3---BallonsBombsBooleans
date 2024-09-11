using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [Header("References")]
    public GameObject panel;
    public TMP_Text sessionScoreText;
    public TMP_Text sessionTimeText;

    private void Start()
    {
        GameManager.singleton.onGameOver += OnGameOver;
    }
    private void OnDestroy()
    {
        GameManager.singleton.onGameOver -= OnGameOver;
    }

    private void Update()
    {
        // show only if game is over
        if (GameManager.singleton.IsGameOver)
        {
            panel.gameObject.SetActive(true);
        }
        else panel.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        UpdateGameOverUI();
    }

    private void UpdateGameOverUI()
    {
        // display session score
        sessionScoreText.text = GameManager.singleton.Score.ToString();

        // update session time
        sessionTimeText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(GameManager.singleton.SessionFinishTime / 60), Mathf.FloorToInt(GameManager.singleton.SessionFinishTime % 60)); ;
    }

    public void PlayAgain()
    {
        // reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
