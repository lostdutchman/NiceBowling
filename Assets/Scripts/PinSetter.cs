using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	
	public GameObject pinSet;
	public AudioClip[] strikeAudio;
	public AudioClip[] spareAudio;
	public AudioClip[] turkeyAudio;
	public AudioClip[] gutterAudio;
	public AudioClip silence;
    public AudioClip TimeSlowing;
	public GameObject exitButton, Swipper, TouchInput;
	public bool GameOver, StartGame;

	private AudioSource audioSource;
	private NiceBowling niceBowling;
    private NiceBowlingReset niceBowlingReset;
	private Animator animator;
	private PinCounter pinCounter;
    private bool EndTurn = false;
	
	
	void Start () {
		GameOver = false;
        StartGame = true;
		animator = GetComponent<Animator>();
		pinCounter = GameObject.FindObjectOfType<PinCounter>();
		audioSource = GetComponent <AudioSource>();
		niceBowling = GameObject.FindObjectOfType<NiceBowling>();
        niceBowlingReset = GameObject.FindObjectOfType<NiceBowlingReset>();
        Swipper.SetActive (false);
		exitButton.SetActive(false);
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
		if (PlayerPrefsManager.NiceBowlingGet () == 1) {
			foreach (Pins pin in GameObject.FindObjectsOfType<Pins>()) {
				pin.DefaultPins ();//Fix specific effects that prevent the lane from clearing
			}
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
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            pin.NicePins();//reinstate the effects on the pins
        }
        Swipper.SetActive(false);
        ThingTracker.firstPin = true;
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
		case ActionMaster.Action.Tidy:		animator.SetTrigger("tidyTrigger"); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.Reset:		animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = false; break;
		case ActionMaster.Action.EndTurn:   niceBowlingReset.Reset(); animator.SetTrigger("resetTrigger"); pinCounter.Reset(); Swipper.SetActive (true); TouchInput.SetActive (false); EndTurn = true; break;
		case ActionMaster.Action.EndGame:	GameEndButton (); break;
		default: Debug.Log ("Pinsetter.PinsHaveSettled recived invalid input from ActionMaster"); break;
		}
	}

    void OnTriggerExit(Collider collider)
    {
        GameObject thingLeft = collider.gameObject;

        if (thingLeft.tag == "ChildBall")
            ThingTracker.ballout = true;
    }

    public void SlowTime()
    {
        audioSource.clip = TimeSlowing;
        audioSource.Play();
        Time.timeScale = .2f;
        StartCoroutine(RestoreTime());
    }

    private IEnumerator RestoreTime()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
    }

    public void PlayAudio(int type){
		switch (type){
		case 1: audioSource.clip = strikeAudio[Random.Range(0, strikeAudio.Length)]; break;
		case 2: audioSource.clip = spareAudio[Random.Range(0, spareAudio.Length)]; break;
		case 3: audioSource.clip = turkeyAudio[Random.Range(0, turkeyAudio.Length)]; break;
		case 4: audioSource.clip = gutterAudio[Random.Range(0, gutterAudio.Length)]; break;
		case 5: audioSource.clip = silence; break;
			default: break;
		}

		audioSource.Play();

	}
	
	public void GameEndButton(){
		exitButton.SetActive(true);
		TouchInput.SetActive (false);
		GameOver = true;

	}
	
	public void EnableTouch(){
        if (!EndTurn)
        {
            TouchInput.SetActive(true);
        }
    }

}
