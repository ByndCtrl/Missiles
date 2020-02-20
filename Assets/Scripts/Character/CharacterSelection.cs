using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] Characters = null;
    public int SelectionIndex = 0;

    private Vector3 spawnPosition = new Vector3(0, 5, 0);

    private void Awake()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Instantiate(Characters[i], spawnPosition, Quaternion.identity);
        }

        Characters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject character in Characters)
        {
            character.SetActive(false);
            character.gameObject.name = character.GetComponent<Character>().characterGeneralData.CharacterName;
        }

        Characters[0].SetActive(true);
    }

    public void NextCharacter()
    {
        Characters[SelectionIndex].SetActive(false);

        SelectionIndex++;
        if (SelectionIndex == Characters.Length)
        {
            SelectionIndex = 0;
        }

        Characters[SelectionIndex].SetActive(true);
    }

    public void PreviousCharacter()
    {
        Characters[SelectionIndex].SetActive(false);

        SelectionIndex--;
        if (SelectionIndex < 0)
        {
            SelectionIndex = Characters.Length - 1;
        }

        Characters[SelectionIndex].SetActive(true);
    }
}
