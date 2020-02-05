using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public Character[] Characters;
    [HideInInspector] public int CharacterSelectionIndex = 0;

    private Vector3 spawnPoint = new Vector3(0, 5, 0);

    private void Awake()
    {
        foreach (Character character in Characters)
        {
            Instantiate(character, spawnPoint, Quaternion.identity);
            character.gameObject.SetActive(false);
        }

        Characters[0].gameObject.SetActive(true);
    }

    public void NextCharacter()
    {
        CharacterSelectionIndex--;

        if (CharacterSelectionIndex < 0)
        {
            CharacterSelectionIndex = Characters.Length - 1;
        }

        SwitchCharacter(CharacterSelectionIndex);
        Debug.Log(CharacterSelectionIndex);
    }

    public void PreviousCharacter()
    {
        CharacterSelectionIndex++;

        if (CharacterSelectionIndex == Characters.Length)
        {
            CharacterSelectionIndex = 0;
        }

        SwitchCharacter(CharacterSelectionIndex);
        Debug.Log(CharacterSelectionIndex);
    }

    public void SelectCharacter()
    {
        PersistentCharacterData.CharacterSelectionIndex = CharacterSelectionIndex;
    }

    private void DisableCharacters()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            if (Characters[i].gameObject.activeSelf)
            {
                Characters[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Called when cycling between characters.
    /// Makes sure only the currently selected character is active.
    /// </summary>
    /// <param name="index"></param>
    public void SwitchCharacter(int index)
    {
        DisableCharacters();
        Characters[index].gameObject.SetActive(true);
    }
}
