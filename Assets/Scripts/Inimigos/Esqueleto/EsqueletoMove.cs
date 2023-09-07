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
    private float _scaleX;
    private Vector2 direction;
    private Vector2 dir;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerVida = FindObjectOfType<PlayerVida>();
        co = GetComponent<EdgeCollider2D>();
        

    }

    private void Start()
    {
        _scaleX = transform.localScale.x;

    }

    private void FixedUpdate()
    {
        direction = (_Player - transform.position).normalized;
        dir = (FindObjectOfType<PlayerMove>().transform.position - transform.position).normalized;
        hitLeft = Physics2D.Raycast(transform.position + new Vector3(0, alturaOlhos, 0), new Vector2(-1, dir.y ), distanceVision, lPlayer);
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0, alturaOlhos, 0), new Vector2(1, dir.y), distanceVision, lPlayer);
        Movimento();
        Attack();
    }

    void Movimento()
    {
        if (estaVendo == false)
        {
            if (transform.localScale.x == _scaleX && hitLeft.collider != null && hitLeft.distance < rangeFlip && estaAtacando == false)
            {
                transform.localScale = new Vector3(-_scaleX, transform.localScale.y, transform.localScale.z);
                estaVendo = true;
                StartCoroutine(ViuPersonagem());
            }
            else if (transform.localScale.x != _scaleX && hitRight.collider != null && hitRight.distance < rangeFlip && estaAtacando == false)
            {
                transform.localScale = new Vector3(_scaleX, transform.localScale.y, transform.localScale.z);
                estaVendo = true;
                StartCoroutine(ViuPersonagem());
            }
            
            if (transform.localScale.x == _scaleX && hitRight.collider != null)
            {
                StartCoroutine(ViuPersonagem());
                estaVendo = true;
            }
            else if(transform.localScale.x != _scaleX && hitLeft.collider != null)
            {
                StartCoroutine(ViuPersonagem());
                estaVendo = true;
            }
        }
        else if (estaVendo == true)
        {
            if (hitLeft.collider != null)
            {
                _Player = hitLeft.collider.transform.position;
            }

            if (hitRight.collider != null)
            {
                _Player = hitRight.collider.transform.position;
            }
        }

        if (hitRight.collider == null && hitLeft.collider == null)
        {
            estaVendo = false;
        }

        if (anim.GetBool("estaAndando") == true)
        {
            transform.Translate(new Vector2(-direction.x, transform.position.y) * speed * Time.deltaTime);
        }

        if (estaAtacando == false && hitLeft.collider != null && hitLeft.distance <= rangeAttack || estaAtacando == true)
        {
            anim.SetBool("estaAndando", false);

        }
        else if (estaVendo && estaAtacando == false && jaChecou == true)
        {
            anim.SetBool("estaAndando", true);
        }
       

        if ((estaAtacando == false && hitRight.collider != null && hitRight.distance <= rangeAttack) || estaAtacando == true)
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

        Debug.DrawRay(transform.position + new Vector3(0, alturaOlhos, 0), dir * distanceVision, Color.red);

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
