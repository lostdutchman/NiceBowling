using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {
	static MusicManager instance = null;

	public AudioClip[] myMusic;
	public AudioSource audioSource;
    public float MenuTimeDelay;
    public Slider slider;

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
			PlayerPrefsManager.SetMasterVolume(.5f);
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}
	public void ChangeVolume()
	{
        int value = (int)slider.value;
        switch (value)
        {
            case 0: audioSource.volume = 0; PlayerPrefsManager.SetMasterVolume(0f); break;
            case 1: audioSource.volume = 0.143f; PlayerPrefsManager.SetMasterVolume(0.143f); break;
            case 2: audioSource.volume = 0.286f; PlayerPrefsManager.SetMasterVolume(0.286f); break;
            case 3: audioSource.volume = 0.429f; PlayerPrefsManager.SetMasterVolume(0.429f); break;
            case 4: audioSource.volume = 0.572f; PlayerPrefsManager.SetMasterVolume(0.572f); break;
            case 5: audioSource.volume = 0.715f; PlayerPrefsManager.SetMasterVolume(0.715f); break;
            case 6: audioSource.volume = 0.858f; PlayerPrefsManager.SetMasterVolume(0.858f); break;
            case 7: audioSource.volume = 1; PlayerPrefsManager.SetMasterVolume(1f); break;
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
