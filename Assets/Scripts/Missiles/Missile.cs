using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Missile : MonoBehaviour
{
    protected Rigidbody2D missileRigidbody = null;
    protected BoxCollider2D boxCollider = null;
    protected GameObject model = null;

    [Header("Missile")]
    protected float movementSpeed = 25f;
    protected float rotationSpeed = 25f;
    protected float damage = 25f;
    protected float selfDestructTime = 0f;
    protected float rubberbandDistance = 25f;
    protected float rubberbandMovementModifier = 1.25f;

    [Header("Target")]
    protected Transform target = null;
    protected string targetTag = "Target";
    protected float distanceToTarget = 0;

    [Header("SFX")]
    [SerializeField] protected AudioClip[] collisionSFX = null;
    [SerializeField] protected AudioClip[] destroySFX = null;

    [Header("VFX")]
    [SerializeField] protected GameObject collisionVFX = null;
    [SerializeField] protected GameObject destroyVFX = null;

    [Header("Debug")]
    [SerializeField, Tooltip("Color of the line to the target.")]
    protected Color lineColor = new Color(255, 0, 155, 1);

    private PlayerResources playerResources = null;
    private MissileController missileController = null;

    private void Awake()
    {
        missileRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        missileController = FindObjectOfType<MissileController>();
        playerResources = FindObjectOfType<PlayerResources>();

        model = this.transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        Rubberbanding();
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
        Vector2 movementDirection = (Vector2)target.position - missileRigidbody.position;
        movementDirection.Normalize();

        float rotation = Vector3.Cross(movementDirection, transform.up).z;

        missileRigidbody.angularVelocity = -rotation * rotationSpeed;
        missileRigidbody.velocity = transform.up * movementSpeed;
    }

    private void Rubberbanding()
    {
        if (target != null)
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (distanceToTarget > rubberbandDistance)
            {
                movementSpeed = missileController.MovementSpeed() * missileController.RubberbandMovementModifier();
                rotationSpeed = missileController.RotationSpeed() * missileController.RubberbandMovementModifier();
            }
            else
            {
                movementSpeed = missileController.MovementSpeed();
                rotationSpeed = missileController.RotationSpeed();
            }
        }
    }

    private void InitMissile()
    {
        movementSpeed = missileController.MovementSpeed();
        rotationSpeed = missileController.RotationSpeed();
        damage = missileController.Damage();
        target = missileController.MissileTarget();
        selfDestructTime = missileController.SelfDestructTime();
        rubberbandDistance = missileController.RubberBandDistance();
        StartCoroutine(Destroy(selfDestructTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Target")
        {
            movementSpeed = missileController.MovementSpeed();
            target = missileController.PlayerTransform();
        }

        if (other.gameObject.tag == "Player")
        {
            playerResources.TakeDamage(damage);
            StartCoroutine(OnMissileCollision());
        }
    }

    private IEnumerator Destroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(OnMissileDestroy());
    }
    
    private IEnumerator OnMissileCollision()
    {
        collisionVFX = MissilePool.Instance.GetCollisionVFX(); // Get VFX instance from object pool
        collisionVFX.transform.position = this.transform.position; // Position VFX instance to the missile object
        collisionVFX.transform.localRotation = this.transform.localRotation; // Set rotation of VFX instance to be the same as the missile
        collisionVFX.GetComponent<VisualEffect>().Play(); // Play VFX
        CameraShake.CameraImpulse(0.25f); // Camera shake
        model.SetActive(false); // Deactive the missile model
        boxCollider.enabled = false; // Disable the missile collider
        yield return new WaitForSeconds(0.75f); // Wait for the VFX to play out
        gameObject.SetActive(false); // Deactive the entire missile object
        model.SetActive(true); // Activate model before returning to pool
        boxCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddMissileToPool(gameObject); // Return missile to pool
        MissilePool.Instance.AddCollisionVFXToPool(collisionVFX); // Return VFX to pool
    }
    private IEnumerator OnMissileDestroy()
    {
        destroyVFX = MissilePool.Instance.GetDestroyVFX(); // Get VFX instance from object pool
        destroyVFX.transform.position = this.transform.position; // Position VFX instance to the missile object
        destroyVFX.GetComponent<VisualEffect>().Play(); // Play VFX
        //AudioController.Instance.PlayRandom(explosionSelfDestructSFX);
        model.SetActive(false); // Deactive the missile model
        boxCollider.enabled = false; // Disable the missile collider
        yield return new WaitForSeconds(0.75f); // Wait for the VFX to play out
        gameObject.SetActive(false); // Deactive the entire missile object
        model.SetActive(true); // Activate model before returning to pool
        boxCollider.enabled = true; // Enable collider before returning to pool
        MissilePool.Instance.AddMissileToPool(gameObject); // Return missile to pool
        MissilePool.Instance.AddDestroyVFXToPool(destroyVFX); // Return VFX to pool
    }
    
    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
