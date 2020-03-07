using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class ScoreController : MonoBehaviour
{
    public int Score = 0;
    public int HighScore = 0;
    public float TimeSurvived = 0;
    public float MaxTimeSurvived = 0;
    public float MissilesDestroyed = 0;
    
    private float timeSinceGameStart = 0;

    private void Awake()
    {

    }

    private void Start()
    {
        timeSinceGameStart = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        TimeSurvived = Time.time;
    }

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }

    public void SubtractScore(int scoreToSubtract)
    {
        Score -= scoreToSubtract;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void ResetTimeSurvived()
    {
        TimeSurvived = 0;
        MaxTimeSurvived = 0;
    }
}
