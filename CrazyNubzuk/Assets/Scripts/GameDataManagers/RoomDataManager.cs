using UnityEngine;

public class RoomDataManager : MonoBehaviour
{
    public static RoomDataManager Instance;

    [Header("방 정보")]
    public string roomId;
    public string hostNickname;
    public string guestNickname;
    public string hostDirection;
    public string guestDirection;

    [Header("참가 상태")]
    public int participants = 0;

    [Header("준비 상태")]
    public bool hostReady = false;
    public bool guestReady = false;

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

    public bool AreBothReady()
    {
        return hostReady && guestReady;
    }

    public void Reset()
    {
        roomId = "";
        hostNickname = "";
        guestNickname = "";
        participants = 0;
        hostReady = false;
        guestReady = false;
    }
}
