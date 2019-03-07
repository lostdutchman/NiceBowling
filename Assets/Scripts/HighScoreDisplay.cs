using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class HighScoreDisplay : MonoBehaviour {

    private List<HighScores> scoreCards = new List<HighScores>();
    private List<PlayerScores> playerScores = new List<PlayerScores>();
    public GameObject[] ScoreBoards;
    public Text[] names;
    private HighScores highScores;
    private string[] npcs = new string[] { "Jeremy", "Sam", "Nina", "Tifa", "LostGold", "Suguaro", "BowlKing" };

    void Start()
    {
        highScores = new HighScores();
        scoreCards = highScores.GetHighScores();

        //Set up score cards in case there are no high scores and initialize player data
        for (int i = 0; i <= 4; i++)
        {
            var player = new PlayerScores();
            player.player = i;
            player.playerName = npcs[UnityEngine.Random.Range(0, npcs.Length)];
            player.bowls = new List<int>();
            player.bowlTexts = new Text[21];
            player.frameTexts = new Text[10];
            player.ScoreBoard = ScoreBoards[i];
            playerScores.Add(player);
        }
        foreach (var player in playerScores)
        {
            int bowl = 0;
            int frame = 0;
            names[player.player].text = player.playerName;
            foreach (Text T in player.ScoreBoard.GetComponentsInChildren<Text>())
            {
                if (T.name == "Score")
                {
                    player.frameTexts[frame] = T;
                    player.frameTexts[frame].text = "-";
                    frame++;
                }
                else if (T.name == "BowlA" || T.name == "BowlB" || T.name == "BowlC")
                {
                    player.bowlTexts[bowl] = T;
                    player.bowlTexts[bowl].text = "-";
                    bowl++;
                }
            }
        }

        //fill out each players high scores
        foreach (var player in playerScores)
        {
            if(scoreCards.Count > player.player)
            {
                names[player.player].text = scoreCards[player.player].playerName;
                FillBowls(scoreCards[player.player].bowls, player.player);
                FillFrames(ScoreMaster.ScoreCumulative(scoreCards[player.player].bowls), player.player);
            }
        }
    }

    //Fills in the number of pins knocked over every turn
    public void FillBowls(List<int> bowls, int playerIndex)
    {
        string scoreString = FormatBowls(bowls);
        for (int i = 0; i < scoreString.Length - 1; i++)
        {
            playerScores[playerIndex].bowlTexts[i].text = scoreString[i].ToString();
        }
    }

    //Fills in the score for each frame
    public void FillFrames(List<int> frames, int playerIndex)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            playerScores[playerIndex].frameTexts[i].text = frames[i].ToString();
        }
    }

    public static string FormatBowls (List<int> Bowls){
		string output = "";

		for (int i = 0; i < Bowls.Count; i++) {

			int box = output.Length + 1; //Tracks which score box we are working with, 1-21

            if (Bowls[i] == 0)
            {  //Gutter
                output += "-";
            }
            else if (box % 2 == 0 && (Bowls[i - 1] + Bowls[i] == 10))
            {  //Spare
                output += "/";
            }
            else if (box >= 19 && Bowls[i] == 10)
            { //Strike in last frame
                output += "X";

            }
            else if (Bowls[i] == 10)
            {   //Strike
                output += "X ";
            }
            else
            {
                output += Bowls[i].ToString(); //1-9 bowl
            }
		}
		return output;
	}
}
