using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;
    public GameObject currentPlayer;

    private void Awake()
    {
        instance = this;
        PlayerRespawn();
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }
}
