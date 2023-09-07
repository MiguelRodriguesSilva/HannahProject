using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVida : MonoBehaviour
{
    [SerializeField] int vidaAtuInimigo, vidaMaxInimigo;

    private void Start()
    {
        vidaAtuInimigo = vidaMaxInimigo;
    }

    private void Update()
    {
        if (vidaAtuInimigo > vidaMaxInimigo)
        {
            vidaAtuInimigo = vidaMaxInimigo;
        }

        if (vidaAtuInimigo < 0)
        {
            vidaAtuInimigo = 0;
        }
    }

    public void Dano(int danoAttackPlayer)
    {
        vidaAtuInimigo -= danoAttackPlayer;
    }
}
