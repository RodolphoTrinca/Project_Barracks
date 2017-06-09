using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControleBarracas : MonoBehaviour {

    //Class
    [System.Serializable]
    public class Tags
    {
        [Tooltip("Object tag with the money control script.")]
        public string controleDinheiro = "MainCamera";
        [Tooltip("Name of the barracks that multiplies the value of the player.")]
        public string barracaDeMultiplicacao = "A";
    }
    [Tooltip("Classe com as tags para funcionar o jogo")]
    public Tags tagNames;
    [Space]

    //Public Variables
    [Tooltip("Index that will determine the behavior of the barrack.")]
    public int indexBehavior;
    [Tooltip("Barrack`s name")]
    public string nomeBarracas;
    [Tooltip("Time in seconds that Unity will release another user click.")]
    public float tempoPorClick = 0.25f;
    [Space]
    [Tooltip("Scripts to animate the coins.")]
    public Moedas[] moedas;

    public enum buildProject { Android, WebGL};
    public buildProject targetBuild;

    [Space]
    [Tooltip("Text mesh to player know the ammount of profit.")]
    public TextMeshPro textMeshProfit;
    [Tooltip("Text mesh to player know the ammount of loss.")]
    public TextMeshPro textMeshLoss;
    [Tooltip("Time in seconds the text mesh is showing in screen")]
    public float timeFeedbackProfit = 0.5f;

    [Space]
    [Tooltip("Text mesh to player know the ammount of profit.")]
    public Text textProfit;
    [Tooltip("Text mesh to player know the ammount of loss.")]
    public Text textLoss;

    //Private Variables
    ControleDinheiro controleDinheiro;
    int[] valoresBarraca;
    int indexValores;
    bool clickRoutine = true;

    void Awake()
    {
        //Refers the script of control canvas
        controleDinheiro = GameObject.FindGameObjectWithTag(tagNames.controleDinheiro).GetComponent<ControleDinheiro>();
    }

	void Start () {
        indexValores = 0;
        valoresBarraca = new int[controleDinheiro.valoresBarracaA.Length];
        //Disable from screen the text mesh
        textMeshProfit.enabled = false;
        textMeshLoss.enabled = false;
        textProfit.enabled = false;
        textLoss.enabled = false;

        //Determine the behavior of barrack
        nomeBarracas = controleDinheiro.compBarracks[controleDinheiro.randomBarracks[indexBehavior]];
        PreencheArray(nomeBarracas);
    }

    //Fills the array with behavior values
    void PreencheArray(string n)
    {
        if (n == "A")
        {
            for (int i = 0; i < controleDinheiro.valoresBarracaA.Length; i++)
            {
                valoresBarraca[i] = controleDinheiro.valoresBarracaA[i];
            }
            return;
        }
        else if (n == "B")
        {
            for (int i = 0; i < controleDinheiro.valoresBarracaB.Length; i++)
            {
                valoresBarraca[i] = controleDinheiro.valoresBarracaB[i];
            }
            return;
        }
        else if (n == "C")
        {
            for (int i = 0; i < controleDinheiro.valoresBarracaC.Length; i++)
            {
                valoresBarraca[i] = controleDinheiro.valoresBarracaC[i];
            }
            return;
        }
    }

    //Call function when the player clicks on the barracks
    void OnMouseDown () {
		if (clickRoutine)
        {
            //save player ammount to calc his profit or loss 
            var playerAmmount = controleDinheiro.dinheiro;

            //Set boolean to false to avoid unwanted clicks
            clickRoutine = false;
            StartCoroutine(ClickRoutine());

            //If the attempts are finished, the end-of-game image
            if (controleDinheiro.quantidadeDeJogadas <= 0)
            {
                controleDinheiro.imagemFimDeJogo.enabled = true;
                return;
            }

            //If it is the barracks "A" multiply the value by 2
            else if (nomeBarracas == tagNames.barracaDeMultiplicacao && valoresBarraca[indexValores] == 2)
            {
                controleDinheiro.dinheiro *= 2;
            }

            //Takes the values ​​by the index and subtracts / adds to the amount of the player
            else
            {
                controleDinheiro.dinheiro += valoresBarraca[indexValores];
            }

            //Anim coins
            for (int i = 0; i < moedas.Length; i++)
            {
                moedas[i].SoltaAnimacao(0.1f * (i + 1), transform.position + Vector3.up * -1.2f);
            }

            //Shows Profit/Loss player
            var profitLoss = controleDinheiro.dinheiro - playerAmmount;
            if (Mathf.Sign(profitLoss) > 0)
            {
                if (targetBuild == buildProject.Android)
                {
                    textProfit.text = string.Format("$ +{0}", profitLoss);
                    if (textLoss.enabled)
                    {
                        textLoss.enabled = false;
                    }
                    textProfit.enabled = true;
                    StartCoroutine(TextMeshRoutine());
                }
                else if (targetBuild == buildProject.WebGL)
                {
                    textMeshProfit.text = string.Format("$ +{0}", profitLoss);
                    if (textMeshLoss.enabled)
                    {
                        textMeshLoss.enabled = false;
                    }
                    textMeshProfit.enabled = true;
                    StartCoroutine(TextMeshRoutine());
                }
            }
            else
            {
                if (targetBuild == buildProject.Android)
                {
                    textLoss.text = string.Format("$ {0}", profitLoss);
                    if (textProfit.enabled)
                    {
                        textProfit.enabled = false;
                    }
                    textLoss.enabled = true;
                    StartCoroutine(TextMeshRoutine());
                }
                else if (targetBuild == buildProject.WebGL)
                {
                    textMeshLoss.text = string.Format("$ {0}", profitLoss);
                    if (textMeshProfit.enabled)
                    {
                        textMeshProfit.enabled = false;
                    }
                    textMeshLoss.enabled = true;
                    StartCoroutine(TextMeshRoutine());
                }
            }

            indexValores++;
            controleDinheiro.quantidadeDeJogadas--;
        }
	}

    IEnumerator ClickRoutine()
    {
        yield return new WaitForSeconds(tempoPorClick);
        clickRoutine = true;
    }

    IEnumerator TextMeshRoutine()
    {
        yield return new WaitForSeconds(timeFeedbackProfit);
        textMeshProfit.enabled = false;
        textMeshLoss.enabled = false;
        textLoss.enabled = false;
        textProfit.enabled = false;
    }
}
