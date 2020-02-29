using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    /// <summary>
    /// Temporary, TODO: Overwrite with values based on selected character
    /// </summary>
    [Header("Movement")]
    [SerializeField] private float baseMovementSpeed = 100f;
    [SerializeField] private float baseRotationSpeed = 100f;

    private float currentMovementSpeed = 0f;
    private float currentRotationSpeed = 0f;

    private float maxMovementSpeed = 0f;
    private float maxRotationSpeed = 0f;

    [Header("Boost")]
    [SerializeField] private float maxSpeedModifier = 1.25f;
    [SerializeField] private float boostStep = 0.1f;
    [SerializeField] private float interpolationStep = 0.1f;

    private void Start()
    {
        currentMovementSpeed = baseMovementSpeed;
        currentRotationSpeed = baseRotationSpeed;

        maxMovementSpeed = baseMovementSpeed * maxSpeedModifier;
        maxRotationSpeed = baseRotationSpeed * maxSpeedModifier;
    }

    private void FixedUpdate()
    {
        Movement();
        Boost();
        InterpolateMovement();
        //Debug.Log("[BasicPlayerController] Current movement speed: " + currentMovementSpeed + ".");
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMovementSpeed = Mathf.Lerp(baseMovementSpeed, maxMovementSpeed, boostStep * Time.deltaTime);
        }
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * currentMovementSpeed * Time.deltaTime, Space.Self);

        float rotation = Input.GetAxis("Horizontal") * -currentRotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, 0, rotation, Space.Self);
    }

    private void InterpolateMovement()
    {
        if (currentMovementSpeed != baseMovementSpeed)
        {
            currentMovementSpeed = Mathf.Lerp(CurrentMovementSpeed(), baseMovementSpeed, interpolationStep);
        }

        if (currentRotationSpeed != baseRotationSpeed)
        {
            currentRotationSpeed = Mathf.Lerp(CurrentRotationSpeed(), baseRotationSpeed, interpolationStep);
        }
    }

    private float CurrentMovementSpeed()
    {
        return currentMovementSpeed;
    }

    private float CurrentRotationSpeed()
    {
        return currentRotationSpeed;
    }
}