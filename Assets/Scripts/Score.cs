using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "0";
    }

    public void AddScore()
    {
        int currentScore = int.Parse(scoreText.text);
        currentScore++;
        scoreText.text = currentScore.ToString();
    }
}