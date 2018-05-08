using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (((collision.gameObject.tag == "Pin") || (collision.gameObject.tag == "NBTemp")) && (collision.gameObject.transform.parent != this.transform))
        {
            //collision.gameObject.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
