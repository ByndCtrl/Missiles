using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    private Transform playerModelTransform; // Used to append instantiated gameobject as child to player model

    private float xRot = 0f;
    private float yRot = 0f;
    private float zRot = 0f;

    private void Awake()
    {
        playerModelTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
    }

    private void Start()
    {
        if (PersistentCharacterData.Characters[PersistentCharacterData.CharacterSelectionIndex]
          .characterData.CharacterModel != null)
        {
            GameObject playerCharacter = Instantiate(PersistentCharacterData.Characters[PersistentCharacterData.CharacterSelectionIndex]
          .characterData.CharacterModel, transform.position, Quaternion.identity);
            playerCharacter.transform.SetParent(playerModelTransform);
            playerCharacter.transform.eulerAngles = new Vector3(xRot, yRot, zRot);
        }
    }
}
