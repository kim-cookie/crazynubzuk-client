using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RecordSceneController : MonoBehaviour
{
    [Header("UI References")]
    public Transform rankingListPanel; // ← RankingListPanel
    public GameObject rankingItemPrefab; // ← RankingItem 프리팹
    public Button lobbyButton;

    void Start()
    {
        lobbyButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LobbyScene");
        });

        // 2. 서버로 Record 요청 보내기
        string roomId = RoomDataManager.Instance.roomId;
        NetworkManager.Instance.Send(new RequestPacketData.Record(roomId));

        // 3. 응답 핸들러 등록
        NetworkManager.Instance.RegisterHandler<ResponsePacketData.Record>(OnReceiveRecord);
    }

    // 4. 서버 응답 처리
    private void OnReceiveRecord(ResponsePacketData.Record data)
    {
        foreach (Transform child in rankingListPanel)
        {
            Destroy(child.gameObject); // 기존 항목 정리
        }

        foreach (var item in data.rankingList)
        {
            GameObject go = Instantiate(rankingItemPrefab, rankingListPanel);
            RankingItem itemUI = go.GetComponent<RankingItem>();
            itemUI.Set(item.rank, item.username1, item.username2, item.highest);
        }
    }
}
