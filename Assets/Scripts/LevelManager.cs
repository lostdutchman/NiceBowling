using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Slider volumeSlider;
	private MusicManager musicManager;
    public bool autoload = false;
	
	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
        autoload = false;
        try
        {
            volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        }
        catch
        {
            //This scene does not have volume slider.
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (autoload)
        {
            LoadLevel("Opening");
        }
        try
        {
            musicManager.ChangeVolume(volumeSlider.value);
        }
        catch
        {
            //Still no volume slider
        }
    }

    public void LoadLevel(string name){
        autoload = false;
		SceneManager.LoadScene(name);
	}	
	
	public void LoadNice(){
		PlayerPrefsManager.NiceBowlingSet(1);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		SceneManager.LoadScene("Game");
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
