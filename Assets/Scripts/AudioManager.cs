using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	static AudioManager instance = null;

	public AudioClip[] myMusic;
	private AudioSource audioSource;
    public float MenuTimeDelay;
    public Slider slider;
    public AudioMixer masterMixer;

    // Use this for initialization
    void Start() {
		if (instance != null){
			Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}

		audioSource = GetComponent<AudioSource> ();

        if (!PlayerPrefs.HasKey("master_volume"))
            PlayerPrefsManager.SetMasterVolume(-15f);

        if (!PlayerPrefs.HasKey("music_volume"))
            PlayerPrefsManager.SetMusicVolume(0f);

        if (!PlayerPrefs.HasKey("sfx_volume"))
            PlayerPrefsManager.SetSFXVolume(0f);

        masterMixer.SetFloat("master",PlayerPrefsManager.GetMasterVolume());
        masterMixer.SetFloat("music", PlayerPrefsManager.GetMusicVolume());
        masterMixer.SetFloat("sfx", PlayerPrefsManager.GetSFXVolume());
    }

	public void ChangeMasterVolume()
	{
        int value = (int)slider.value;
        switch (value)
        {
            case 0: masterMixer.SetFloat("master", -80f); PlayerPrefsManager.SetMasterVolume(0f); break;
            case 1: masterMixer.SetFloat("master", -30f); PlayerPrefsManager.SetMasterVolume(0.143f); break;
            case 2: masterMixer.SetFloat("master", -25f); PlayerPrefsManager.SetMasterVolume(0.286f); break;
            case 3: masterMixer.SetFloat("master", -20f); PlayerPrefsManager.SetMasterVolume(0.429f); break;
            case 4: masterMixer.SetFloat("master", -15f); PlayerPrefsManager.SetMasterVolume(0.572f); break;
            case 5: masterMixer.SetFloat("master", -10f); PlayerPrefsManager.SetMasterVolume(0.715f); break;
            case 6: masterMixer.SetFloat("master", -5f); PlayerPrefsManager.SetMasterVolume(0.858f); break;
            case 7: masterMixer.SetFloat("master", 0f); PlayerPrefsManager.SetMasterVolume(1f); break;
        }

	}

    public void ChangeMusicVolume()
    {
        int value = (int)slider.value;
        switch (value)
        {
            case 0: masterMixer.SetFloat("music", -80f); PlayerPrefsManager.SetMusicVolume(-80f); break;
            case 1: masterMixer.SetFloat("music", -30f); PlayerPrefsManager.SetMusicVolume(-30f); break;
            case 2: masterMixer.SetFloat("music", -25f); PlayerPrefsManager.SetMusicVolume(-20f); break;
            case 3: masterMixer.SetFloat("music", -20f); PlayerPrefsManager.SetMusicVolume(-10f); break;
            case 4: masterMixer.SetFloat("music", -15f); PlayerPrefsManager.SetMusicVolume(-5f); break;
            case 5: masterMixer.SetFloat("music", -10f); PlayerPrefsManager.SetMusicVolume(0f); break;
            case 6: masterMixer.SetFloat("music", -5f); PlayerPrefsManager.SetMusicVolume(5f); break;
            case 7: masterMixer.SetFloat("music", 0f); PlayerPrefsManager.SetMusicVolume(10f); break;
        }

    }

    public void ChangeSFXVolume()
    {
        int value = (int)slider.value;
        switch (value)
        {
            case 0: masterMixer.SetFloat("sfx", -80f); PlayerPrefsManager.SetSFXVolume(-80f); break;
            case 1: masterMixer.SetFloat("sfx", -30f); PlayerPrefsManager.SetSFXVolume(-30f); break;
            case 2: masterMixer.SetFloat("sfx", -25f); PlayerPrefsManager.SetSFXVolume(-20f); break;
            case 3: masterMixer.SetFloat("sfx", -20f); PlayerPrefsManager.SetSFXVolume(-10f); break;
            case 4: masterMixer.SetFloat("sfx", -15f); PlayerPrefsManager.SetSFXVolume(-5f); break;
            case 5: masterMixer.SetFloat("sfx", -10f); PlayerPrefsManager.SetSFXVolume(0f); break;
            case 6: masterMixer.SetFloat("sfx", -5f); PlayerPrefsManager.SetSFXVolume(5f); break;
            case 7: masterMixer.SetFloat("sfx", 0f); PlayerPrefsManager.SetSFXVolume(10f); break;
        }

    }

    void Update() {
        if (Time.timeSinceLevelLoad >= MenuTimeDelay)
        {
            if (!audioSource.isPlaying)
                playRandomMusic();
        }
	}
	
	public void playRandomMusic() { 
		audioSource.clip = myMusic[Random.Range(0, myMusic.Length)]; 
		audioSource.Play(); 
	}

}
