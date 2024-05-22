using UnityEngine;
using Cinemachine;

public class CameraShakeFX : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDir;
    [SerializeField] private float forceMultiplier;

    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector2(shakeDir.x * facingDir, shakeDir.y) * forceMultiplier;
        impulse.GenerateImpulse();
    }
}
