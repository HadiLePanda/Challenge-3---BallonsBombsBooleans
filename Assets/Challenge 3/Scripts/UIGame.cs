using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    [Header("References")]
    public GameObject panel;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    private void Update()
    {
        // show only while game is not over
        if (!GameManager.singleton.IsGameOver)
        {
            // update score UI
            scoreText.text = GameManager.singleton.Score.ToString();

            // update timer UI
            timerText.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(GameManager.singleton.TimeSinceStart / 60), Mathf.FloorToInt(GameManager.singleton.TimeSinceStart % 60)); ;

            panel.gameObject.SetActive(true);
        }
        else panel.gameObject.SetActive(false);
    }
}
