using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.3f;          // Time the Camera Shake effect will last
    [SerializeField] private float shakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    [SerializeField] private float shakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private static float shakeElapsedTime = 0f;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (cinemachineVirtualCamera != null)
            virtualCameraNoise = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            shakeElapsedTime = shakeDuration;
        }

        if (cinemachineVirtualCamera != null && virtualCameraNoise != null)
        {
            if (shakeElapsedTime > 0)
            {
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

    public static void CameraImpulse(float duration)
    {
        shakeElapsedTime = duration;
    }
}