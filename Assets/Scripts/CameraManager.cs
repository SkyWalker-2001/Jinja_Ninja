using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    [SerializeField] private Color gizmosColor;

    private void Start()
    {
        myCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            myCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player_Controller>() != null)
        {
            myCamera.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(polygonCollider2D.bounds.center, polygonCollider2D.bounds.size);
    }
}
