using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Slider volumeSlider;
	private MusicManager musicManager;
	
	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.ChangeVolume(volumeSlider.value);
	}
	
	public void LoadLevel(string name){
		Application.LoadLevel(name);
	}	
	
	public void LoadNice(){
		PlayerPrefsManager.NiceBowlingSet(1);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		Application.LoadLevel("Game");
	}
	
	public void LoadNorm(){
		PlayerPrefsManager.NiceBowlingSet(0);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		Application.LoadLevel("Game");
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
