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
    float direcaoCamera = 1;

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
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            direcaoCamera = Input.GetAxisRaw("Horizontal") * 0.3f;
        }

        float pontoAjusteBarreira = 27f;

        
        Vector2 posicaoLerp = Vector2.Lerp(transform.position, player.position, velocidadeCamera);

        transform.position = new Vector2(posicaoLerp.x + direcaoCamera, transform.position.y);
        


        if (hitUp.collider == null)
        {
            transform.position = new Vector2(transform.position.x, posicaoLerp.y);
        }

        if (hitLeft.collider != null)
        {
            if (transform.position.x < hitLeft.collider.transform.position.x + pontoAjusteBarreira)
            {
                transform.position = new Vector2(hitLeft.collider.transform.position.x + pontoAjusteBarreira, transform.position.y);
                
            }
        }

        if (hitRight.collider != null)
        {
            if (transform.position.x > hitRight.collider.transform.position.x - pontoAjusteBarreira)
            {
                transform.position = new Vector2(hitRight.collider.transform.position.x - pontoAjusteBarreira, transform.position.y);
            }
        }
    }
}
