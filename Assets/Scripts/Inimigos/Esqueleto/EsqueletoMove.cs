using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoMove : MonoBehaviour
{
    private Animator anim;
    [SerializeField] LayerMask lPlayer;
    [SerializeField] float danoAttack;
    [SerializeField] float distanceVision;
    [SerializeField] float rangeAttack;
    [SerializeField] float rangeFlip;
    [SerializeField] float alturaOlhos;
    [SerializeField] float speed;
    private RaycastHit2D hitLeft, hitRight;
    private SpriteRenderer sr;
    private bool estaVendo = false;
    private Vector3 _Player;
    private PlayerVida playerVida;
    public bool estaAtacando;
    private Collider2D co;
    private bool jaChecou;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerVida = FindObjectOfType<PlayerVida>();
        co = GetComponent<EdgeCollider2D>();

    }

    private void FixedUpdate()
    {
        Movimento();
        Attack();
        
    }

    void Movimento()
    {
        hitLeft = Physics2D.Raycast(transform.position + new Vector3 (0 , alturaOlhos, 0), Vector2.left, distanceVision, lPlayer);
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0, alturaOlhos, 0), Vector2.right, distanceVision, lPlayer);


        if (estaVendo == false)
        {
            if (transform.rotation == new Quaternion(0, 180, 0, co.transform.rotation.w))
            {
                if (hitLeft.collider != null)
                {
                    _Player = hitLeft.collider.transform.position;
                    StartCoroutine(ViuPersonagem());
                    estaVendo = true;
                }
            }
            else if (hitLeft.collider != null && hitLeft.distance < rangeFlip)
            {
                transform.rotation = new Quaternion(0, 180, 0, co.transform.rotation.w);
            }
            
            if (transform.rotation == new Quaternion(0, 0, 0, co.transform.rotation.w))
            {
                if (hitRight.collider != null)
                {
                    StartCoroutine(ViuPersonagem());
                    estaVendo = true;
                }
            }
            else if (hitRight.collider != null && hitRight.distance < rangeFlip)
            {
                transform.rotation = new Quaternion(0, 0, 0, co.transform.rotation.w);
            }


        }
        else if (estaVendo == true)
        {
            if (hitLeft.collider != null)
            {
                if (estaAtacando == false)
                {
                    transform.rotation = new Quaternion(0, 180, 0, co.transform.rotation.w);
                }
                
                _Player = hitLeft.collider.transform.position;
            }

            if (hitRight.collider != null)
            {
                if (estaAtacando == false)
                {
                    transform.rotation = new Quaternion(0, 0, 0, co.transform.rotation.w);
                }
                _Player = hitRight.collider.transform.position;
            }


        }

        if (hitLeft.collider == null && hitRight.collider == null)
        {
            estaVendo = false;
        }


        Debug.DrawRay(transform.position + new Vector3(0, alturaOlhos, 0), Vector2.right * distanceVision, Color.red);

        Vector2 direction = (_Player - transform.position).normalized;
        if (anim.GetBool("estaAndando") == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (estaAtacando == false && hitLeft.collider != null && hitLeft.distance < rangeAttack || estaAtacando == true)
        {
            anim.SetBool("estaAndando", false);

        }
        else if (estaVendo && estaAtacando == false && jaChecou == true)
        {
            anim.SetBool("estaAndando", true);
        }
       

        if ((estaAtacando == false && hitRight.collider != null && hitRight.distance < rangeAttack) || estaAtacando == true)
        {
            anim.SetBool("estaAndando", false);

        }
        else if (estaVendo && estaAtacando == false && jaChecou == true)
        {
            anim.SetBool("estaAndando", true);
        }

        if (direction.x > -0.1  && direction.x < 0.1)
        {
            anim.SetBool("estaAndando", false);
        }

    }

    IEnumerator ViuPersonagem()
    {
        jaChecou = false;
        anim.Play("ReactEsqueleto");
        yield return new WaitForSeconds(1f);
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            anim.SetBool("estaAndando", true);
        }
        jaChecou = true;
    }

    void Attack()
    {
        anim.SetBool("estaAtacando", estaAtacando);
        if (estaAtacando == false && hitLeft.collider != null && hitLeft.distance < rangeAttack)
        {
            anim.Play("AttackEsqueleto");
            estaAtacando = true;

        }

        if (estaAtacando == false && hitRight.collider != null && hitRight.distance < rangeAttack)
        {
            anim.Play("AttackEsqueleto");
            estaAtacando = true;

        }


    }

}
