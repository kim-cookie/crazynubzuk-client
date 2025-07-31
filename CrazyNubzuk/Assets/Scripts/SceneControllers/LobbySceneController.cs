using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LobbySceneController : MonoBehaviour
{
    [Header("Room List")]
    public GameObject roomItemPrefab;
    public Transform roomListContent;

    [Header("Room Info UI")]
    public GameObject roomInfoPanel;
    public TextMeshProUGUI roomTitleText;
    public TextMeshProUGUI hostNameText;
    public TextMeshProUGUI participantsText;

    [Header("Buttons")]
    public Button createRoomButton;
    public Button enterRoomButton;

    [Header("Create Room Popup")]
    public GameObject createRoomPopup;
    public TMP_InputField roomTitleInput;
    public Toggle leftToggle;
    public Toggle rightToggle;
    public Button confirmCreateButton;
    public Button cancelCreateButton;
    private Dictionary<string, RoomItem> roomItemMap = new(); // roomId -> RoomItem

    private string selectedRoomId = "";

    void Start()
    {
        roomInfoPanel.SetActive(false);
        createRoomPopup.SetActive(false);

        createRoomButton.onClick.AddListener(() => createRoomPopup.SetActive(true));
        cancelCreateButton.onClick.AddListener(() => createRoomPopup.SetActive(false));
        confirmCreateButton.onClick.AddListener(OnCreateRoomConfirmed);
        enterRoomButton.onClick.AddListener(OnEnterRoomClicked);

        NetworkManager.Instance.RegisterHandler<ResponsePacketData.CreateRoom>(OnCreateRoom);
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.EnterRoom>(OnEnterRoom);
    }

    // 방 생성 팝업에서 확인 누르면
    void OnCreateRoomConfirmed()
    {
        string roomId = roomTitleInput.text.Trim();
        if (string.IsNullOrEmpty(roomId)) return;

        string direction = leftToggle.isOn ? "Left" : "Right";
        string hostName = UserDataManager.Instance.nickname;

        // 서버에 CREATE_ROOM 요청
        var req = new RequestPacketData.CreateRoom(hostName, roomId, direction);
        NetworkManager.Instance.Send(req);

        // ✅ 임시 데이터 저장 (예: UserDataManager 활용)
        RoomDataManager.Instance.roomId = roomId;
        RoomDataManager.Instance.hostNickname = hostName;
        RoomDataManager.Instance.hostDirection = direction;
        RoomDataManager.Instance.guestDirection = direction == "Left" ? "Right" : "Left";
        UserDataManager.Instance.isHost = true;
        UserDataManager.Instance.direction = direction;
    }

    void OnCreateRoom(ResponsePacketData.CreateRoom data)
    {
        // RoomItem 생성
        GameObject item = Instantiate(roomItemPrefab, roomListContent);
        RoomItem roomItem = item.GetComponent<RoomItem>();
        roomItem.Init(data.roomName, data.participants, this);
        roomItemMap[data.roomName] = roomItem;

        RoomDataManager.Instance.participants = data.participants;

        createRoomPopup.SetActive(false);
        SceneManager.LoadScene("WaitingScene");
    }


    // ScrollView의 방 항목 클릭 시 호출될 함수
    public void ShowRoomInfo(string roomId, string hostName, int participants)
    {
        selectedRoomId = roomId;
        roomTitleText.text = $"Room ID: {roomId}";
        hostNameText.text = $"Host: {hostName}";
        participantsText.text = $"Participants: {participants} / 2";
        roomInfoPanel.SetActive(true);
    }

    // RoomItem에서 호스트 이름을 가져감
    public string GetHostName(string roomId)
    {
        return RoomDataManager.Instance.hostNickname;
    }

    void OnEnterRoomClicked()
    {
        if (string.IsNullOrEmpty(selectedRoomId)) return;

        // 참가자 수 제한 검사
        if (!roomItemMap.TryGetValue(selectedRoomId, out var roomItem)) return;
        if (roomItem.GetParticipants() >= 2)
        {
            Debug.LogWarning("이미 인원이 가득 찬 방입니다.");
            // 팝업 UI나 알림 창으로도 보여줄 수 있음
            return;
        }

        string nickname = UserDataManager.Instance.nickname;

        // 서버에 ENTER_ROOM 요청
        var req = new RequestPacketData.EnterRoom(nickname);
        NetworkManager.Instance.Send(req);

        // 데이터 저장
        RoomDataManager.Instance.roomId = selectedRoomId;
        RoomDataManager.Instance.participants = 2;
        UserDataManager.Instance.isHost = false;
    }

    void OnEnterRoom(ResponsePacketData.EnterRoom data)
    {
        UserDataManager.Instance.direction = data.direction;
        SceneManager.LoadScene("WaitingScene");
    }


}
