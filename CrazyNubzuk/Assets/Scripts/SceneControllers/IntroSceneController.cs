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

        // WebSocketManager.Send("ENTER_LOBBY", {nickname}) ← 나중에
        // 지금은 씬 이동만
        SceneManager.LoadScene("LobbyScene");
    }
}
