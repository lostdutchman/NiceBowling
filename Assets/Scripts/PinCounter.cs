using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	
	private int lastStandingCount = -1; //Minus one means that the pins have settled/are not moving
	private float lastChange;
	private int lastSettledCount = 10;
	private GameManager gameManager;
    [Tooltip("how long the pins have to settle before score is counted")]
    public float settleTime = 3f;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	void Update () {
        if (ThingTracker.ballout && !ThingTracker.framelock)
        {
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
    void CheckStanding()
    {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)
        {
            lastChange = Time.time;
            lastStandingCount = currentStanding;
            return;
        }
        if ((Time.time - lastChange) > settleTime)
        {
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
		ThingTracker.ballout = false;
        ThingTracker.firstPin = true;

    }

    public void Reset(){
		lastStandingCount = -1;
		ThingTracker.ballout = false;
        ThingTracker.firstPin = true;
        lastSettledCount = 10;
	}
}
