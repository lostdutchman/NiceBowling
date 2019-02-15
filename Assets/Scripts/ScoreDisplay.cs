using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

    public Text[] Totals;
    private LocalMultiplayer multiplayer;

    void Start () {
        multiplayer = GetComponent<LocalMultiplayer>();
	}
		
//Fills in the number of pins knocked over every turn
	public void FillBowls (List<int> bowls){
		string scoreString = FormatBowls (bowls);
        int index = multiplayer.GetCurrentPlayer() - 1;
        for (int i = 0; i < scoreString.Length; i++)
        {
            try
            {
                multiplayer.scoreCard[index].bowlTexts[i].text = scoreString[i].ToString();
                if (i == scoreString.Length - 1)
                {
                    multiplayer.scoreCard[index].bowlTexts[i].canvasRenderer.SetAlpha(.01f);
                    multiplayer.scoreCard[index].bowlTexts[i].CrossFadeAlpha(1, .25f, false);
                }
            }
            catch
            {
                Debug.Log("ScoreDisplay.FillBowls Failed. Bowl=" + i + " PlayerIndex=" + index);
            }
        }
	}

//Fills in the score for each frame
	public void FillFrames (List<int> frames){
        int index = multiplayer.GetCurrentPlayer() - 1;
		for (int i = 0; i < frames.Count; i++) {
            if (i == frames.Count - 1 && multiplayer.scoreCard[index].frameTexts[i].text == " ")
            {
                multiplayer.scoreCard[index].frameTexts[i].text = frames[i].ToString();
                multiplayer.scoreCard[index].frameTexts[i].canvasRenderer.SetAlpha(.01f);
                multiplayer.scoreCard[index].frameTexts[i].CrossFadeAlpha(1, .5f, false);
                Totals[index].text = frames[i].ToString();
                Totals[index].canvasRenderer.SetAlpha(.01f);
                Totals[index].CrossFadeAlpha(1, .75f, false);
            }
            else
            {
                multiplayer.scoreCard[index].frameTexts[i].text = frames[i].ToString();
            }
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
