﻿using UnityEngine;
using System.Collections;
using System;

public class Pins : MonoBehaviour {
	
	public float standingThreshold = 7f; //how many degrees it can be tilted
	public float distanceToRaise = 45f;
    public float distanceThreshold = 5f;
    public AudioClip PinHitPin;
    public AudioClip PinHitBall;
    private Rigidbody rigidBody;
	private AudioSource audioSource;
    private string PinStatus = "None";
    private Vector3 StartPos;

    void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
        StartPos = this.transform.position;
    }

    void Awake()
    {
        this.GetComponent<Rigidbody>().solverVelocityIterations = 10;
    }

    //Lifts pins, is called by pinsetter
    public void Raise(){
		if(IsStanding() && this.tag != "WasStuck") //Spike ball steals the pin and I dont want it to be lowered if thats the case
        {
            rigidBody.useGravity = false;
		    transform.Translate (new Vector3(0, distanceToRaise, 0), Space.World);
		}
	}
	
	//Lowers pins, is called by pinsetter
	public void Lower(){
        if(this.tag != "WasStuck") //Spike ball steals the pin and I dont want it to be lowered if thats the case
        {
            transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
            rigidBody.useGravity = true;
            transform.rotation = Quaternion.identity;  //Set rotation to 0, lays pin on side
            transform.rotation = Quaternion.Euler(-90, 0, 0);//Correct previous line to make sure pins are all straight after each play.
        }
	}
	

	public bool IsStanding(){

		if(!HasTilted())
        {
            return true;
        }
        else{
            return false;
		}
	}

    //We get the current rotation and position then check to see if it is past the threshold.
    private bool HasTilted()
    {
        //eulerAngles change that into a vector 3
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        Vector3 currentPos = this.transform.position;
        
        //Check to see if position moved far enough to be out
        if (currentPos.x - StartPos.x > distanceThreshold ||
            currentPos.x - StartPos.x < distanceThreshold * -1||
            currentPos.z - StartPos.z > distanceThreshold ||
            currentPos.z - StartPos.z < distanceThreshold * -1)
        {
            //Debug.Log("distanceTriggered");
            //Debug.Log("x:" + (currentPos.x - StartPos.x) + " z:" + (currentPos.z - StartPos.z));
            PinStatus = "HasTilted";
            return true;
        }
        //Mathf.abs gets absolute position so we dont have to worry about pos or neg, deltaangle calculates shortest ange difference between two angles. 
        // (e.g. Mathf.DeltaAngle(355, 5) is 10, because 360 is basically the same as 0). 
        //So if the Pins have a rotation of 0 on the z axis when starting the game, you use 0 as second parameter. 
        else if ((Mathf.Abs(Mathf.DeltaAngle(270 - rotationInEuler.x, 0)) < standingThreshold && Mathf.Abs(Mathf.DeltaAngle(rotationInEuler.z, 0)) < standingThreshold))
        {
            return false;
        }
        else
        {
            PinStatus = "HasTilted";
            return true;
        }

    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Pin")
        {
            audioSource.clip = PinHitPin;
            audioSource.Play();
        }
        else if (col.gameObject.tag == "ChildBall")
        {
            audioSource.clip = PinHitBall;
            //Add force when pin is hit to make physics apply more realisticly.
            Vector3 HitForce = (transform.position - col.contacts[0].point).normalized * col.rigidbody.mass * 2.5f;
            rigidBody.AddForce(HitForce, ForceMode.Impulse);
            rigidBody.AddTorque(new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-30, 30), UnityEngine.Random.Range(-30, 30)), ForceMode.Impulse);
            audioSource.Play();
        }
	}

    //fix issues cause by nice bowling mode
    public void DefaultPins()
    {
        if (this.tag != "WasStuck")
        {
            if (rigidBody.isKinematic)
            {
                rigidBody.isKinematic = false;
                if(PinStatus != "HasTilted")
                    PinStatus = "BoardwalkPins";
            }
            else if (rigidBody.drag != 0)
            {
                rigidBody.drag = 0;
                rigidBody.angularDrag = 0.05f;
                if (PinStatus != "HasTilted")
                    PinStatus = "IncreasePinDrag";
            }
            else if (rigidBody.useGravity == false)
            {
                if (PinStatus != "HasTilted")
                    PinStatus = "NoGravity";
            }
        }
    }

    //fix issues cause by nice bowling mode
    public void NicePins()
    {
        if (PinStatus == "BoardwalkPins") {
            rigidBody.isKinematic = true;
        }
        else if (PinStatus == "IncreasePinDrag") {
            rigidBody.drag = 15f;
            rigidBody.angularDrag = 30f;
        }
        else if (PinStatus == "NoGravity")
        {
            rigidBody.useGravity = false;
        }
    }

    public void MarkEndTurnPins()
    {
        PinStatus = "HasTilted";
    }

    public void KillEndTurnPins()
    {
        if(PinStatus == "HasTilted")
        {
            Destroy(this.gameObject);
        }
    }
}
