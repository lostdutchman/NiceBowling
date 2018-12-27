using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMultiplayer : MonoBehaviour {

    public int numberOfPlayers = 1;
    public List<PlayerScores> scoreCard = new List<PlayerScores>();
    public GameObject P1Total, P2Total, P3Total, P4Total, P5Total, P6Total, currentPlayerNameBox;
    public RawImage[] Boards;
    public GameObject[] ScoreBoards;
    public Text P1Name, P2Name, P3Name, P4Name, P5Name, P6Name;
    private Text currentPlayerName;
    int currentPlayer = 1;


	void Start () {
        //Initiate Score cards
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            var player = new PlayerScores();
            player.player = i;
            player.playerName = "Player " + i;
            player.bowls = new List<int>();
            player.bowlTexts = new Text[21];
            player.frameTexts = new Text[10];
            player.ScoreBoard = ScoreBoards[i - 1];
            scoreCard.Add(player);
        }
        foreach(var Player in scoreCard)
        {
            int bowl = 0;
            int frame = 0;
            foreach (Text T in Player.ScoreBoard.GetComponentsInChildren<Text>())
            {
                if (T.name == "Score")
                {
                    Player.frameTexts[frame] = T;
                    Player.frameTexts[frame].text = " ";
                    frame++;
                }
                else if (T.name == "BowlA" || T.name == "BowlB" || T.name == "BowlC")
                {
                    Player.bowlTexts[bowl] = T;
                    Player.bowlTexts[bowl].text = " ";
                    bowl++;
                }
            }
        }
        //Initiate gamespace
        currentPlayer = 1;
        currentPlayerName = currentPlayerNameBox.GetComponentInChildren<Text>();
        for (int i = 1; i < Boards.Length; i++)
        {
            Boards[i].canvasRenderer.SetAlpha(0f);
        }

        switch (numberOfPlayers)
        {
            case 2:
                currentPlayerNameBox.SetActive(true);
                currentPlayerName.text = scoreCard[0].playerName + " Bowl!";
                P1Total.SetActive(true);
                P1Name.text = scoreCard[0].playerName + ":";
                P2Total.SetActive(true);
                P2Name.text = scoreCard[1].playerName + ":";
                break;
            case 3:
                P3Total.SetActive(true);
                P3Name.text = scoreCard[2].playerName + ":";
                goto case 2;
            case 4:
                P4Total.SetActive(true);
                P4Name.text = scoreCard[3].playerName + ":";
                goto case 3;
            case 5:
                P5Total.SetActive(true);
                P5Name.text = scoreCard[4].playerName + ":";
                goto case 4;
            case 6:
                P6Total.SetActive(true);
                P6Name.text = scoreCard[5].playerName + ":";
                goto case 5;
            default: break;
        }
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void NextPlayer()
    {
        StartCoroutine(FadePlayerChange(currentPlayer - 1));
        if (currentPlayer == numberOfPlayers)
        {
            currentPlayer = 0;
        }
        currentPlayer++;
    }

    private IEnumerator FadePlayerChange(int index)
    {
        //Allow score to fill in befors starting the animations
        yield return new WaitForSecondsRealtime(1f);
        Boards[index].CrossFadeAlpha(0, .5f, false);
        currentPlayerName.CrossFadeAlpha(0, .5f, false);
        yield return new WaitForSecondsRealtime(.55f);
        index++;
        if (index == numberOfPlayers)
        {
            index = 0;
        }
        currentPlayerName.text = scoreCard[index].playerName + " Bowl!";
        currentPlayerName.CrossFadeAlpha(1, .5f, false);
        Boards[index].CrossFadeAlpha(1, .5f, false);
    }
}
