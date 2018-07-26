using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {

	public Text[]  bowlTexts, frameTexts;
	
	void Start () {
		for(int i = 0; i < 21; i++) {bowlTexts[i].text = " ";}
		for(int i = 0; i < 10; i++) {frameTexts[i].text = " ";}
		}
		
//Fills in the number of pins knocked over every turn
	public void FillBowls (List<int> bowls){
		string scoreString = FormatBowls (bowls);

        for (int i = 0; i < scoreString.Length; i++)
        {
            bowlTexts[i].text = scoreString[i].ToString();
            print("B:" + i + "." + scoreString.Length);
            if (i == scoreString.Length - 1)
            {
                bowlTexts[i].canvasRenderer.SetAlpha(.01f);
                bowlTexts[i].CrossFadeAlpha(1, 1, false);
            }
        }
	}

//Fills in the score for each frame
	public void FillFrames (List<int> frames){
		for (int i = 0; i < frames.Count; i++) {
			frameTexts[i].text = frames[i].ToString();
            print("F:" + i + "." + frames.Count);
            if (i == frames.Count - 1)
            {
                frameTexts[i].canvasRenderer.SetAlpha(.01f);
                frameTexts[i].CrossFadeAlpha(1, 1.5f, false);
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
