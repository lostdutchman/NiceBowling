using UnityEngine;
using System.Collections;

public class iwasiRigid : MonoBehaviour {
	public bool mizuwoeta;

	void Update(){
		float probability = Time.deltaTime * .05f;
		if(Random.value < probability){
			Rigidbody rigid = GetComponent<Rigidbody> ();
			rigid.AddForce (Random.Range (-50f, 30f),Random.Range (50f, 200f),Random.Range (-35f, 45f), ForceMode.Impulse);
			GetComponent<AudioSource> ().Play ();
		}
	}
}
