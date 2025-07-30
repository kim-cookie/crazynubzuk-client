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

        // 🔁 WebSocket 응답 핸들러 등록
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.ReadyGame>(OnReadyGame);
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.StartGame>(OnStartGame);
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.Exit>(OnExitResponse);
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
        string nickname = UserDataManager.Instance.nickname;
        NetworkManager.Instance.Send(new RequestPacketData.ReadyGame(nickname));
    }

    void OnReadyGame(ResponsePacketData.ReadyGame data)
    {
        RoomDataManager.Instance.hostReady = data.hostReady;
        RoomDataManager.Instance.guestReady = data.guestReady;

        hostStatusText.text = data.hostReady ? "Status: Ready" : "Status: Not Ready";
        guestStatusText.text = data.guestReady ? "Status: Ready" : "Status: Not Ready";

        UpdateStartButtonState();
    }

    void UpdateStartButtonState()
    {
        startGameButton.interactable = (RoomDataManager.Instance.participants >= 2 && RoomDataManager.Instance.hostReady && RoomDataManager.Instance.guestReady && isHost);
    }

    void OnClickStartGame()
    {
        string nickname = UserDataManager.Instance.nickname;
        string role = UserDataManager.Instance.direction;
        NetworkManager.Instance.Send(new RequestPacketData.StartGame(nickname, role));
    }

    void OnStartGame(ResponsePacketData.StartGame _)
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnClickExit()
    {
        NetworkManager.Instance.Send(new RequestPacketData.Exit());
    }

    void OnExitResponse(ResponsePacketData.Exit _)
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
