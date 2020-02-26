using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour
{
    private CharacterSelection characterSelection;
    private List<Character> characters = null;
    private int selectionIndex = 0;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI MovementSpeedText;
    public TextMeshProUGUI RotationSpeedText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI EnergyText;

    private void Awake()
    {
        characterSelection = FindObjectOfType<CharacterSelection>();
        characters = characterSelection.Characters;

        foreach (Character character in characters)
        {
            character.GetComponent<Character>();
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        Name.text = characters[selectionIndex].characterGeneralData.name;
        MovementSpeedText.text = characters[selectionIndex].characterStatsData.MovementSpeed.ToString();
        RotationSpeedText.text = characters[selectionIndex].characterStatsData.RotationSpeed.ToString();
        HealthText.text = characters[selectionIndex].characterResourcesData.Health.ToString();
        ShieldText.text = characters[selectionIndex].characterResourcesData.Shield.ToString();
        EnergyText.text = characters[selectionIndex].characterResourcesData.Energy.ToString();
    }

}
