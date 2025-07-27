using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomTitleText;
    public TextMeshProUGUI participantsText;

    private LobbySceneController controller;
    private string roomId;
    private int participants;

    public void Init(string roomId, int participants, LobbySceneController controller)
    {
        this.roomId = roomId;
        this.participants = participants;
        this.controller = controller;

        roomTitleText.text = roomId;
        participantsText.text = $"{participants} / 2";

        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        controller.ShowRoomInfo(roomId, controller.GetHostName(roomId), participants);
    }

    public void UpdateParticipants(int participants)
    {
        this.participants = participants;
        participantsText.text = $"{participants} / 2";
    }

    public string GetRoomId()
    {
        return roomId;
    }

    public int GetParticipants()
    {
        return participants;
    }
}
