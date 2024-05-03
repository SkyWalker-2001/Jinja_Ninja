using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue_BG : MonoBehaviour
{
    [Range(-3f, 3f)]
    public float speed = .1f;

    public Material myMaterial;

    public float offset_X;
    public float offset_Y;

    private Vector2 newOffset;

    private void Update()
    {
        newOffset.x += offset_X * speed * Time.deltaTime;
        newOffset.y += offset_Y * speed * Time.deltaTime;
        myMaterial.mainTextureOffset = newOffset;
    }
}
