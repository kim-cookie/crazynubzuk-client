using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI thisTryRecord;
    public TextMeshProUGUI bestRecord;
    public Button retryButton;
    public Button exitButton;

    private void Start()
    {
        gameOverPanel.SetActive(false); // 시작할 땐 숨김

        retryButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene"); // 다시시작
        });

        exitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("RecordScene"); // 기록 보기로 이동
        });
    }

    public void ShowGameOver(int thisTry, int best)
    {
        gameOverPanel.SetActive(true);
        thisTryRecord.text = $"{thisTry}m";
        bestRecord.text = $"{best}m";
    }
}
