using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public bool inPlay = false;
	private Vector3 ballStartPos;
    public AudioClip BallRolling;
    private AudioSource audioSource;
    public GameObject Tut, LeftArrow, RightArrow, TouchInput;

    void Start () {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        childBall.GetComponent<Rigidbody>().useGravity = false;
		audioSource = GetComponent<AudioSource>();
    }

    public void Launch (Vector3 velocity)
	{
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        ballStartPos = this.transform.position;
        childBall.GetComponent<Rigidbody>().useGravity = true;
        childBall.GetComponent<Rigidbody>().velocity = velocity;
		inPlay = true;
        Tut.SetActive(false);
        LeftArrow.SetActive(false);
        RightArrow.SetActive(false);
        audioSource.clip = BallRolling;
        audioSource.Play();
    }
	
	public void Reset ()
	{
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Rigidbody rigidBody = childBall.GetComponent<Rigidbody>();
        GameObject.FindGameObjectWithTag("ChildBall").GetComponent<MeshRenderer>().enabled = false;
        childBall.transform.position = ballStartPos;
		childBall.transform.rotation = Quaternion.identity;
        rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
        inPlay = false;
	}

    void FixedUpdate()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        //Make it so that if ball stops moving the frame resets
        if (inPlay)
        {
            if (childBall.GetComponent<Rigidbody>().velocity.z <= 0)
            {
                ThingTracker.ballout = true;
            }
        }
        else
        {
            if (TouchInput.activeInHierarchy == true)
            {
                LeftArrow.SetActive(true);
                RightArrow.SetActive(true);
                childBall.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void TryAgain()
    {
        Tut.SetActive(true);
    }
}
