using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : Singleton<CharacterSelection>
{
    public List<Character> Characters = new List<Character>();
    public List<GameObject> CharacterModels = new List<GameObject>();
    public int SelectionIndex = 0;

    private Vector3 spawnPosition = new Vector3(0, 5, 0);

    private CharacterSelectionUI selectionUI = null;

    private void Awake()
    {
        base.Awake();
        selectionUI = FindObjectOfType<CharacterSelectionUI>();
    }

    private void Start()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            GameObject character = Instantiate(Characters[i].characterGeneralData.CharacterModel, spawnPosition, Quaternion.identity);
            character.gameObject.name = Characters[i].characterGeneralData.CharacterName;
            character.SetActive(false);

            CharacterModels.Add(character);
        }

        foreach (Character character in Characters)
        {
            character.gameObject.SetActive(false); 
        }

        CharacterModels[0].gameObject.SetActive(true);
    }

    public void NextCharacter()
    {
        CharacterModels[SelectionIndex].gameObject.SetActive(false);

        SelectionIndex++;
        if (SelectionIndex == Characters.Count)
        {
            SelectionIndex = 0;
        }

        CharacterModels[SelectionIndex].gameObject.SetActive(true);

        selectionUI.UpdateUI();
    }

    public void PreviousCharacter()
    {
        CharacterModels[SelectionIndex].gameObject.SetActive(false);

        SelectionIndex--;
        if (SelectionIndex < 0)
        {
            SelectionIndex = Characters.Count - 1;
        }

        CharacterModels[SelectionIndex].gameObject.SetActive(true);

        selectionUI.UpdateUI();
    }

    private void OnDestroy()
    {
        Characters.Clear();
        CharacterModels.Clear();
    }
}
