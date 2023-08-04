using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHannah : MonoBehaviour
{
    public float velocidadeHannah = 5;
    public float alturaPulo = 5;
    Rigidbody2D rgHannah;
    private bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rgHannah.AddForce(transform.up * alturaPulo * 100);
            }

        }
        
    }

    void Movimentacao()
    {

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * velocidadeHannah * horizontalMove * Time.deltaTime);
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
