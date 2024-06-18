using UnityEngine;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;

    public GameObject fruitPrefab;
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

            DropFruit();

            return true;
        }
        return false;
    }

    private void DropFruit()
    {
        int fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Fruit_Types)).Length);

        GameObject newFruit = Instantiate(fruitPrefab, currentPlayer.transform.position, transform.rotation);
        newFruit.GetComponent<Fruit_DroppedByPlayer>().FruitSetup(fruitIndex);
        Destroy(newFruit, 20);
    }

    public void OnTakeDamage()
    {
        if (!HaveEnoughFruits())
        {
            KillPlayer();

            if (GameManager.instance.difficulty < 3)
            {
                Invoke("PlayerRespawn", 1);
            }
            else
            {
                inGame_UI.OnDeath();
            }
        }
    }

    // private void PermanentDeath()
    // {
    //     if (GameManager.instance.difficulty < 3)
    //     {
    //         Invoke("PlayerRespawn", 1);
    //     }
    //     else
    //     {
    //         inGame_UI.OnDeath();
    //     }
    // }

    public void OnFalling()
    {
        KillPlayer();

        int difficulty = GameManager.instance.difficulty;

        if (difficulty < 3)
        {
            Invoke("PlayerRespawn", 1);

            if (difficulty > 1)
            {
                HaveEnoughFruits();
            }
        }
        else
        {
            inGame_UI.OnDeath();
        }

        // if (difficulty == 1)
        // {
        //     Invoke("PlayerRespawn", 1);
        // }

        // if (difficulty == 2)
        // {
        //     HaveEnoughFruits();
        //     Invoke("PlayerRespawn", 1);
        // }

        // if (difficulty == 3)
        // {
        //     inGame_UI.OnDeath();
        // }
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
            inGame_UI.AssignPlayerController(currentPlayer.GetComponent<Player_Controller>());
            AudioManager.instance.PlaySFX(11);
        }
    }

    public void KillPlayer()
    {
        AudioManager.instance.PlaySFX(0);

        GameObject newDeath_FX = Instantiate(playerdeath_FX, currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(newDeath_FX, 1);
        Destroy(currentPlayer);
    }
}
