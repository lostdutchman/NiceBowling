using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	
	const string MASTER_VOLUME = "master_volume";
	const string NICE_BOWLING = "nice_bowling";
	const string HIGH_SCORE = "high_score";
	const string NICE_SCORE = "nice_score";

	public static void SetMasterVolume (float volume){
	if ((volume >= 0f) && (volume <= .8f)){
		PlayerPrefs.SetFloat (MASTER_VOLUME,volume);
		}else{
		Debug.LogError ("Master volume out of range" + volume.ToString());
		}
	}
	
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME);
	}
	
	public static void NiceBowlingSet(int boolean){
		PlayerPrefs.SetInt(NICE_BOWLING, boolean);
	}
	
	public static int NiceBowlingGet(){
		return PlayerPrefs.GetInt(NICE_BOWLING);
	}
	
	public static void SetHighScore(int highscore){
		PlayerPrefs.SetInt (HIGH_SCORE, highscore);
	}
	
	public static int GetHighScore(){
		return PlayerPrefs.GetInt (HIGH_SCORE);
	}
	
	public static void SetNiceScore(int highscore){
		PlayerPrefs.SetInt (NICE_SCORE, highscore);
	}
	
	public static int GetNiceScore(){
		return PlayerPrefs.GetInt (NICE_SCORE);
	}
}
