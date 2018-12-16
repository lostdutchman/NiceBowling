using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public List<PlayerScores> scoreCard = new List<PlayerScores>();
    private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;
    private LocalMultiplayer multiplayer;
    public GameObject Menu, TouchPad, Arrows, SoundMenu;

	// Use this for initialization
	void Start () {
        multiplayer = GetComponent<LocalMultiplayer>();
		pinSetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball>();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        Time.timeScale = 1;
    }

    private string PrintListInt(List<int> list)
    {
        string result = "";
        foreach (var number in list)
        {
            result += number + ", ";
        }
        return result;
    }

    public void Bowl (int pinFall){
        print("PinFall: " + pinFall);
		scoreCard[multiplayer.GetCurrentPlayer() - 1].bowls.Add(pinFall);
		ball.Reset();
		pinSetter.performAction (ActionMaster.NextAction(scoreCard[multiplayer.GetCurrentPlayer() - 1].bowls));

        scoreDisplay.FillBowls(scoreCard[multiplayer.GetCurrentPlayer() - 1].bowls);
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(scoreCard[multiplayer.GetCurrentPlayer() - 1].bowls));
        print("Current Bowles: " + PrintListInt(scoreCard[multiplayer.GetCurrentPlayer() - 1].bowls));
        print("__________________________________________________________________________");
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
