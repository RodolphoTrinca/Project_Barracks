using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ControleDinheiro : MonoBehaviour{

    [HideInInspector]
    public int dinheiro = 5;
    [HideInInspector]
    public int quantidadeDeJogadas;
    [HideInInspector]
    public string[] compBarracks = new string[3] { "A", "B", "C" };
    [HideInInspector]
    public List<int> randomBarracks;

    [Tooltip("Control of values ​​that will be distributed to wiht the barracks A during the plays.")]
    public int[] valoresBarracaA = new int[36];
    [Tooltip("Control of values ​​that will be distributed to wiht the barracks B during the plays.")]
    public int[] valoresBarracaB = new int[36];
    [Tooltip("Control of values ​​that will be distributed to wiht the barracks C during the plays.")]
    public int[] valoresBarracaC = new int[36];

    [Space]
    [Tooltip("Space on the canvas where the amount of money will be presented to the player.")]
    public Text texto;
    [Tooltip("Image on the canvas to signal the player the end of the game.")]
    public Image imagemFimDeJogo;
    [Tooltip("Text on canvas that will indicate to the player the remaining amount of plays.")]
    public Text textoJogadas;

    [Space]
    [Tooltip("Positions from cam aspect ratio.")]
    public Transform[] cameraPositions;

    void Awake()
    {
        //Randomise the barracks
        for (int i = 0; i < compBarracks.Length; i++)
        {
            randomBarracks.Add(i);
        }
        for (int i = 0; i < randomBarracks.Count; i++)
        {
            int temp = randomBarracks[i];
            int randomIndex = Random.Range(i, randomBarracks.Count);
            randomBarracks[i] = randomBarracks[randomIndex];
            randomBarracks[randomIndex] = temp;
        }

        //Stores the number of plays to be indicated on the canvas
        quantidadeDeJogadas = valoresBarracaA.Length;


        if (Camera.main.aspect >= 1.7)
        {
            Debug.Log("16:9");
            transform.position = cameraPositions[0].position;
        }
        else if (Camera.main.aspect >= 1.5)
        {
            Debug.Log("3:2");
            transform.position = cameraPositions[1].position;
        }
        else
        {
            Debug.Log("4:3");
            transform.position = cameraPositions[2].position;
        }

        imagemFimDeJogo.enabled = false;
    }

    void Update() {
        //Update the amount of money in the player wallet
        texto.text = string.Format("Dinheiro: ${0}", dinheiro);
        //Updates the amount of remaining moves
        textoJogadas.text = string.Format("Jogadas restantes: {0}", quantidadeDeJogadas);
    }
}
