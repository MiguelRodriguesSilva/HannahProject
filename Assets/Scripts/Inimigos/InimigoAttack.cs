using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoAttack : MonoBehaviour
{
    [SerializeField] int danoInimigo;
    private PlayerVida playerVida;
    [SerializeField] Collider2D hitBox;
    [SerializeField] Collider2D player;

    private void Awake()
    {
        playerVida = FindObjectOfType<PlayerVida>();
        player = FindObjectOfType<PlayerVida>().GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        if (hitBox.IsTouching(player))
        {
            playerVida.Dano(danoInimigo, transform.position);
        }

    }

    /*private void OnTriggerStay2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            podeTomarDano = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            podeTomarDano = false;
        }
    }*/
}
