using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionCamera : MonoBehaviour
{
    public Transform player;
    public float velocidadeCamera;
    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, velocidadeCamera);
    }
}
