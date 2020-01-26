using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX; // Experimental

public class Missile : MonoBehaviour
{
    private MissileController missileController = null;
    private Transform missileTarget = null;

    private Rigidbody2D missileRigidbody;
    private float missileMovementSpeed = 25f;
    private float missileRotationSpeed = 25f;

    // Experimental
    private VisualEffect explosionVFX = null;

    private void Awake()
    {
        missileController = FindObjectOfType<MissileController>();   
        missileRigidbody = GetComponent<Rigidbody2D>();

        // Experimental
        explosionVFX = GetComponent<VisualEffect>();
    }

    private void FixedUpdate()
    {
        MissileMovement();
    }

    private void OnEnable()
    {
        missileController.activeMissiles.Add(this);
        InitMissile();
    }

    private void OnDisable()
    {
        missileController.activeMissiles.Remove(this);
    }

    private void MissileMovement()
    {
        Vector2 movementDirection = (Vector2)missileTarget.position - missileRigidbody.position;
        movementDirection.Normalize();

        float missileRotation = Vector3.Cross(movementDirection, transform.up).z;

        missileRigidbody.angularVelocity = -missileRotation * missileRotationSpeed;
        missileRigidbody.velocity = transform.up * missileMovementSpeed;
    }

    private void InitMissile()
    {
        missileMovementSpeed = missileController.MissileMovementSpeed();
        missileRotationSpeed = missileController.MissileRotationSpeed();
        missileTarget = missileController.MissileTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            explosionVFX.Play();
            Debug.Log("Hit Player, playing explosionVFX.");
            CameraShake.CameraImpulse(0.25f);
        }
    }

    private void OnDrawGizmos()
    {
        if (missileTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, missileTarget.position);
        }
    }
}
