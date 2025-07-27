using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;

    [Header("유저 정보")]
    public string nickname;
    public bool isHost;
    public string direction; // "left" or "right"

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

    public void Reset()
    {
        nickname = "";
        isHost = false;
        direction = "";
    }
}
