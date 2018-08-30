using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	
	const string MASTER_VOLUME = "master_volume";
    const string MUSIC_VOLUME = "music_volume";
    const string SFX_VOLUME = "sfx_volume";
    const string HIGH_SCORE = "high_score";

	public static void SetMasterVolume (float volume){
	if ((volume >= -80f) && (volume <= 0f)){
		PlayerPrefs.SetFloat (MASTER_VOLUME,volume);
		}else{
		Debug.LogError ("Master volume out of range" + volume.ToString());
		}
	}
	
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME);
	}

    public static void SetMusicVolume(float volume)
    {
        if ((volume >= -80f) && (volume <= 10f))
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        }
        else
        {
            Debug.LogError("Music volume out of range" + volume.ToString());
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME);
    }

    public static void SetSFXVolume(float volume)
    {
        if ((volume >= -80f) && (volume <= 10f))
        {
            PlayerPrefs.SetFloat(SFX_VOLUME, volume);
        }
        else
        {
            Debug.LogError("SFX volume out of range" + volume.ToString());
        }
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME);
    }

    public static void SetHighScore(int highscore){
		PlayerPrefs.SetInt (HIGH_SCORE, highscore);
	}
	
	public static int GetHighScore(){
		return PlayerPrefs.GetInt (HIGH_SCORE);
	}
	
}
