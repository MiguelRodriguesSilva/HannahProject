using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVida : MonoBehaviour
{
    [Header("Quantidade de Vida")]
    public int vidaMax = 200;
    public int vidaAtual;

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

    public void Dano(int quantiDano)
    {
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
                StopAllCoroutines();
                StartCoroutine(DanoCritico());
                vidaAtual -= quantiDano;
            }
            else if (danoCritou == false)
            {
                vidaAtual -= quantiDano;
                _barraCritico.localScale = new Vector2(_porcent * vidaAtual, _barraCritico.localScale.y);
            }
            
        }

        if (_barraCritico.localScale.x == _porcent * vidaAtual)
        {
            danoCritou = false;
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

}
