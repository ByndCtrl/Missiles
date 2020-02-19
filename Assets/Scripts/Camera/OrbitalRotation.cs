using UnityEngine;
using System.Collections;

public class OrbitalRotation : MonoBehaviour
{
    public Transform Target;
    public float RotationSpeed;
    public float OnClickSpeedModifier = 5f;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("CharacterSelection").transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(Target.position, Vector3.up, Input.GetAxis("Mouse X") * RotationSpeed * OnClickSpeedModifier * Time.deltaTime);
        }

        if (!Input.GetMouseButton(1))
        {
            transform.RotateAround(Target.position, Vector3.up, RotationSpeed * Time.deltaTime);
        }
    }
}