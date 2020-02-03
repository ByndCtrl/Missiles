using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string characterName = "";
    [SerializeField] private int characterHealth = 100;
    [SerializeField] private int characterShield = 100;
    [SerializeField] private int characterEnergy = 100;
    [SerializeField] private int characterPrice = 0;
    [SerializeField] private int characterMovementSpeed = 100;
    [SerializeField] private int characterRotationSpeed = 100;
    [SerializeField] private GameObject characterModel = null;

    public string CharacterName { get { return characterName; } }
    public int CharacterHealth { get { return characterHealth; } }
    public int CharacterShield { get { return characterShield; } }
    public int CharacterEnergy { get { return characterEnergy; } }
    public int CharacterPrice { get { return characterPrice; } }
    public int CharacterMovementSpeed { get { return characterMovementSpeed; } }
    public int CharacterRotationSpeed { get { return characterRotationSpeed; } }
    public GameObject CharacterModel { get { return characterModel; } }
}