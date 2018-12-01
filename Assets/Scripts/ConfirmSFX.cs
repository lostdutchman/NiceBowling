using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSFX : MonoBehaviour {

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayConfirmSound()
    {
        audioSource.Play();
    }
}
