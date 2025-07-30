using UnityEngine;
using TMPro;

public class RankingItem : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;
    public TextMeshProUGUI scoreText;

    public void Set(int rank, string p1, string p2, int score)
    {
        rankText.text = $"Rank {rank}";
        player1Text.text = p1;
        player2Text.text = p2;
        scoreText.text = $"{score}m";
    }
}
