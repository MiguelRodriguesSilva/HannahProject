using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJogador : MonoBehaviour
{
    public ActionInputsPlayer input;

    private void Awake()
    {
        input = new ActionInputsPlayer();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

}
