using UnityEngine;
using Cinemachine;

public class VirtualCamFollow : MonoBehaviour
{
    private Transform cameraTarget;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;

        if (cinemachineVirtualCamera != null)
        {
            cinemachineVirtualCamera.Follow = cameraTarget;
            cinemachineVirtualCamera.LookAt = cameraTarget;
        }
    }
}