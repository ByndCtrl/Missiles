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

    private Vector2 selfDestructTimeMinMax = new Vector2(10, 20f);
    private float selfDestructTime = 0f;

    private VisualEffect trail;
    private GameObject explosionCollisionVFX = null;
    private GameObject explosionSelfDestructVFX = null;

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
        trail = GetComponent<VisualEffect>();
        selfDestructTime = Random.Range(selfDestructTimeMinMax.x, selfDestructTimeMinMax.y);
    }

    private void FixedUpdate()
    {
        MissileMovement();
    }

    private void OnEnable()
    {
        missileController.activeMissiles.Add(this);
        InitMissile();
        trail.Play();
    }

    private void OnDisable()
    {
        missileController.activeMissiles.Remove(this);
        trail.Stop();
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
        StartCoroutine(SelfDestruct(selfDestructTime));
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
        explosionCollisionVFX = MissilePool.Instance.GetVFXCollisionFromPool(); // Get VFX instance from object pool
        explosionCollisionVFX.transform.position = this.transform.position; // Position VFX instance to the missile object
        explosionCollisionVFX.transform.localRotation = this.transform.localRotation; // Set rotation of VFX instance to be the same as the missile
        explosionCollisionVFX.GetComponent<VisualEffect>().Play(); // Play VFX
        CameraShake.CameraImpulse(0.25f); // Camera shake
        missileModel.SetActive(false); // Deactive the missile model
        missileCollider.enabled = false; // Disable the missile collider
        trail.Stop();
        yield return new WaitForSeconds(0.75f); // Wait for the VFX to play out
        gameObject.SetActive(false); // Deactive the entire missile object
        missileModel.SetActive(true); // Activate model before returning to pool
        missileCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddToMissilePool(gameObject); // Return missile to pool
        MissilePool.Instance.AddToVFXCollisionPool(explosionCollisionVFX); // Return VFX to pool
        yield return null; // Exit
    }
    private IEnumerator OnMissileSelfDestruct()
    {
        explosionSelfDestructVFX = MissilePool.Instance.GetVFXDestroyFromPool(); // Get VFX instance from object pool
        explosionSelfDestructVFX.transform.position = this.transform.position; // Position VFX instance to the missile object
        explosionSelfDestructVFX.GetComponent<VisualEffect>().Play(); // Play VFX
        missileModel.SetActive(false); // Deactive the missile model
        missileCollider.enabled = false; // Disable the missile collider
        trail.Stop();
        yield return new WaitForSeconds(0.75f); // Wait for the VFX to play out
        gameObject.SetActive(false); // Deactive the entire missile object
        missileModel.SetActive(true); // Activate model before returning to pool
        missileCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddToMissilePool(gameObject); // Return missile to pool
        MissilePool.Instance.AddToVFXDestroyPool(explosionSelfDestructVFX); // Return VFX to pool
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
