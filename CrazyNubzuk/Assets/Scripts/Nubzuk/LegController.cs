using UnityEngine;

public class LegController : MonoBehaviour
{
    public Transform bodyRoot;         // 회전 기준 (BodyRoot)
    public Transform leftLeg;
    public Transform rightLeg;

    public float legRotationMultiplier = 0.2f;  // 1/5 회전

    void Update()
    {
        // 몸의 Z 회전 각도 (0~360 → -180~180 보정)
        float bodyZ = bodyRoot.eulerAngles.z;
        if (bodyZ > 180f) bodyZ -= 360f;

        // 다리 회전 적용 (Z축)
        float legZ = bodyZ * legRotationMultiplier;

        leftLeg.localRotation = Quaternion.Euler(0f, 0f, legZ);
        rightLeg.localRotation = Quaternion.Euler(0f, 0f, legZ);
    }
}
