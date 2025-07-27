using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WaitingSceneController : MonoBehaviour
{
    [Header("Title")]
    public TextMeshProUGUI titleText; // RoomID 표시용

    [Header("Host Info")]
    public TextMeshProUGUI hostNicknameText;
    public TextMeshProUGUI hostDirectionText;
    public TextMeshProUGUI hostStatusText;

    [Header("Guest Info")]
    public GameObject guestPanel;
    public TextMeshProUGUI guestNicknameText;
    public TextMeshProUGUI guestDirectionText;
    public TextMeshProUGUI guestStatusText;

    [Header("Buttons")]
    public Button readyButton;
    public Button startGameButton;
    public Button exitButton;

    // 상태 변수
    private bool isHost;

    void Start()
    {
        // 초기 설정
        isHost = UserDataManager.Instance.isHost;
        titleText.text = $"Room ID: {RoomDataManager.Instance.roomId}";

        hostNicknameText.text = $"Nickname: {RoomDataManager.Instance.hostNickname}";
        hostDirectionText.text = $"Direction: {RoomDataManager.Instance.hostDirection}";
        hostStatusText.text = "Status: Not Ready";

        guestPanel.SetActive(true);
        // 게스트 패널 초기화
        guestNicknameText.text = "Waiting for player...";
        guestDirectionText.gameObject.SetActive(false);
        guestStatusText.gameObject.SetActive(false);

        startGameButton.interactable = false;

        // 버튼 연결
        readyButton.onClick.AddListener(OnClickReady);
        startGameButton.onClick.AddListener(OnClickStartGame);
        exitButton.onClick.AddListener(OnClickExit);
    }

    void Update()
    {
        if (RoomDataManager.Instance.participants >= 2)
        {
            UpdateGuestPanel();
        }
    }

    void UpdateGuestPanel()
    {
        guestNicknameText.text = $"Nickname: {RoomDataManager.Instance.guestNickname}";
        guestDirectionText.text = $"Direction: {RoomDataManager.Instance.guestDirection}";
        guestDirectionText.gameObject.SetActive(true);
        guestStatusText.text = "Status: Not Ready";
        guestStatusText.gameObject.SetActive(true);
    }

    void OnClickReady()
    {
        if (isHost)
        {
            RoomDataManager.Instance.hostReady = !RoomDataManager.Instance.hostReady;
            hostStatusText.text = RoomDataManager.Instance.hostReady ? "Status: Ready" : "Status: Not Ready";
        }
        else
        {
            RoomDataManager.Instance.guestReady = !RoomDataManager.Instance.guestReady;
            guestStatusText.text = RoomDataManager.Instance.guestReady ? "Status: Ready" : "Status: Not Ready";
        }

        UpdateStartButtonState();
    }

    void UpdateStartButtonState()
    {
        startGameButton.interactable = (RoomDataManager.Instance.participants >= 2 && RoomDataManager.Instance.hostReady && RoomDataManager.Instance.guestReady && isHost);
    }

    void OnClickStartGame()
    {
        if (startGameButton.interactable)
        {
            Debug.Log("게임 시작!");
            // 예: SceneManager.LoadScene("GameScene");
        }
    }

    void OnClickExit()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
