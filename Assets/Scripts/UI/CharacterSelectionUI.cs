using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour
{
    private CharacterSelection characterSelection;
    private List<Character> characters = null;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI MovementSpeedText;
    public TextMeshProUGUI RotationSpeedText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI EnergyText;

    public Button selectButton;
    public Button buyButton;

    private void Awake()
    {
        characterSelection = FindObjectOfType<CharacterSelection>();
        characters = characterSelection.Characters;

    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        Name.text = characters[characterSelection.SelectionIndex].characterGeneralData.CharacterName;
        MovementSpeedText.text = characters[characterSelection.SelectionIndex].characterStatsData.MovementSpeed.ToString();
        RotationSpeedText.text = characters[characterSelection.SelectionIndex].characterStatsData.RotationSpeed.ToString();
        HealthText.text = characters[characterSelection.SelectionIndex].characterResourcesData.Health.ToString();
        ShieldText.text = characters[characterSelection.SelectionIndex].characterResourcesData.Shield.ToString();
        EnergyText.text = characters[characterSelection.SelectionIndex].characterResourcesData.Energy.ToString();

        if (!characters[characterSelection.SelectionIndex].isCharacterOwned)
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        } 
        else
        {
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
    }

}
