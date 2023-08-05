using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionCamera : MonoBehaviour
{
    public Transform player;
    Transform reajuste;
    public float velocidadeCamera;
    public Transform pontoMin, pontoMax;

    private void Start()
    {
        transform.position = player.position; 
    }
    private void FixedUpdate()
    {
        if (player.position.x >)
        CameraSmooth();
        
    }

    void CameraSmooth()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, velocidadeCamera);
    }
}
