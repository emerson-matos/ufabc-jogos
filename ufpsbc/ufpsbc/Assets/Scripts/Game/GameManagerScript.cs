using MLAPI;
using MLAPI.NetworkVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : NetworkBehaviour
{
    private bool gameStarted;
    private NetworkVariableFloat gameCountDown;
    private bool bombPlanted;
    private float bombCountDown;
    private NetworkVariableBool isRoundFinished;

    private NetworkVariableInt roundsCT;
    private NetworkVariableInt roundsTR;
    private int roundNumber;

    //private NetworkVariableString scoreStringFormat;
    //private NetworkVariableString roundStringFormat;
    private string scoreStringFormat;
    private string roundStringFormat;

    public Text time;
    public Text victoryMessage;
    public Text score;
    public Text RoundCount;

    public GameObject HudInfoCanvas;

    public static GameManagerScript Instance;

    private void Awake()
    {
        Instance = this;

        gameStarted = false;

        gameCountDown =  new NetworkVariableFloat(0);
        isRoundFinished = new NetworkVariableBool(true);

        roundsCT = new NetworkVariableInt(0);
        roundsTR = new NetworkVariableInt(0);
        //scoreStringFormat = new NetworkVariableString("CT {0} | {1} TR");
        //roundStringFormat = new NetworkVariableString("Round {0}");
        bombPlanted = false;
        roundNumber = 1;
        scoreStringFormat = "CT {0} | {1} TR";
        roundStringFormat = "Round {0}";
        HudInfoCanvas.SetActive(false);
    }

    private void Update()
    {
        if(gameCountDown.Value > 0)
        {
            this.gameCountDown.Value -= Time.deltaTime;
        }
        else
        {
            this.gameCountDown.Value = 0f;
            if (!bombPlanted && !isRoundFinished.Value)
            {
                ResetGame(Constants.TEAM.COUNTERTERRORISTS);
            }
        }

        time.text = gameCountDown.Value.ToString("0.00", CultureInfo.InvariantCulture);
        score.text = String.Format(scoreStringFormat, roundsCT, roundsTR);
    }

    public void StartHud()
    {
        HudInfoCanvas.SetActive(true);
    }

    private void ResetGame(Constants.TEAM winner)
    {
        StartCoroutine(ResetarUI(winner));
        ResetPositions();
        //travar o jogo por 5 segundos [OPCIONAL]
        //liberar a movimentação e inicio do novo round
    }

    public void ResetPositions()
    {
        List<Player> players = FindObjectsOfType<Player>().ToList();
        players.ForEach(x => x.ResetarPosicao());
        gameCountDown.Value = Constants.TIME_TO_PLANT_BOMB;
        isRoundFinished.Value = false;

    }

    private IEnumerator ResetarUI(Constants.TEAM winner)
    {
        isRoundFinished.Value = true;
        switch (winner)
        {
            case Constants.TEAM.COUNTERTERRORISTS:
                //vitoria dos CT
                roundsCT.Value++;
                victoryMessage.text = "Os contra-terroristas venceram!";
                break;
            case Constants.TEAM.TERRORISTS:
                //vitoria dos TR
                roundsTR.Value++;
                victoryMessage.text = "Os terroristas venceram!";
                break;
        }

        yield return new WaitForSeconds(5f);

        victoryMessage.text = string.Empty;
    }
}
