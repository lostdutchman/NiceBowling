using UnityEngine;
using System.Collections;

public class iwasiRigid : MonoBehaviour {
	public bool mizuwoeta;

	void Update(){
		float probability = Time.deltaTime * .05f;
		if(Random.value < probability){
			Rigidbody rigid = GetComponent<Rigidbody> ();
			rigid.AddForce (Random.Range (-5f, 3f),Random.Range (5f, 20f),Random.Range (-3.5f, 4.5f), ForceMode.Impulse);
			GetComponent<AudioSource> ().Play ();
		}
	}
}
