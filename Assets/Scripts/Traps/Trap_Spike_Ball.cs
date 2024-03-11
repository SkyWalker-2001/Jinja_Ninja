using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 pushDirection;

    void Start()
    {
        _rb.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
