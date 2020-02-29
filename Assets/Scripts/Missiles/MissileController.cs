using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 missileMovementSpeedMinMax = new Vector2(75f, 150f);
    public Vector2 missileRotationSpeedMinMax = new Vector2(50f, 100f);
    private float missileMovementSpeed = 50f;
    private float missileRotationSpeed = 50f;

    [Header("Stats")]
    [SerializeField] private Vector2 missileDamageMinMax = new Vector2(5, 25);
    private float missileDamage = 25f;

    private Transform playerTransform = null;
    public List<Transform> Targets = new List<Transform>();

    [HideInInspector] public List<Missile> activeMissiles = null;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeMissiles = new List<Missile>();
    }

    private void FixedUpdate()
    {
        //Debug.Log("Active Missiles: " + activeMissiles.Count + ".");
        //Debug.Log("Current missile movement speed: " + MissileMovementSpeed() + ".");
        //Debug.Log("Current missile rotation speed: " + MissileRotationSpeed() + ".");
    }

    public float MissileMovementSpeed()
    {
        missileMovementSpeed = Mathf.Lerp(missileMovementSpeedMinMax.x, missileMovementSpeedMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return missileMovementSpeed;
    }

    public float MissileRotationSpeed()
    {
        missileRotationSpeed = Mathf.Lerp(missileRotationSpeedMinMax.x, missileRotationSpeedMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return missileRotationSpeed;
    }

    public float MissileDamage()
    {
        missileDamage = Mathf.Lerp(missileDamageMinMax.x, missileDamageMinMax.y, Difficulty.GetMissileDifficultyPercent());
        return missileDamage;
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
            MissilePool.Instance.AddToMissilePool(activeMissiles[i].gameObject);
        }
    }

    private void OnDestroy()
    {
        DestroyAllMissiles();
        Targets.Clear();
    }
}
