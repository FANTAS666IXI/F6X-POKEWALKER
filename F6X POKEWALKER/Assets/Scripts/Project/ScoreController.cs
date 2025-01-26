using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        score = 0;
    }

    public void AddScore()
    {
        if (score < 1000000)
            score++;
    }

    public int GetScore()
    {
        return score;
    }
}