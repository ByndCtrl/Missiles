using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<Character> Characters = new List<Character>();
    public List<GameObject> CharacterModels = new List<GameObject>();
    public int SelectionIndex = 0;

    private Vector3 spawnPosition = new Vector3(0, 5, 0);

    private void Awake()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            GameObject character = Instantiate(Characters[i].characterGeneralData.CharacterModel, spawnPosition, Quaternion.identity);
            character.SetActive(false);
            CharacterModels.Add(character);
        }

        foreach (Character character in Characters)
        {
            character.gameObject.SetActive(false);
            character.gameObject.name = character.GetComponent<Character>().characterGeneralData.CharacterName;
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
    }

    private void OnDestroy()
    {
        Characters.Clear();
        CharacterModels.Clear();
    }
}
