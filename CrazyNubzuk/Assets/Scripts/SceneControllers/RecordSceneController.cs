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
    }
}
