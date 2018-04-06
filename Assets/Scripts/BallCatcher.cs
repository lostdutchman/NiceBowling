using UnityEngine;
using System.Collections;

public class BallCatcher : MonoBehaviour {

	
	//this resets the ball if it falls off the lane and destroys any pins that make it past the pin setter.
	void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;
		
		if(thingLeft.GetComponent<Ball>())
			 ThingTracker.ballout = true;
		else
			Destroy(thingLeft);
	}
}
