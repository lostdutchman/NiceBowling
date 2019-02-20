using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnd : MonoBehaviour {

    public GameObject Menu, TouchInput, screen, brokenScreen;
    public AudioClip crash;
    private AudioSource sfx;
    private GameManager gameManager;

	void Start () {
        sfx = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "ChildBall")
        {
            StartCoroutine(EndTheDemo());
            collision.collider.tag = "Untagged";
        }
    }

    private IEnumerator EndTheDemo()
    {
        screen.SetActive(false);
        sfx.PlayOneShot(crash);
        yield return new WaitForSecondsRealtime(.2f);
        brokenScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(crash.length + .3f);
        Time.timeScale = 0;
        gameManager.ToggleMenu();
    }
}
