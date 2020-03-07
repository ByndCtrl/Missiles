using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [Header("General")]
    public Vector2 SelfDestructTimeMinMax = new Vector2(10, 15);
    private float selfDestructTime = 0f;

    [Header("Movement")]
    public Vector2 MovementSpeedMinMax = new Vector2(75f, 150f);
    public Vector2 RotationSpeedMinMax = new Vector2(50f, 100f);
    [SerializeField] private float rubberbandDistance = 200f;
    [SerializeField] private float rubberbandMovementModifier = 1.25f;

    private float movementSpeed = 50f;
    private float rotationSpeed = 50f;

    [Header("Damage")]
    [SerializeField] private Vector2 damageMinMax = new Vector2(5, 25);
    private float damage = 5f;

    [Header("Targets")]
    public List<Transform> Targets = new List<Transform>();
    private Transform playerTransform = null;

    [HideInInspector] public List<Missile> activeMissiles = null;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeMissiles = new List<Missile>();
    }

    public float MovementSpeed()
    {
        movementSpeed = Mathf.Lerp(MovementSpeedMinMax.x, MovementSpeedMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return movementSpeed;
    }

    public float RotationSpeed()
    {
        rotationSpeed = Mathf.Lerp(RotationSpeedMinMax.x, RotationSpeedMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return rotationSpeed;
    }

    public float RubberBandDistance()
    {
        return rubberbandDistance;
    }

    public float RubberbandMovementModifier()
    {
        return rubberbandMovementModifier;
    }

    public float Damage()
    {
        damage = Mathf.Lerp(damageMinMax.x, damageMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return damage;
    }

    public float SelfDestructTime()
    {
        selfDestructTime = Random.Range(SelfDestructTimeMinMax.x, SelfDestructTimeMinMax.y);
        return selfDestructTime;
    }

    public Transform PlayerTransform()
    {
        return playerTransform;
    }

    public Transform MissileTarget()
    {
        return Targets[Random.Range(0, Targets.Count)];
    }

    public void DestroyAllMissiles()
    {
        for (int i = 0; i < activeMissiles.Count; i++)
        {
            MissilePool.Instance.AddMissileToPool(activeMissiles[i].gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyAllMissiles();
        Targets.Clear();
    }
}
