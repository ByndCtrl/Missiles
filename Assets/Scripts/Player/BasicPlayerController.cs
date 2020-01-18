using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    /// <summary>
    /// Temporary, TODO: Overwrite with values based on selected character
    /// </summary>
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 100f;
    [SerializeField] private float rotationSpeed = 100f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime, Space.Self);

        float rotation = Input.GetAxis("Horizontal") * -rotationSpeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, 0, rotation, Space.Self);
    }
}