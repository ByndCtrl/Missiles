using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("Text"), Tooltip("Assign through inspector.")]
    [SerializeField] private TextMeshProUGUI timeSurvived = null;
    [SerializeField] private TextMeshProUGUI missilesDestroyed = null;
    [SerializeField] private TextMeshProUGUI score = null;

    [Header("References")]
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private ScoreController scoreController = null;

    private void Awake()
    { 
        scoreController = FindObjectOfType<ScoreController>();
    }

    private void Start()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        UpdatePauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0f;
            } 
            else
            {
                Time.timeScale = 1f;
            }
        }

        UpdatePauseMenu();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdatePauseMenu()
    {
        timeSurvived.text = scoreController.TimeSurvived.ToString("F2");
        missilesDestroyed.text = scoreController.MissilesDestroyed.ToString();
        score.text = scoreController.Score.ToString();
    }
}
