using UnityEngine;
using TMPro;

public class GameSceneController : MonoBehaviour
{
    public static GameSceneController Instance { get; private set; }

    [Header("타이머 UI")]
    public TextMeshProUGUI timerText;

    [Header("게임 오버 UI")]
    public GameOverUIController gameOverUI;

    [Header("바디 컨트롤러")]
    public BodyController bodyController;

    private float survivalTime = 0f;
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

        // 서버로부터 balance response 받기 위한 핸들러 등록
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.UpdateBalance>(OnReceiveUpdateBalance);
    }

    public void StartGame()
    {
        survivalTime = 0f;
        isGameRunning = true;
        GameStateManager.Instance.gameStarted = true;
        Time.timeScale = 1f;
    }

    public void TriggerGameOver()
    {
        if (!isGameRunning) return;

        isGameRunning = false;
        GameStateManager.Instance.gameFinished = true;

        int thisTry = Mathf.FloorToInt(survivalTime);

        // ✅ RoomDataManager 기반 기록 비교 및 갱신
        if (thisTry > RoomDataManager.Instance.bestRecord)
        {
            RoomDataManager.Instance.bestRecord = thisTry;
        }
        Time.timeScale = 0f;
        gameOverUI.ShowGameOver(thisTry, RoomDataManager.Instance.bestRecord);
    }

    // 서버에서 balance 값이 들어올 때 호출되는 핸들러
    private void OnReceiveUpdateBalance(ResponsePacketData.UpdateBalance data)
    {
        if (!isGameRunning) return;

        // 1. Balance 적용
        bodyController.ApplyBalanceRotation(data.balance);

        // 2. 생존 시간 UI 동기화 (선택사항)
        survivalTime = Mathf.FloorToInt(data.survivalTime);
        timerText.text = survivalTime + "m";

        // 3. 게임 오버 처리
        if (data.isGameOver)
        {
            TriggerGameOver();
        }
    }
}
