using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector2 missileMovementSpeedMinMax = new Vector2(75f, 150f);
    [SerializeField] private Vector2 missileRotationSpeedMinMax = new Vector2(50f, 100f);

    private float missileMovementSpeed = 50f;
    private float missileRotationSpeed = 50f;

    private Transform missileTarget = null;
    [HideInInspector] public List<Missile> activeMissiles = null;

    private void Awake()
    {
        missileTarget = GameObject.FindGameObjectWithTag("Player").transform;
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

    public Transform MissileTarget()
    {
        return missileTarget;
    }

    public void DestroyAllMissiles()
    {
        for (int i = 0; i < activeMissiles.Count; i++)
        {
            MissilePool.Instance.AddToPool(activeMissiles[i].gameObject);
        }
    }
}
