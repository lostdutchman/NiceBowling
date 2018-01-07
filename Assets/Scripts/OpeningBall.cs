using UnityEngine;
using System.Collections;

public class OpeningBall : MonoBehaviour {

	private Rigidbody rigidBody;
	public AudioSource canvas;
	public float launchSpeed;
	private GameObject menu;
	private float MenuTimeDelay;
	public AudioClip[] niceBowling;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		menu = GameObject.Find ("Canvas");
		menu.SetActive (false);
		float yPos = rigidBody.transform.position.y;
		float zPos = rigidBody.transform.position.z;
		rigidBody.transform.position = new Vector3 (Random.Range(-10, 10), yPos, zPos);
		ThrowBall ();
		if (!PlayerPrefs.HasKey ("nice_score"))
			PlayerPrefsManager.SetNiceScore(0);
		if (!PlayerPrefs.HasKey ("high_score"))
			PlayerPrefsManager.SetHighScore(0);
		if(Application.loadedLevel == 0)
			canvas.clip = niceBowling[Random.Range(0, niceBowling.Length)];
        MenuTimeDelay = GameObject.FindObjectOfType<MusicManager>().Delay + .5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad >= MenuTimeDelay){
			menu.SetActive(true);
		}
		
	}
	
	public void ThrowBall ()
	{
		rigidBody.useGravity = true;
		rigidBody.velocity = new Vector3 (0, 0, launchSpeed);
		rigidBody.angularVelocity = new Vector3 (20, 0, 0);
	}
}
