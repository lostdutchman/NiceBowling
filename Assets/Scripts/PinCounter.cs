using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	
	private int lastStandingCount = -1; //Minus one means that the pins have settled/are not moving
	private float lastChange;
	private int lastSettledCount = 10;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	void Update () {
		if(BallOutOfPlay.ballout){
			CheckStanding();
		}
	}

	//Counts pins
	public int CountStanding(){
		int standing = 0;
		
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			if(pin.IsStanding()){
				standing++;
			}
		}	
		return standing;
	}
	
	//Checks if a pin is standing then updates the standing count until pins dont change for 3 seconds
	void CheckStanding ()
	{
		int currentStanding = CountStanding ();
		
		if(currentStanding != lastStandingCount){
			lastChange = Time.time;
			lastStandingCount = currentStanding;
			return;
		}
		float settleTime = 3f; if (BallOutOfPlay.ballout == false) {settleTime = .5f;} //in case of low gravity speed up pin count.
		if((Time.time - lastChange) > settleTime){//If last change > 3 seconds
			PinsHaveSettled();
		}
	}

	void PinsHaveSettled ()
	{
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;
		gameManager.Bowl (pinFall);

		lastStandingCount = -1;
		BallOutOfPlay.ballout = false;
	}

	public void Reset(){
		lastStandingCount = -1;
		BallOutOfPlay.ballout = false;
		lastSettledCount = 10;
	}
}
