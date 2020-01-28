using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats", fileName = "Stats")]
public class PlayerStatsData : ScriptableObject
{
    [SerializeField] private int movementSpeed = 0;
    [SerializeField] private int rotationSpeed = 0;

    public int MovementSpeed { get { return movementSpeed; } }
    public int RotationSpeed { get { return rotationSpeed; } }
}