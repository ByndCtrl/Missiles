using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private GameObject debugUI = null;

    [SerializeField] private TextMeshProUGUI score = null;
    [SerializeField] private TextMeshProUGUI highScore = null;
    [SerializeField] private TextMeshProUGUI timeSurvived = null;
    [SerializeField] private TextMeshProUGUI missilesDestroyed = null;
    [SerializeField] private TextMeshProUGUI currency = null;
    [SerializeField] private TextMeshProUGUI selectionIndex = null;

    private void Start()
    {
        UpdateDebugUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (debugUI.activeSelf)
            {
                debugUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                debugUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        UpdateDebugUI();
    }

    public void UpdateDebugUI()
    {
        score.text = ScoreController.Instance.Score.ToString();
        highScore.text = "Highscore: " + ScoreController.Instance.HighScore.ToString();
        timeSurvived.text = ScoreController.Instance.TimeSurvived.ToString("F2");
        missilesDestroyed.text = ScoreController.Instance.MissilesDestroyed.ToString();
        currency.text = CurrencyController.Currency.ToString();
    }

    public void DebugSave()
    {
        PersistenceController.Save();
    }

    public void DebugLoad()
    {
        PersistenceController.Load();
    }

    public void DebugAddScore(int value)
    {
        ScoreController.Instance.AddScore(value);
    }

    public void DebugSubtractScore(int value)
    {
        ScoreController.Instance.SubtractScore(value);
    }

    public void DebugAddMissilesDestroyed(int value)
    {
        ScoreController.Instance.AddMissilesDestroyed(value);
    }

    public void DebugSubtractMissilesDestroyed(int value)
    {
        ScoreController.Instance.SubtractMissilesDestroyed(value);
    }

    public void DebugAddCurrency(int value)
    {
        CurrencyController.AddCurrency(value);
    }

    public void DebugSubtractCurrency(int value)
    {
        CurrencyController.SubtractCurrency(value);
    }

    public void ResetEverything()
    {
        ScoreController.Instance.ResetScore();
        ScoreController.Instance.ResetTimeSurvived();
        ScoreController.Instance.ResetMissilesDestroyed();
        CurrencyController.ResetCurrency();
        CharacterSelection.Instance.SelectionIndex = 0;
    }
}
