using UnityEngine;

public class NubzukController : MonoBehaviour
{
    [Header("Parts")]
    public Transform head;
    public Transform body;
    public Transform leftLeg;
    public Transform rightLeg;

    [Header("Balance Settings")]
    public float torqueForce = 10f;         // 회전 힘
    public float maxAngle = 60f;            // 최대 회전 각도 제한
    public float groundY = -2.5f;           // 머리 닿는 y 위치

    private Rigidbody2D rb;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 머리/몸통 위치 고정
        head.localPosition = new Vector3(0f, 1.5f, 0f);
        body.localPosition = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if (isGameOver) return;

        HandleInput();
        FixLegRotation();
        CheckGameOver();
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(torqueForce); // 왼쪽으로 돌림
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-torqueForce); // 오른쪽으로 돌림
        }

        // 최대 회전 각도 제한 (넘으면 정지)
        float angleZ = rb.rotation; // Z축 각도
        if (angleZ > 180f) angleZ -= 360f;

        if (Mathf.Abs(angleZ) >= maxAngle)
        {
            rb.angularVelocity = 0;
            Debug.Log("⚠ 최대 각도 초과, 넘어짐 직전");
        }
    }

    void FixLegRotation()
    {
        // 다리는 항상 수직
        leftLeg.localRotation = Quaternion.identity;
        rightLeg.localRotation = Quaternion.identity;
    }

    void CheckGameOver()
    {
        if (head.position.y <= groundY)
        {
            isGameOver = true;
            rb.angularVelocity = 0;
            Debug.Log("💀 Game Over: 머리 바닥 닿음");
            // TODO: 애니메이션, UI, 리스타트 등
        }
    }
}
