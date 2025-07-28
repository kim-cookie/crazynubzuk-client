using UnityEngine;
using TMPro;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController Instance { get; private set; }

    [Header("타이머 UI")]
    public TextMeshProUGUI timerText;

    [Header("게임 오버 UI")]
    public GameOverUIController gameOverUI;

    private float elapsedTime = 0f;
    private bool isGameRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!isGameRunning) return;

        elapsedTime += Time.deltaTime;
        int displayTime = Mathf.FloorToInt(elapsedTime);
        timerText.text = displayTime + "m";
    }

    public void StartGame()
    {
        elapsedTime = 0f;
        isGameRunning = true;
        GameStateManager.Instance.gameStarted = true;
    }

    public void TriggerGameOver()
    {
        if (!isGameRunning) return;

        isGameRunning = false;
        GameStateManager.Instance.gameFinished = true;

        int thisTry = Mathf.FloorToInt(elapsedTime);

        // ✅ RoomDataManager 기반 기록 비교 및 갱신
        if (thisTry > RoomDataManager.Instance.bestRecord)
        {
            RoomDataManager.Instance.bestRecord = thisTry;
        }
        Time.timeScale = 0f;
        gameOverUI.ShowGameOver(thisTry, RoomDataManager.Instance.bestRecord);
    }
}
