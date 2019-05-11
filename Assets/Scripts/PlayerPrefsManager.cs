using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME = "master_volume";
    const string MUSIC_VOLUME = "music_volume";
    const string SFX_VOLUME = "sfx_volume";
    const string GAME_MODE = "game_mode";
    const string DIFFICULTY = "difficulty";

    public static void SetDifficulty(float difficulty)
    {
        //Very Easy = 8, Easy = 6, Normal = 3.5, Hard = 2, Too Hard = 1
        if (difficulty > 0f)
        {
            PlayerPrefs.SetFloat(DIFFICULTY, difficulty);
        }
        else
        {
            Debug.LogError("DIFFICULTY out of range " + difficulty.ToString());
        }
    }

    public static float GetDifficulty()
    {
        if (PlayerPrefs.HasKey(DIFFICULTY))
        {
            return PlayerPrefs.GetFloat(DIFFICULTY);
        }
        else
        {
            SetDifficulty(3.5f);
            return 3.5f;
        }
    }

    public static void SetGameMode(int players)
    {
        //0 = online, 1-6 is number of players local.
        if (players >= 0 && players < 7)
        {
            PlayerPrefs.SetInt(GAME_MODE, players);
        }
        else
        {
            Debug.LogError("Number of players is out of range " + players.ToString());
        }
    }

    public static int GetGameMode()
    {
        if (PlayerPrefs.HasKey(GAME_MODE))
        {
            return PlayerPrefs.GetInt(GAME_MODE);
        }
        else
        {
            SetGameMode(1);
            return 1;
        }
    }

    public static void SetPlayerName(int player, string name)
    {
        if (player > 0 && player < 7)
        {
            if (name.Length > 0)
            {
                if (name.Length > 8)
                {
                    PlayerPrefs.SetString("player_" + player, name.Remove(7));
                }
                else
                {
                    PlayerPrefs.SetString("player_" + player, name);
                }
            }
            else
            {
                PlayerPrefs.SetString("player_" + player, "PLAYER " + player);
            }
        }
        else
        {
            Debug.Log("Player number invalid " + player + " " + name);
        }
    }

    public static string GetPlayerName(int player)
    {
        if (player > 0 && player < 7)
        {
            if(!PlayerPrefs.HasKey("player_" + player))
            {
                SetPlayerName(player, "PLAYER " + player);
            }
            return PlayerPrefs.GetString("player_" + player);
        }
        else
        {
            Debug.Log("Invalid player number " + player);
            return "Player " + player;
        }
    }

    public static void SetMasterVolume (float volume){
	if ((volume >= -80f) && (volume <= 0f)){
		PlayerPrefs.SetFloat (MASTER_VOLUME,volume);
		}else{
		Debug.LogError ("Master volume out of range " + volume.ToString());
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
            Debug.LogError("Music volume out of range " + volume.ToString());
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
            Debug.LogError("SFX volume out of range " + volume.ToString());
        }
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME);
    }
}
