using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI timeSurvivedText = null;
    [SerializeField] private TextMeshProUGUI missilesDestroyedText = null;

    private void Awake()
    {

    }

    private void Start()
    {
        InitScoreUI();
    }

    private void Update()
    {
        UpdateScoreUI();
    }

    void InitScoreUI()
    {
        ScoreController.Instance.ScoreChange += OnScoreChange;
    }

    void OnScoreChange()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + ScoreController.Instance.Score;
        }
    }

    public void UpdateScoreUI()
    {
        if (timeSurvivedText != null)
        {
            timeSurvivedText.text = ScoreController.Instance.TimeSurvived.ToString("F2");
        }

        if (scoreText != null)
        {
            scoreText.text = ScoreController.Instance.Score.ToString();
        }

        if (missilesDestroyedText != null)
        {
            missilesDestroyedText.text = ScoreController.Instance.MissilesDestroyed.ToString();
        }
    }

    private void OnDestroy()
    {
        ScoreController.Instance.ScoreChange -= OnScoreChange;
    }
}
