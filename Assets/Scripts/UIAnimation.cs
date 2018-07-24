using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    private Animation Anim;
    public Text NBtext;
    public GameObject NBEffect, Tut, TouchInput;

    public IEnumerator NiceBowlingEffects(List<string> Effects, bool FirstFrame)
    {
        Anim = GetComponent<Animation>();
        NBEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f); //give frame ect a chance to load.
        foreach (var Effect in Effects)
        {
            NBtext.text = Effect;
            yield return new WaitForSecondsRealtime(.1f); //text time to change
            Anim.Play();
            yield return new WaitForSecondsRealtime(Anim.clip.length +.01f);
        }
        NBEffect.SetActive(false);
        if (FirstFrame)
        {
            Tut.SetActive(true);
        }
        TouchInput.SetActive(true);
    }
}
