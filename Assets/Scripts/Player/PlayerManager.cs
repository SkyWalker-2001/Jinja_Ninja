using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public int choosenSkinId;
    public int fruits;

    [Header("Camera Shake FX")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDir;
    [SerializeField] private float forceMultiplier;

    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector2(shakeDir.x * facingDir, shakeDir.y) * forceMultiplier;
        impulse.GenerateImpulse();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }
}
