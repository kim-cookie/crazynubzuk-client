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

        retryButton.onClick.AddListener(OnRetryClicked);
        exitButton.onClick.AddListener(OnExitClicked);

        // retryButton.onClick.AddListener(() =>
        // {
        //     Time.timeScale = 1f;
        //     SceneManager.LoadScene("GameScene"); // 다시시작
        // });

        // exitButton.onClick.AddListener(() =>
        // {
        //     // ✅ 서버에 Record 전송
        //     var recordPacket = new RequestPacketData.Record(RoomDataManager.Instance.roomId);
        //     NetworkManager.Instance.Send(recordPacket);

        //     SceneManager.LoadScene("RecordScene"); // 기록 보기로 이동
        // });
    }

    public void ShowGameOver(int thisTry, int best)
    {
        gameOverPanel.SetActive(true);
        thisTryRecord.text = $"{thisTry}m";
        bestRecord.text = $"{best}m";
    }

    private void OnRetryClicked()
    {
        Time.timeScale = 1f;

        // ✅ 서버에 Restart 요청
        string nickname = UserDataManager.Instance.nickname;
        NetworkManager.Instance.Send(new RequestPacketData.Restart(nickname));

        // 씬 재시작
        SceneManager.LoadScene("GameScene");
    }

    private void OnExitClicked()
    {
        // ✅ 서버에 Record 및 Exit 요청
        string roomId = RoomDataManager.Instance.roomId;

        NetworkManager.Instance.Send(new RequestPacketData.Record(roomId));
        NetworkManager.Instance.Send(new RequestPacketData.Exit());

        // 씬 전환
        SceneManager.LoadScene("RecordScene");
    }
}
