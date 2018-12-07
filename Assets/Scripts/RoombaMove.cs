using UnityEngine;
using System.Collections;

public class RoombaMove : MonoBehaviour {

    public float speed = 10f;
    public float forwardSpeed = 2f;
    private bool rotate = false;
    private bool ignore = false;
    private bool stop = false;
    public AudioClip RoombaHit, RoombaNoise;
    public AudioClip[] RoombaResponse;
    private AudioSource SoundPlayer;

    private void Start()
    {
        SoundPlayer = GetComponent<AudioSource>();
    }

    void Update () {
        if ((transform.position.x > 40 || transform.position.x < -40) && !ignore && this.tag != "WasStuck" && !stop)
        {
            StartCoroutine(TurnAround());
        }
        else if(rotate || this.tag == "WasStuck")
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        else if (stop)
        {
            //Dont do anything
        }
        else
        {
            //Move forward
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;
        }
	
	}

    IEnumerator TurnAround()
    {
        ignore = true;
        rotate = true;
        yield return new WaitForSecondsRealtime(Random.Range(.3f, 2f));
        rotate = false;
        yield return new WaitForSecondsRealtime(Random.Range(.5f, 1f));
        ignore = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ChildBall")
        {
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        int tmpNum = UnityEngine.Random.Range(0, RoombaResponse.Length);
        stop = true;
        SoundPlayer.Stop();
        SoundPlayer.clip = RoombaHit;
        SoundPlayer.loop = false;
        SoundPlayer.Play();
        yield return new WaitForSecondsRealtime(RoombaHit.length);
        SoundPlayer.Stop();
        SoundPlayer.clip = RoombaResponse[tmpNum];
        SoundPlayer.Play();
        yield return new WaitForSecondsRealtime(RoombaResponse[tmpNum].length);
        SoundPlayer.Stop();
        stop = false;
        SoundPlayer.clip = RoombaNoise;
        SoundPlayer.loop = true;
        SoundPlayer.Play();
    }
}