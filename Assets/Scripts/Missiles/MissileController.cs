using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //private float baseMissileMovementSpeed = 50f;
    //private float baseMissileRotationSpeed = 50f;

    public float missileMovementSpeed = 50f;
    public float missileRotationSpeed = 50f;

    public Transform missileTarget = null;
    public List<Missile> activeMissiles = null;

    private void Awake()
    {
        missileTarget = GameObject.FindGameObjectWithTag("Player").transform;

        activeMissiles = new List<Missile>();
    }

    private void FixedUpdate()
    {
        Debug.Log("Active Missiles: " + activeMissiles.Count + ".");
    }
}
