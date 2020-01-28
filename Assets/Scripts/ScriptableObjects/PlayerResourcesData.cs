using UnityEngine;

[CreateAssetMenu(menuName = "Player/Resources", fileName = "Resources")]
public class PlayerResourcesData : ScriptableObject
{
    [SerializeField] private int health = 0;
    [SerializeField] private int shield = 0;
    [SerializeField] private int energy = 0;

    public int Health { get { return health; } }
    public int Shield { get { return shield; } }
    public int Energy { get { return energy; } }
}