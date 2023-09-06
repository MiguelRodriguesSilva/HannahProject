using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdatePositionCamera : MonoBehaviour
{
    private SpriteRenderer srP;
    private Transform player;
    float direcaoCamera = 1;
    [SerializeField] float distanceUpDown, distanceRightLeft;
    [SerializeField] float velocidadeCamera;
    [SerializeField] LayerMask blockCamera;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitDown;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;

    private void Awake()
    {
        srP = FindObjectOfType<PlayerMove>().GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>().transform;

    }
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
        if (srP.flipX == false)
        {
            direcaoCamera = 0.3f;
        }
        else
        {
            direcaoCamera = -0.3f;
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
