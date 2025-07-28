using UnityEngine;

public class BodyController : MonoBehaviour
{
    [Header("물리 설정")]
    public Rigidbody2D bodyRb;
    public float torquePower = 10f;

    private string myDirection;

    void Start()
    {
        // 본인의 방향 저장
        // myDirection = UserDataManager.Instance.direction.ToLower(); // "left" or "right"
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            bodyRb.AddTorque(torquePower);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            bodyRb.AddTorque(-torquePower);
        }
        // // Left 방향을 맡은 경우
        // if (myDirection == "left" && Input.GetKey(KeyCode.LeftArrow))
        // {
        //     bodyRb.AddTorque(torquePower);
        // }
        // // Right 방향을 맡은 경우
        // else if (myDirection == "right" && Input.GetKey(KeyCode.RightArrow))
        // {
        //     bodyRb.AddTorque(-torquePower);
        // }
    }
}
