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
            UpdateDifficulty(3f);
        }
        else if (PlayerPrefsManager.GetDifficulty() == 3f)
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

        if (difficulty == 8)
        {
            Diff.text = "デフォルト感度";
        }
        else if (difficulty == 10)
        {
            Diff.text = "感度が低い";
        }
        else if (difficulty == 15)
        {
            Diff.text = "最低感度";
        }
        else if (difficulty == 6)
        {
            Diff.text = "もっと感度";
        }
        else if (difficulty == 3)
        {
            Diff.text = "最も感度";
        }
        else 
        {
            UpdateDifficulty(15f);
        }
    }
}
