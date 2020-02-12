using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public ScoreController Instance;

    public static int Score;
    public static int HighScore;
    public static float TimeSurvived;
    public static float MaxTimeSurvived;
    
    private float timeSinceGameStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }

    public static void SubtractScore(int scoreToSubtract)
    {
        Score -= scoreToSubtract;
    }

    public static void ResetScore()
    {
        Score = 0;
    }

    public static void ResetTimeSurvived()
    {
        TimeSurvived = 0;
        MaxTimeSurvived = 0;
    }
}
