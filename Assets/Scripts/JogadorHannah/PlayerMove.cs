using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimentacao")]
    [SerializeField] float velocidadeHannah = 5;
    [SerializeField] float alturaPulo = 5;
    public bool spriteInvertido = false;
    private Rigidbody2D rgHannah;
    private SpriteRenderer srHannah;
    private bool isFalling = false;
    private InputJogador action;

    // Start is called before the first frame update

    private void Awake()
    {
        srHannah = GetComponent<SpriteRenderer>();
        action = GetComponent<InputJogador>();
        rgHannah = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimentacao();
    }

    private void Update()
    {
        if (isFalling == false)
        {
            if (action.input.Player.Pulo.WasPressedThisFrame())
            {
                rgHannah.AddForce(transform.up * alturaPulo * 100);
            }

        }

        spriteInvertido = GetComponent<SpriteRenderer>().flipX;
        
    }

    void Movimentacao()
    {

        float x = action.input.Player.Movimento.ReadValue<float>();
        transform.Translate(Vector3.right * x * velocidadeHannah * Time.deltaTime);

        if (x < 0)
        {
            srHannah.flipX = true;
        }
        else if (x > 0)
        {
            srHannah.flipX = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "chao")
        {
            isFalling = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "chao")
        {
            isFalling = true;
        }   
    }
}
