using UnityEngine;

public class HeadController : MonoBehaviour
{
    public GameOverUIController gameOverUI; // Inspector에서 연결
    public int thisTryDistance = 0;         // 이번 시도 거리
    public int bestDistance = 0;            // 최고 거리 (저장된 값 등)

    private bool isGameOver = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;

        if (other.CompareTag("Ground"))
        {
            Debug.Log("Game Over: 머리가 땅에 닿았어요!");
            isGameOver = true;
            GameSceneController.Instance.TriggerGameOver();
        }
    }
}
