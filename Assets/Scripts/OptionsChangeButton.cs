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
        if (PlayerPrefsManager.GetDifficulty() == 15f)
        {
            UpdateDifficulty(10f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 10f)
        {
            UpdateDifficulty(8f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 8f)
        {
            UpdateDifficulty(6f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 6f)
        {
            UpdateDifficulty(3.5f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 3.5f)
        {
            UpdateDifficulty(15f);
        }
        else
        {
            UpdateDifficulty(8f);
        }
    }

    private void UpdateDifficulty(float difficulty)
    {
        PlayerPrefsManager.SetDifficulty(difficulty);

        if(difficulty == 8)
        {
            Diff.text = "DEFAULT SENSITIVITY";
        }
        else if (difficulty == 10)
        {
            Diff.text = "LOW SENSITIVITY";
        }
        else if (difficulty == 15)
        {
            Diff.text = "LEAST SENSITIVITY";
        }
        else if (difficulty == 6)
        {
            Diff.text = "VERY SENSITIVE";
        }
        else if (difficulty == 3.5)
        {
            Diff.text = "MOST SENSITIVE";
        }
        else 
        {
            Diff.text = "DEFAULT SENSITIVITY";
            UpdateDifficulty(8f);
        }
    }
}
