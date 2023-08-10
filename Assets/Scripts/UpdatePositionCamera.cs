using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdatePositionCamera : MonoBehaviour
{
    public Transform player;
    Transform reajuste;
    public float velocidadeCamera;
    RaycastHit2D hitUp;
    RaycastHit2D hitDown;
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;
    public float distanceUpDown, distanceRightLeft;
    public LayerMask blockCamera;

    private void Start()
    {
        transform.position = player.position; 
    }
    private void FixedUpdate()
    {

        CameraSmooth();   

    }

    private void Update()
    {
        hitUp = Physics2D.Raycast(player.position, Vector2.up, distanceUpDown, blockCamera);
        hitDown = Physics2D.Raycast(player.position, Vector2.down, distanceUpDown, blockCamera);
        hitRight = Physics2D.Raycast(player.position, Vector2.right, distanceRightLeft, blockCamera);
        hitLeft = Physics2D.Raycast(player.position, Vector2.left, distanceRightLeft, blockCamera);
        

    }


    void CameraSmooth()
    {
        Vector2 posicaoLerp = Vector2.Lerp(transform.position, player.position, velocidadeCamera);

        if (hitRight.collider == null && hitLeft.collider == null)
        {
            transform.position = new Vector2(posicaoLerp.x, transform.position.y);
        }

        if (hitUp.collider == null)
        {
            transform.position = new Vector2(transform.position.x, posicaoLerp.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BarreiraCamera")
        {

        }
    }
}
