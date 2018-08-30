using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Slider mainSlider, masterSlider, musicSlider, sfxSlider;
    public AudioMixer masterMixer;
    public GameObject Menu, SoundMenu;

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
        mainSlider.value = ConvertMasterVolume(PlayerPrefsManager.GetMasterVolume());
        masterSlider.value = ConvertMasterVolume(PlayerPrefsManager.GetMasterVolume());
        musicSlider.value = ConvertVolume(PlayerPrefsManager.GetMusicVolume());
        sfxSlider.value = ConvertVolume(PlayerPrefsManager.GetSFXVolume());
    }

    private float ConvertMasterVolume(float volume)
    {
        switch ((int)volume)
        {
            case -30: return 1;
            case -25: return 2;
            case -20: return 3;
            case -15: return 4;
            case -10: return 5;
            case -5: return 6;
            case 0: return 7;
            default: return 0;
        }
    }

    private float ConvertVolume(float volume)
    {
        switch ((int)volume)
        {
            case -30: return 1;
            case -20: return 2;
            case -10: return 3;
            case -5: return 4;
            case 0: return 5;
            case 5: return 6;
            case 10: return 7;
            default: return 0;
        }
    }

    public void ToggleSoundMenu()
    {
        if (SoundMenu.activeSelf)
        {
            SoundMenu.SetActive(false);
            Menu.SetActive(true);
            mainSlider.value = ConvertMasterVolume(PlayerPrefsManager.GetMasterVolume());
        }
        else
        {
            SoundMenu.SetActive(true);
            Menu.SetActive(false);
            masterSlider.value = ConvertMasterVolume(PlayerPrefsManager.GetMasterVolume());
        }
    }

	public void ChangeMasterVolume()
	{
        int value = 0;

        if (SoundMenu.activeSelf)
        {
            value = (int)masterSlider.value;
        }
        else
        {
            value = (int)mainSlider.value;
        }

        switch (value)
        {
            case 0: masterMixer.SetFloat("master", -80f); PlayerPrefsManager.SetMasterVolume(-80f); break;
            case 1: masterMixer.SetFloat("master", -30f); PlayerPrefsManager.SetMasterVolume(-30f); break;
            case 2: masterMixer.SetFloat("master", -25f); PlayerPrefsManager.SetMasterVolume(-25f); break;
            case 3: masterMixer.SetFloat("master", -20f); PlayerPrefsManager.SetMasterVolume(-20f); break;
            case 4: masterMixer.SetFloat("master", -15f); PlayerPrefsManager.SetMasterVolume(-15f); break;
            case 5: masterMixer.SetFloat("master", -10f); PlayerPrefsManager.SetMasterVolume(-10f); break;
            case 6: masterMixer.SetFloat("master", -5f); PlayerPrefsManager.SetMasterVolume(-5f); break;
            case 7: masterMixer.SetFloat("master", 0f); PlayerPrefsManager.SetMasterVolume(0f); break;
        }

	}

    public void ChangeMusicVolume()
    {
        int value = (int)musicSlider.value;
        switch (value)
        {
            case 0: masterMixer.SetFloat("music", -80f); PlayerPrefsManager.SetMusicVolume(-80f); break;
            case 1: masterMixer.SetFloat("music", -30f); PlayerPrefsManager.SetMusicVolume(-30f); break;
            case 2: masterMixer.SetFloat("music", -20f); PlayerPrefsManager.SetMusicVolume(-20f); break;
            case 3: masterMixer.SetFloat("music", -10f); PlayerPrefsManager.SetMusicVolume(-10f); break;
            case 4: masterMixer.SetFloat("music", -5f); PlayerPrefsManager.SetMusicVolume(-5f); break;
            case 5: masterMixer.SetFloat("music", 0f); PlayerPrefsManager.SetMusicVolume(0f); break;
            case 6: masterMixer.SetFloat("music", 5f); PlayerPrefsManager.SetMusicVolume(5f); break;
            case 7: masterMixer.SetFloat("music", 10f); PlayerPrefsManager.SetMusicVolume(10f); break;
        }

    }

    public void ChangeSFXVolume()
    {
        int value = (int)sfxSlider.value;
        AudioSource clip = sfxSlider.GetComponent<AudioSource>();
        switch (value)
        {
            case 0: masterMixer.SetFloat("sfx", -80f); PlayerPrefsManager.SetSFXVolume(-80f); break;
            case 1: masterMixer.SetFloat("sfx", -30f); PlayerPrefsManager.SetSFXVolume(-30f); break;
            case 2: masterMixer.SetFloat("sfx", -20f); PlayerPrefsManager.SetSFXVolume(-20f); break;
            case 3: masterMixer.SetFloat("sfx", -10f); PlayerPrefsManager.SetSFXVolume(-10f); break;
            case 4: masterMixer.SetFloat("sfx", -5f); PlayerPrefsManager.SetSFXVolume(-5f); break;
            case 5: masterMixer.SetFloat("sfx", 0f); PlayerPrefsManager.SetSFXVolume(0f); break;
            case 6: masterMixer.SetFloat("sfx", 5f); PlayerPrefsManager.SetSFXVolume(5f); break;
            case 7: masterMixer.SetFloat("sfx", 10f); PlayerPrefsManager.SetSFXVolume(10f); break;
        }
        clip.Play();
    }
}
