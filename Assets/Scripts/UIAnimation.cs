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
        yield return new WaitForSeconds(.2f); //give frame ect a chance to load.
        foreach (var Effect in Effects)
        {
            NBtext.text = Effect;
            Anim.Play();
            yield return new WaitForSeconds(Anim.clip.length);
        }
        NBEffect.SetActive(false);
        if (FirstFrame)
        {
            Tut.SetActive(true);
        }
        TouchInput.SetActive(true);
    }
}
