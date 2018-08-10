using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISprite : MonoBehaviour {

    public Sprite[] sprites;
    public float animationSpeed;
    public Image image;

    public IEnumerator AnimateSprite()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(animationSpeed);
        }
        Trigger();
    }
	
	// Update is called once per frame
	public void Trigger () {
        StartCoroutine(AnimateSprite());
    }
}
