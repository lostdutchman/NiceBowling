using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour {

    public Image Splash;
    float fadeTime = .5f;
    Color colorToFadeTo;
    private float Delay;

	// Use this for initialization
	void Start () {

        Delay = GameObject.FindObjectOfType<MusicManager>().Delay;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > Delay)
        {
            colorToFadeTo = new Color(1f, 1f, 1f, 0f);
            Splash.CrossFadeColor(colorToFadeTo, fadeTime, true, true);
        }
    }
}
