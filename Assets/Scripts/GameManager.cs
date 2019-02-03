using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;
    private LocalMultiplayer multiplayer;
    public GameObject Menu, TouchPad, Arrows, SoundMenu;

    //Lets me auto test while still reviewing code
    void Awake()
    {
        if (Application.isEditor)
            Application.runInBackground = true;
    }

    // Use this for initialization
    void Start () {
        multiplayer = GetComponent<LocalMultiplayer>();
		pinSetter = GameObject.FindObjectOfType<PinSetter>();
		ball = GameObject.FindObjectOfType<Ball>();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        Time.timeScale = 1;
    }

    //private string PrintListInt(List<int> list)
    //{
    //    string result = "";
    //    foreach (var number in list)
    //    {
    //        result += number + ", ";
    //    }
    //    return result;
    //}

    public void Bowl (int pinFall){
        int index = multiplayer.GetCurrentPlayer() - 1;
        //Debug.Log("Adding " + pinFall + " to scorecard " + index);
		multiplayer.scoreCard[index].bowls.Add(pinFall);
		ball.Reset();

        //Debug.Log(PrintListInt(multiplayer.scoreCard[index].bowls));

        scoreDisplay.FillBowls(multiplayer.scoreCard[index].bowls);
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(multiplayer.scoreCard[index].bowls));

        ActionMaster.Action am = ActionMaster.NextAction(multiplayer.scoreCard[index].bowls, index, multiplayer.GetCurrentPlayer(), multiplayer.numberOfPlayers);
        pinSetter.performAction(am);

        if (am == ActionMaster.Action.EndTurn)
        {
            NextPlayer();
        }
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

    private void NextPlayer()
    {
        if (multiplayer.numberOfPlayers > 1)
        {
            multiplayer.NextPlayer();
        }
    }
}
