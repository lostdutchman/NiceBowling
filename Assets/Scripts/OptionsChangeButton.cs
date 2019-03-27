using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsChangeButton : MonoBehaviour {

    public Text Diff;

	// Use this for initialization
	void Start () {
        UpdateDifficulty(PlayerPrefsManager.GetDifficulty());
	}

    public void ToggleDifficulty()
    {
        if (PlayerPrefsManager.GetDifficulty() == 8f)
        {
            UpdateDifficulty(6f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 6f)
        {
            UpdateDifficulty(3.5f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 3.5f)
        {
            UpdateDifficulty(2f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 2f)
        {
            UpdateDifficulty(1f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 1f)
        {
            UpdateDifficulty(8f);
        }
    }

    private void UpdateDifficulty(float difficulty)
    {
        PlayerPrefsManager.SetDifficulty(difficulty);

        if(difficulty == 3.5)
        {
            Diff.text = "MODE: NORMAL";
        }
        else if (difficulty == 6)
        {
            Diff.text = "MODE: EASY";
        }
        else if (difficulty == 8)
        {
            Diff.text = "MODE: VERY EASY";
        }
        else if (difficulty == 2)
        {
            Diff.text = "MODE: HARD";
        }
        else if (difficulty == 1)
        {
            Diff.text = "MODE: TOO HARD";
        }
        else 
        {
            Diff.text = "MODE: UNKNOWN";
            Debug.Log("UpdateDifficulty got invalid value of " + difficulty);
        }
    }
}
