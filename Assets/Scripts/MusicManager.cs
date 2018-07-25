using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	static MusicManager instance = null;

	public AudioClip[] myMusic;
	public AudioSource audioSource;
    public float MenuTimeDelay;


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
		ChangeVolume(PlayerPrefsManager.GetMasterVolume());
	}
	public void ChangeVolume(float value)
	{
		audioSource.volume = value;
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
