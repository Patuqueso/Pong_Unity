using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Score player1Score;
    [SerializeField] private Score player2Score;

    private void HandleGoal(Player player)
    {
        if (player == Player.Left)
        {
            player1Score.AddScore();
        }
        else
        {
            player2Score.AddScore();
        }
    }
    private void OnEnable()
    {
        BallMove.OnGoalScored += HandleGoal;
    }

    private void OnDisable()
    {
        BallMove.OnGoalScored -= HandleGoal;
    }
}