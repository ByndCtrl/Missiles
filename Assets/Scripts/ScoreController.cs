using System;
using UnityEngine;

public class ScoreController : Singleton<ScoreController>
{
    public int Score = 0;
    public int HighScore = 0;
    public int MissilesDestroyed = 0;
    public float TimeSurvived = 0;
    public float MaxTimeSurvived = 0;

    private float timeSinceGameStart = 0;

    public event Action ScoreChange;

    private void Start()
    {
        timeSinceGameStart = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        TimeSurvived = Time.time;
    }

    public void AddScore(int scoreAmount)
    {
        Score += scoreAmount;

        if (Score > HighScore)
        {
            HighScore = Score;
        }

        ScoreChange();
    }

    public void SubtractScore(int scoreAmount)
    {
        if (Score > 0)
        {
            Score -= scoreAmount;

            if (Score < 0)
            {
                Score = 0;
            }
        }

        ScoreChange();
    }

    public void AddMissilesDestroyed(int amount)
    {
        MissilesDestroyed += amount;

        ScoreChange();
    }

    public void SubtractMissilesDestroyed(int amount)
    {
        if (MissilesDestroyed > 0)
        {
            MissilesDestroyed -= amount;
        }

        ScoreChange();
    }

    public void ResetScore()
    {
        Score = 0;
        HighScore = 0;
    }

    public void ResetTimeSurvived()
    {
        TimeSurvived = 0;
        MaxTimeSurvived = 0;
    }

    public void ResetMissilesDestroyed()
    {
        MissilesDestroyed = 0;
    }
}
