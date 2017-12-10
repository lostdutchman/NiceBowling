using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private List <int> bowls = new List<int>();
	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();

	}
	
	public void Bowl (int pinFall){
		bowls.Add (pinFall);
		ball.Reset ();
		pinSetter.performAction (ActionMaster.NextAction (bowls));
	
		scoreDisplay.FillBowls(bowls);
		scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(bowls));
		if (pinSetter.GameOver == true)
			SetHighScores ();

	}

	public void SetHighScores(){
		if (PlayerPrefsManager.NiceBowlingGet () == 1) {
			if(ScoreMaster.endScore > PlayerPrefsManager.GetNiceScore()){PlayerPrefsManager.SetNiceScore(ScoreMaster.endScore);}
		} else {
			if(ScoreMaster.endScore > PlayerPrefsManager.GetHighScore()){PlayerPrefsManager.SetHighScore(ScoreMaster.endScore);}
		}
	}
			
}
