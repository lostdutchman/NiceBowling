using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class AudioManagerNoOptions : MonoBehaviour {

    public AudioMixer masterMixer;

    // Use this for initialization
    void Start() {
        if (!PlayerPrefs.HasKey("master_volume"))
            PlayerPrefsManager.SetMasterVolume(-15f);

        if (!PlayerPrefs.HasKey("music_volume"))
            PlayerPrefsManager.SetMusicVolume(0f);

        if (!PlayerPrefs.HasKey("sfx_volume"))
            PlayerPrefsManager.SetSFXVolume(0f);

        masterMixer.SetFloat("master",PlayerPrefsManager.GetMasterVolume());
        masterMixer.SetFloat("music", PlayerPrefsManager.GetMusicVolume());
        masterMixer.SetFloat("sfx", PlayerPrefsManager.GetSFXVolume());
    }
}
