using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Missile : MonoBehaviour
{
    private MissileController missileController = null;
    private Transform missileTarget = null;

    private Rigidbody2D missileRigidbody;
    private float missileMovementSpeed = 25f;
    private float missileRotationSpeed = 25f;
    private float missileDamage = 25f;

    private VisualEffect explosionCollisionVFX = null;
    private VisualEffect explosionSelfDestructVFX = null;

    /// <summary>
    /// missileModel is the 2nd child of the main Missile object, assign the model through prefab inspector
    /// disabling the model alone allows the VFX to play on impact without deactivating the entire object
    /// </summary>
    [SerializeField] private GameObject missileModel = null;
    private BoxCollider2D missileCollider = null;

    private void Awake()
    {
        missileController = FindObjectOfType<MissileController>();   
        missileRigidbody = GetComponent<Rigidbody2D>();
        missileCollider = GetComponent<BoxCollider2D>();
        explosionCollisionVFX = null;
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
        missileDamage = missileController.MissileDamage();
        missileTarget = missileController.MissileTarget();
        StartCoroutine(SelfDestruct(15f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collided with " + other.gameObject.name + ".");
            other.gameObject.GetComponent<PlayerResources>().TakeDamage(missileDamage);
            StartCoroutine(OnMissileCollision());
        }
    }

    private IEnumerator SelfDestruct(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(OnMissileSelfDestruct());
    }

    private IEnumerator OnMissileCollision()
    {
        explosionCollisionVFX.Play(); // Play explosion VFX
        CameraShake.CameraImpulse(0.25f); // Camera shake
        missileModel.SetActive(false); // Deactive the model only
        missileCollider.enabled = false; // Disable the collider
        yield return new WaitForSeconds(1f); // Wait 1 second for the VFX to play out
        explosionCollisionVFX.Stop();
        gameObject.SetActive(false); // Deactive the entire missile object
        missileModel.SetActive(true); // Activate model before returning to pool
        missileCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddToPool(gameObject); // Return to pool
        yield return null; // Exit
    }

    private IEnumerator OnMissileSelfDestruct()
    {
        //explosionSelfDestructVFX.Play(); // TODO: Create through VFX Graph
        missileModel.SetActive(false); // Deactive the model only
        missileCollider.enabled = false; // Disable the collider
        yield return new WaitForSeconds(1f); // Wait 1 second for the VFX to play out
        //explosionSelfDestructVFX.Stop(); // TODO: Create through VFX Graph
        gameObject.SetActive(false); // Deactive the entire missile object
        missileModel.SetActive(true); // Activate model before returning to pool
        missileCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddToPool(gameObject); // Return to pool
        yield return null; // Exit
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
