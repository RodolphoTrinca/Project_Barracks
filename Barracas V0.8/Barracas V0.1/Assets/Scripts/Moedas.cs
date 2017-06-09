using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moedas : MonoBehaviour {

    //Public variables
    [HideInInspector]
    public Vector3 posicaoBarraca;
    [Tooltip("Speed ​​as the coins go towards the barracks that was selected to invest.")]
    public float velocidadeDeMovimento = 2;
    [Tooltip("Name parameter that will release the animation.")]
    public string nomeParametroAnimator = "Investiu";
    [Tooltip("Distance from barracks to disable coins.")]
    public float distanciaParaBarraca = 0.5f;
    [Tooltip("Initial position of coins.")]
    public Transform posicaoInicial;

    //Private Variables
    bool anima = false;
    Animator animMoeda;

    void Awake()
    {
        //Ref the animator component
        animMoeda = GetComponent<Animator>();

        //Guarda a posicao inicial
        posicaoInicial.position = transform.position;
    }

    void Update () {
        if (anima)
        {
            //Movimenta as moedas em direção a barraca selecionada
            transform.position = Vector3.Lerp(transform.position, posicaoBarraca, velocidadeDeMovimento * Time.deltaTime);

            //Libera a animação de rotação
            animMoeda.SetBool(nomeParametroAnimator, true);

            //Reseta animators
            StartCoroutine(AnimMoedasRoutine());

            if (Vector3.Distance(transform.position, posicaoBarraca) < distanciaParaBarraca)
            {
                anima = false;
                transform.position = posicaoInicial.position;
            }
        }
    }

    //Chama animações no tempo pre determinado 
    public void SoltaAnimacao(float tempo, Vector3 posicao)
    {
        posicaoBarraca = posicao;
        transform.position = posicaoInicial.position;
        anima = false;
        StartCoroutine(SoltaAnimRoutine(tempo));
    }

    IEnumerator AnimMoedasRoutine()
    {
        yield return new WaitForFixedUpdate();
        animMoeda.SetBool(nomeParametroAnimator, false);
    }
    
    IEnumerator SoltaAnimRoutine(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        anima = true;
    }
}
