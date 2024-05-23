using UnityEngine;
using Cinemachine;
using UnityEditor.Rendering;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public GameObject playerdeath_FX;
    public int choosenSkinId;
    public int fruits;

    public InGame_UI inGame_UI;

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

    private bool HaveEnoughFruits()
    {
        if (fruits > 0)
        {
            fruits--;
            if (fruits < 0)
            {
                fruits = 0;
            }
            return true;
        }
        return false;
    }

    public void OnTakeDamage()
    {
        if (HaveEnoughFruits())
        {
            Debug.Log("Fruits Droped");
        }
        else
        {
            KillPlayer();
            PermanentDeath();
        }
    }

    private void PermanentDeath()
    {
        if (GameManager.instance.difficulty < 3)
        {
            Invoke("PlayerRespawn", 1);
        }
        else
        {
            inGame_UI.OnDeath();
        }
    }

    public void OnFalling()
    {
        KillPlayer();

        if (HaveEnoughFruits())
        {
            PermanentDeath();
        }
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }

    public void KillPlayer()
    {
        GameObject newDeath_FX = Instantiate(playerdeath_FX, currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(currentPlayer);
    }
}
