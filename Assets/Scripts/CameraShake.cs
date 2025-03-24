using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
