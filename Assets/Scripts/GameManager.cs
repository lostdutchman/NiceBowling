using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private List <int> bowls = new List<int>();
	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;
    public GameObject Menu, TouchPad, Arrows, SoundMenu;

	// Use this for initialization
	void Start () {
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        Time.timeScale = 1;
    }
	
	public void Bowl (int pinFall){
		bowls.Add (pinFall);
		ball.Reset ();
		pinSetter.performAction (ActionMaster.NextAction (bowls));
	
		scoreDisplay.FillBowls(bowls);
		scoreDisplay.FillFrames (ScoreMaster.ScoreCumulative(bowls));
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            try
            {
                ToggleMenu();
            }
            catch
            {
                //Do nothing, this it to keep from getting an error on scenes that done have a menu
            }
        }
    }

    public void ToggleMenu()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
            SoundMenu.SetActive(false);
            TouchPad.SetActive(true);
            Arrows.SetActive(true);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            Menu.SetActive(true);
            SoundMenu.SetActive(false);
            TouchPad.SetActive(false);
            Arrows.SetActive(false);
        }
    }
			
}
