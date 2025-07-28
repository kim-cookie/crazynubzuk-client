using UnityEngine;

public class BodyController : MonoBehaviour
{
    [Header("물리 설정")]
    public Rigidbody2D bodyRb;
    public float torquePower = 1f;
    public float stabilityTorque = 0.1f;
    public float maxAngularVelocity = 10f;

    private string myDirection;

    void Start()
    {
        float randomTorque = Random.Range(-1f, 1f);
        bodyRb.AddTorque(randomTorque, ForceMode2D.Impulse);
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

    void FixedUpdate()
    {
        float zAngle = NormalizeAngle(transform.eulerAngles.z);

        // 기울어진 각도에 따라 torque 가중 (중심을 기준으로 무너지는 듯)
        float torque = zAngle * stabilityTorque;

        // Z 축 기준 Torque 적용 (양수면 시계방향 회전)
        bodyRb.AddTorque(torque);

        // 최대 회전 속도 제한 (선택사항)
        bodyRb.angularVelocity = Mathf.Clamp(bodyRb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
    }

    // 회전각을 -180 ~ +180도로 정규화
    float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
