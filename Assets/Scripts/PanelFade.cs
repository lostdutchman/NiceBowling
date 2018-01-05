using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour {

    public Image Splash;
    float fadeTime = 3f;
    Color colorToFadeTo;

	// Use this for initialization
	void Start () {
        colorToFadeTo = new Color(1f, 1f, 1f, 0f);
        Splash.CrossFadeColor(colorToFadeTo, fadeTime, true, true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
