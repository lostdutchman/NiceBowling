using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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

	private AudioSource audioSource;
	private NiceBowling niceBowling;
    private NiceBowlingReset niceBowlingReset;
	private Animator animator;
	private PinCounter pinCounter;
    private bool EndTurn = false;
    private GameManager Menu;
    private int frame = 0;
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

        //Clears pins that didnt get pushed off the lane
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Destroy(pin.gameObject);
        }

        Instantiate(pinSet, new Vector3(0, 1, 1829), Quaternion.identity);
		Swipper.SetActive (false);
        niceBowling.NiceManager();
	}

	public void performAction(ActionMaster.Action action){
		//Pass pins that have fallen to Action Master to initiate animations
		switch(action){
		case ActionMaster.Action.Tidy:		animator.SetTrigger("tidyTrigger"); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.Reset:		animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.EndTurn:   niceBowlingReset.Reset(); animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); ScrollScore(); EndTurn = true; break;
		case ActionMaster.Action.EndGame:	StartCoroutine(GameEndButton()); break;
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

    private IEnumerator GameEndButton(){
        yield return new WaitForSecondsRealtime(1.5f);
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
        frame++;
        scoreScroller.NextFrame(frame, multiplayer.GetCurrentPlayer() - 1);
    }
}
