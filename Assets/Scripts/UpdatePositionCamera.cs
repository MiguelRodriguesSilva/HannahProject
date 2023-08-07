using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdatePositionCamera : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    Transform reajuste;
    public float velocidadeCamera;
    public Transform pontoMin, pontoMax;
    public bool estaNoLimite;
    public string direcao;

    private void Start()
    {
        transform.position = player.position; 
    }
    private void FixedUpdate()
    {
        if ( estaNoLimite == false )
        CameraSmooth();
        
    }

    private void Update()
    {
        if ( estaNoLimite == false)
        {
            if (player.position.x < pontoMin.position.x)
            {
                direcao = "Esquerda";
            }

            if (player.position.x > pontoMin.position.x)
            {
                direcao = "Direita";
            }
        }

        if(estaNoLimite == true || player.position.x > transform.position.x || direcao == "Esquerda")
        {
            estaNoLimite = false;
        }

        if (estaNoLimite == true || player.position.x < transform.position.x || direcao == "Direta")
        {
            estaNoLimite = false;
        }


    }


    void CameraSmooth()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, velocidadeCamera);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BarreiraCamera")
        {
            estaNoLimite = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BarreiraCamera")
        {

        }
    }
}
