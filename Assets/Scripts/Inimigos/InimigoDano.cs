using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDano : MonoBehaviour
{
    [SerializeField] int danoInimigo;
    private PlayerVida playerVida;
    [SerializeField] Collider2D hitBox;
    [SerializeField] Collider2D player;
    public float cooldownDano;
    private float _tempoDano;


    private void Awake()
    {
        playerVida = FindObjectOfType<PlayerVida>();
        player = FindObjectOfType<PlayerVida>().GetComponent<Collider2D>();
    }

    private void Start()
    {
        _tempoDano = 0;
    }

    private void Update()
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
