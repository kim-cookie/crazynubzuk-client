using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroSceneController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nicknameInput;
    public Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        // 응답 핸들러 등록
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.EnterLobby>(OnEnterLobby);
    }

    void OnStartClicked()
    {
        string nickname = nicknameInput.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.Log("닉네임을 입력해주세요.");
            return;
        }

        UserDataManager.Instance.nickname = nickname;

        // 서버로 EnterLobby 요청
        var packet = new RequestPacketData.EnterLobby(nickname);
        NetworkManager.Instance.Send(packet);

    }

    void OnEnterLobby(ResponsePacketData.EnterLobby data)
    {
        Debug.Log($"입장 성공");

        SceneManager.LoadScene("LobbyScene");
    }
}
