using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterGeneralData : ScriptableObject
{
    [SerializeField] private string characterName = "";
    [SerializeField] private int characterPrice = 0;
    [SerializeField] private GameObject characterModel = null;

    public string CharacterName { get { return characterName; } }
    public int CharacterPrice { get { return characterPrice; } }
    public GameObject CharacterModel { get { return characterModel; } }
}