using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour {

    public Image Splash;
    public float FadeTime = .5f;
    Color ColorToFadeTo;
    private float MenuTimeDelay;
    private bool Splashed = false;

    // Use this for initialization
    void Start () {
        MenuTimeDelay = GameObject.FindObjectOfType<MusicPlayer>().MenuTimeDelay;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update () {
        if (!Splashed && (Time.timeSinceLevelLoad >= MenuTimeDelay))
        {
            Fade();
            Splashed = true;
        }
    }

    void Fade()
    {
        ColorToFadeTo = new Color(1f, 1f, 1f, 0f);
        Splash.CrossFadeColor(ColorToFadeTo, FadeTime, true, true);
    }
}
