using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsImageFlip : MonoBehaviour {

    public GameObject Img;
	
	// Update is called once per frame
	void Update () {
        if (Screen.height > Screen.width)
        {
            Img.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            Img.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
