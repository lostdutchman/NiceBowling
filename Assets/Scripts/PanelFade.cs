using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour {

    public float fadeTime = .5f;
    private float menuTimeDelay;
    private bool hasSplashed = false;
    private CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 1;
        menuTimeDelay = GameObject.FindObjectOfType<MusicPlayer>().MenuTimeDelay;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!hasSplashed && (Time.timeSinceLevelLoad >= menuTimeDelay))
        {
            StartCoroutine(FadeOut());
            hasSplashed = true;
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
