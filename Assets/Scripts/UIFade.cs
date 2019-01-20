using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

    public float fadeTime;
    private CanvasGroup canvas;
    //call these in animator to fade in/out
    public bool autoFadeIn = false;
    public bool autoFadeOut = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        if(this.gameObject.name != "Main Menu")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (autoFadeIn)
        {
            StartCoroutine(FadeIn());
            autoFadeIn = false;
        }
        else if (autoFadeOut)
        {
            StartCoroutine(FadeOut());
            autoFadeOut = false;
        }
    }

    public IEnumerator FadeIn()
    {
        while (canvas.alpha < 1.0f)
        {
            canvas.alpha += (Time.deltaTime / fadeTime);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (canvas.alpha > 0.0f)
        {
            canvas.alpha -= (Time.deltaTime / fadeTime);
            yield return null;
        }
    }
}
