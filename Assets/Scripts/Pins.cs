using UnityEngine;
using System.Collections;

public class Pins : MonoBehaviour {
	
	public float standingThreshold = 5f; //how many degrees it can be tilted
	public float distanceToRaise = 45f;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
	}
	
	//Lifts pins, is called by pinsetter
	public void Raise(){
		if(IsStanding()){
			rigidBody.useGravity = false;
			transform.Translate (new Vector3(0, distanceToRaise, 0), Space.World);
		}
	}
	
	//Lowers pins, is called by pinsetter
	public void Lower(){
		transform.Translate (new Vector3(0, -distanceToRaise, 0), Space.World);
		rigidBody.useGravity = true;
		transform.rotation = Quaternion.identity;  //Set rotation to 0, lays pin on side
		transform.rotation = Quaternion.Euler (-90,0,0);//Correct previous line to make sure pins are all straight after each play.
	}
	
	//We get the current rotation then check to see if it is past the threshold.
	public bool IsStanding(){
		//eulerAngles change that into a vector 3
		Vector3 rotationInEuler = transform.rotation.eulerAngles;
		//Mathf.abs gets absolute position so we dont have to worry about pos or neg, deltaangle calculates shortest ange difference between two angles. 
		// (e.g. Mathf.DeltaAngle(355, 5) is 10, because 360 is basically the same as 0). 
		//So if your Pins have a rotation of 0 on the z axis when you start the game, you use 0 as second parameter. 
		//If your Pin has another rotation while standing, you should of course use that value instead of 0.
		if(Mathf.Abs (Mathf.DeltaAngle (270 - rotationInEuler.x, 0))< standingThreshold && Mathf.Abs (Mathf.DeltaAngle (rotationInEuler.z, 0)) < standingThreshold ){
			return true;
		}else{
			return false;
		}
	}

	void OnCollisionEnter(){
			audioSource.Play ();
	}

	//fix issues cause by nice bowling mode
	public void DefaultPins(){
		if (rigidBody.isKinematic) {rigidBody.isKinematic = false;} 
		if (rigidBody.drag != 0) {rigidBody.drag = 0;} 
		if (rigidBody.angularDrag != 0.05f) {rigidBody.angularDrag = 0.05f;} 
	}
}
