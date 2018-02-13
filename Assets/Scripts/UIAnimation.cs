using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

    private Animation Anim;
    public Text NBtext;
    public GameObject NBEffect;

    public IEnumerator NiceBowlingEffects(List<string> Effects)
    {
        Anim = GetComponent<Animation>();
        NBEffect.SetActive(true);
        foreach (var Effect in Effects)
        {
            NBtext.text = Effect;
            Anim.Play();
            yield return new WaitForSeconds(Anim.clip.length);
        }
        NBEffect.SetActive(false);
    }
}
