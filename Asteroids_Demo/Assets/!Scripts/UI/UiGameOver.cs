using TMPro;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;

    public void ShowGameOver(int currentScore, int highScore)
    {
        gameOverPanel.gameObject.SetActive(true);

        currentScoreText.SetText($"Your score: {currentScore}");
        hiScoreText.SetText($"High score: {highScore}");
    }

    public void HideGameOver()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
}
