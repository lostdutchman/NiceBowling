﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public bool autoload = false;
	
	// Use this for initialization
	void Start () {
        autoload = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (autoload)
        {
            LoadLevel("Opening");
        }
    }

    public void LoadLevel(string name){
        autoload = false;
		SceneManager.LoadScene(name);
	}	
	
	public void LoadNice(int players){
        PlayerPrefsManager.SetGameMode(players);
		SceneManager.LoadScene("Game");
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
