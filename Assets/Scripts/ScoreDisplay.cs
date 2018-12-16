using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    [Tooltip("This should be the object inside the scoreboard that holds each frame")]
    private GameManager gameManager;
    private LocalMultiplayer multiplayer;

    void Start () {
        gameManager = GetComponent<GameManager>();
        multiplayer = GetComponent<LocalMultiplayer>();

        for (int i = 1; i <= multiplayer.numberOfPlayers; i++)
        {
            var player = new PlayerScores();
            player.player = i;
            player.bowls = new List<int>();
            player.bowlTexts = new Text[21];
            player.frameTexts = new Text[10];
            gameManager.scoreCard.Add(player);
        }

        int card = 0;
        foreach(GameObject SB in GameObject.FindGameObjectsWithTag("ScoreBoard"))
        {
            int bowl = 0;
            int frame = 0;
            foreach(Text T in SB.GetComponentsInChildren<Text>())
            {
                if (T.name == "Score")
                {
                    gameManager.scoreCard[card].frameTexts[frame] = T;
                    gameManager.scoreCard[card].frameTexts[frame].text = " ";
                    frame++;
                }
                else if(T.name == "BowlA" || T.name == "BowlB" || T.name == "BowlC")
                {
                    gameManager.scoreCard[card].bowlTexts[bowl] = T;
                    gameManager.scoreCard[card].bowlTexts[bowl].text = " ";
                    bowl++;
                }
            }
        }
	}
		
//Fills in the number of pins knocked over every turn
	public void FillBowls (List<int> bowls){
		string scoreString = FormatBowls (bowls);

        for (int i = 0; i < scoreString.Length; i++)
        {
            gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].bowlTexts[i].text = scoreString[i].ToString();
            if (i == scoreString.Length - 1)
            {
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].bowlTexts[i].canvasRenderer.SetAlpha(.01f);
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].bowlTexts[i].CrossFadeAlpha(1, 1, false);
            }
        }
	}

//Fills in the score for each frame
	public void FillFrames (List<int> frames){
		for (int i = 0; i < frames.Count; i++) {
            if (i == frames.Count - 1 && gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].frameTexts[i].text == " ")
            {
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].frameTexts[i].text = frames[i].ToString();
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].frameTexts[i].canvasRenderer.SetAlpha(.01f);
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].frameTexts[i].CrossFadeAlpha(1, 1.5f, false);
            }
            else
            {
                gameManager.scoreCard[multiplayer.GetCurrentPlayer() - 1].frameTexts[i].text = frames[i].ToString();
            }
            ScoreMaster.endScore = frames[i];
		}
    }

    public static string FormatBowls (List<int> Bowls){
		PinSetter pinSetter = GameObject.FindObjectOfType<PinSetter> (); //Needed for the audio(voice acting)
		
		string output = "";

		for (int i = 0; i < Bowls.Count; i++) {

			int box = output.Length + 1; //Tracks which score box we are working with, 1-21

			if (Bowls[i] == 0) {  //Gutter
				output += "-";
				pinSetter.PlayAudio(4);
			} else if (box % 2 == 0 && (Bowls [i - 1] + Bowls [i] == 10)){  //Spare
				output += "/";
				pinSetter.PlayAudio (2);
			}else if(box >= 19 && Bowls[i] == 10){ //Strike in last frame
				output += "X";
					if(Bowls[i] + Bowls[i-1] + Bowls[i-2] == 30){ //determins if there is a turkey
						pinSetter.PlayAudio (3);
							} else {pinSetter.PlayAudio (1);
						}
			}else if(Bowls[i] == 10){   //Strike
				output += "X ";
				if(box >= 5){
					if(Bowls[i] + Bowls[i-1] + Bowls[i-2] == 30){ //determins if there is a turkey
						pinSetter.PlayAudio (3);
					} else {pinSetter.PlayAudio (1);
					}
				}else {pinSetter.PlayAudio (1);
				}
			}else {
				output += Bowls [i].ToString (); //1-9 bowl
				pinSetter.PlayAudio (5);//triggers default to clear audio.
				}
		}

		return output;
	}
}
