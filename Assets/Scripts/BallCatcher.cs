using UnityEngine;
using System.Collections;

public class BallCatcher : MonoBehaviour {

	
	//this resets the ball if it falls off the lane and destroys any pins that make it past the pin setter.
	void OnTriggerExit(Collider collider){
		GameObject thingLeft = collider.gameObject;
        if (thingLeft.tag == "ChildBall")
        {
            ThingTracker.ballout = true;

            if (thingLeft.name == "JellyBall(Clone)")
            {
                FindObjectOfType<Ball>().ResetTheJellyBall();
            }
        }
        else
        {
            Destroy(thingLeft);
        }
            

    }
}
