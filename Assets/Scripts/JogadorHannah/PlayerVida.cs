using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVida : MonoBehaviour
{
    [Header("Quantidade de Vida")]
    public int vidaMax = 200;
    public int vidaAtual;

    [Header("Knockback")]
    [SerializeField] float intensidadeKB;
    private int _direction;
    private PlayerMove _player;
    private Vector3 inimigo;
    public float cooldownInvuneravel;
    private float _tempoInvuneravel;

    [Header("Barra De Vida")]
    private Transform _barraBase;
    private Transform _barraVida;
    private Transform _barraCritico;
    private float _porcent;
    private Text _quantiVida;
    private bool danoCritou;
    private Animator _animVida;
    
    

    private void Awake()
    {
        _barraBase = GameObject.Find("BarraFundo").transform;
        _barraVida = GameObject.Find("BarraDeVida").transform;
        _barraCritico = GameObject.Find("BarraCritico").transform;
        _quantiVida = GameObject.Find("TextoVida").GetComponent<Text>();
        _porcent = _barraBase.localScale.x / vidaMax;
        _animVida = GameObject.Find("Vida").GetComponent<Animator>();
        _player = GetComponent<PlayerMove>();
        _tempoInvuneravel = 0;
    }

    private void Update()
    {
        _barraVida.localScale = new Vector2(_porcent * vidaAtual, _barraVida.localScale.y);
        if (vidaAtual > vidaMax)
        {
            vidaAtual = vidaMax;
        }
        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        if (vidaAtual == 0)
        {
            _barraVida.gameObject.SetActive(false);
            if (_barraCritico.localScale.x == _porcent * vidaAtual)
            {
                _barraCritico.gameObject.SetActive(false);
            }
        }
        else
        {
            _barraVida.gameObject.SetActive(true);
            _barraCritico.gameObject.SetActive(true);
        }


        //Saber se critou ou não
        if (_barraCritico.localScale.x == _porcent * vidaAtual)
        {
            danoCritou = false;
        }

        _tempoInvuneravel += Time.deltaTime;



        _quantiVida.text = ("Vida: " + vidaAtual + "/" + vidaMax);


        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Dano(20);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Dano(300);
        }*/
    }

    public void MudancaVida(int maisVidaMax)
    {
        vidaMax += maisVidaMax;
        vidaAtual += maisVidaMax;
        _porcent = _barraBase.localScale.x / vidaMax;
    }

    public void Dano(int quantiDano, Vector3 inim)
    {
        if(_tempoInvuneravel > cooldownInvuneravel)
        {
            inimigo = inim;
            Knockback();
            if (quantiDano >= vidaMax / 100 * 20)
            {
                _animVida.Play("MexeMexe");
                StartCoroutine(DanoCritico());
                _barraCritico.localScale = new Vector2(_porcent * vidaAtual, _barraCritico.localScale.y);
                vidaAtual -= quantiDano;
                danoCritou = true;
            }
            else
            {
                if (danoCritou == true)
                {
                    _barraCritico.localScale = new Vector2(_barraCritico.localScale.x - (quantiDano * _porcent), _barraCritico.localScale.y);
                    if (_barraCritico.localScale.x < 0)
                    {
                        _barraCritico.localScale = new Vector2(_porcent * vidaAtual, _barraCritico.localScale.y);
                    }
                    StopCoroutine(DanoCritico());
                    StartCoroutine(DanoCritico());
                    vidaAtual -= quantiDano;
                }
                else if (danoCritou == false)
                {
                    vidaAtual -= quantiDano;
                    _barraCritico.localScale = new Vector2(_porcent * vidaAtual, _barraCritico.localScale.y);
                }

            }

            _tempoInvuneravel = 0;

        }
        
    }

    IEnumerator DanoCritico()
    {
        yield return new WaitForSeconds(1.5f);
        while (_barraCritico.localScale.x != _barraVida.localScale.x)
        {
            _barraCritico.localScale = new Vector2(_barraCritico.localScale.x - _porcent, _barraCritico.localScale.y);
            if (_barraCritico.localScale.x < _barraVida.localScale.x)
            {
                _barraCritico.localScale = new Vector2(_barraVida.localScale.x, _barraCritico.localScale.y);
            }
            yield return new WaitForSeconds(Time.deltaTime / 1.3f);
        }
        
    }

    public void Cura(int quantiCura)
    {
        vidaAtual += quantiCura;
    }

    public void Knockback()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2( 0, 0);
        if (inimigo.x > transform.position.x)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
        _player.podeMover = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1*_direction, 0.3f) * intensidadeKB, ForceMode2D.Impulse);
        StartCoroutine(tempoKB());
        
    }

    IEnumerator tempoKB()
    {
        yield return new WaitForSeconds(0.2f);
        _player.podeMover = true;
    }

}
