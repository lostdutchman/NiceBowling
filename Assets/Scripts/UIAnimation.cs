using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    private Animator animator;
    private Text NBtext;
    public GameObject NBEffect;
    public bool AnimationComplete;

    // Use this for initialization
    void Start () {
        NBtext = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }
	
    public void NiceBowlingEffects(List<string> Effects)
    {
        NBEffect.SetActive(true);
        foreach (var Effect in Effects)
        {
            NBtext.text = Effect;
            animator.Play("TextEnter");
            do { } while (!AnimationComplete);
        }
        NBEffect.SetActive(false);
    }
}
