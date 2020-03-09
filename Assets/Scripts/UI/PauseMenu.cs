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

    private void Start()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        UpdatePauseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        UpdatePauseMenu();
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
        if (pauseMenu.activeSelf)
        {
            timeSurvived.text = ScoreController.Instance.TimeSurvived.ToString("F2");
            missilesDestroyed.text = ScoreController.Instance.MissilesDestroyed.ToString();
            score.text = ScoreController.Instance.Score.ToString();
        }
    }
}
