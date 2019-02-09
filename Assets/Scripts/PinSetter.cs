using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PinSetter : MonoBehaviour {

    public GameObject pinSet;
	public AudioClip[] strikeAudio;
	public AudioClip[] spareAudio;
	public AudioClip[] turkeyAudio;
	public AudioClip[] gutterAudio;
	public AudioClip silence;
	public GameObject Resume, Swipper, TouchInput;
    public ScoreScroller scoreScroller;
	public bool GameOver, StartGame;

    private HighScores highScores = new HighScores();
    private AudioSource audioSource;
	private NiceBowling niceBowling;
    private NiceBowlingReset niceBowlingReset;
	private Animator animator;
	private PinCounter pinCounter;
    private bool EndTurn = false;
    private GameManager Menu;
    private int bowls = 0;
    private LocalMultiplayer multiplayer;
	
	void Start () {
		GameOver = false;
        StartGame = true;
        multiplayer = GameObject.FindObjectOfType<LocalMultiplayer>();
		animator = GetComponent<Animator>();
		pinCounter = GameObject.FindObjectOfType<PinCounter>();
		audioSource = GetComponent <AudioSource>();
		niceBowling = GameObject.FindObjectOfType<NiceBowling>();
        niceBowlingReset = GameObject.FindObjectOfType<NiceBowlingReset>();
        Swipper.SetActive (false);
        Menu = FindObjectOfType<GameManager>();
        }

    public void Update()
    {
        if (StartGame)
        {
            niceBowlingReset.Reset();
            StartGame = false;
            niceBowling.NiceManager();
        }
    }

    //Called by animator to rais pins
    public void RaisePins(){
		foreach (Pins pin in GameObject.FindObjectsOfType<Pins>()) {
			pin.DefaultPins ();//Fix specific effects that prevent the lane from clearing
		}
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.Raise();
		}
	}
	
	//Called by animator to lowwer pins
	public void LowerPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.Lower();
        }
        if (!EndTurn)
        {
            foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
            {
                pin.NicePins();//reinstate the effects on the pins
            }
        }
        Swipper.SetActive(false);
    }
	
	//Called by animator to add a new set of pins
	public void RenewPins(){
        Instantiate(pinSet, new Vector3(0, 1, 1829), Quaternion.identity);
		Swipper.SetActive (false);
        niceBowling.NiceManager();
	}

	public void performAction(ActionMaster.Action action){
		//Pass pins that have fallen to Action Master to initiate animations
		switch(action){
		case ActionMaster.Action.Tidy: animator.SetTrigger("tidyTrigger"); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.Reset: StartCoroutine(KillRemainingPins()); animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.EndTurn: StartCoroutine(KillRemainingPins()); niceBowlingReset.Reset(); animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); ScrollScore(); EndTurn = true; break;
		case ActionMaster.Action.EndGame: StartCoroutine(EndGame()); break;
		default: Debug.Log ("Pinsetter.PinsHaveSettled recived invalid input from ActionMaster"); break;
		}
	}

    public void PlayAudio(int type){
		switch (type){
		case 1: audioSource.clip = strikeAudio[UnityEngine.Random.Range(0, strikeAudio.Length)]; break;
		case 2: audioSource.clip = spareAudio[UnityEngine.Random.Range(0, spareAudio.Length)]; break;
		case 3: audioSource.clip = turkeyAudio[UnityEngine.Random.Range(0, turkeyAudio.Length)]; break;
		case 4: audioSource.clip = gutterAudio[UnityEngine.Random.Range(0, gutterAudio.Length)]; break;
		case 5: audioSource.clip = silence; break;
			default: break;
		}

		audioSource.Play();
	}

    private IEnumerator EndGame()
    {
        //add single player check
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("End Game Triggered");
        List<HighScores> playerScores = new List<HighScores>();
        for(int i = 0; i <= multiplayer.numberOfPlayers - 1; i++)
        {
            HighScores temp = new HighScores();
            int tempScore = 0;
            foreach(int bowl in multiplayer.scoreCard[i].bowls)
            {
                tempScore += bowl;
            }
            temp.bowls = multiplayer.scoreCard[i].bowls;
            temp.Score = tempScore;
            temp.playerName = multiplayer.scoreCard[i].playerName;
            playerScores.Add(temp);
        }
        highScores.SetHighScore(playerScores);
        StartCoroutine(GameEndButton());
    }

    private IEnumerator GameEndButton(){
        yield return new WaitForSecondsRealtime(1f);
        Menu.ToggleMenu();
        Resume.SetActive(false);
		TouchInput.SetActive (false);
		GameOver = true;
	}
	
	public void EnableTouch(){
        if (!EndTurn)
        {
            TouchInput.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        //Fix issue if jelly ball stops in pin collider
        JellyBall Jelly = FindObjectOfType<JellyBall>();
        if(collider.name == "DefKitSoftbodyNode_4")
        {
            Jelly.kill();
        }
    }

    private void ScrollScore()
    {
        bowls++;
        scoreScroller.NextFrame(bowls, multiplayer.GetCurrentPlayer() - 1);
    }

    private IEnumerator KillRemainingPins()
    {
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            pin.MarkEndTurnPins();
        }
        yield return new WaitForSecondsRealtime(1.5f);
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            pin.KillEndTurnPins();
        }
    }
}
