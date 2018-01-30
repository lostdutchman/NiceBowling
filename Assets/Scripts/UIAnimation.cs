using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    public Animator animator;
    public Text NBtext;
    public GameObject NBEffect;

    public IEnumerator NiceBowlingEffects(List<string> Effects, bool FirstFrame)
    {
        NBEffect.SetActive(true);
        if (FirstFrame)
        {
            yield return new WaitForSeconds(.5f); //Give frame chance to fully load
        }
        foreach (var Effect in Effects)
        {
            NBtext.text = Effect;
            print(Effect);
            animator.SetTrigger("SlideText");
            yield return new WaitForSeconds(.75f); //animation is .7 seconds
        }
        NBEffect.SetActive(false);
    }
}
