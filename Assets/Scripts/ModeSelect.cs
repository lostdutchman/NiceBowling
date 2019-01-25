using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour {
    private int previousMode = 1; //0 for online, 1 for local
    private int previousPlayers = 1;
    private int numberOfPlayers = 1;
    public Slider Mode;
    public Shadow Online, Local;
    public InputField[] inputFields;
    public Text[] placeholders;
    public LevelManager levelManager;

    // Use this for initialization
    void Start () {
        previousPlayers = PlayerPrefsManager.GetGameMode();
        LoadPlaceholderText();
        if(previousPlayers == 0)
        {
            previousMode = 0;
            numberOfPlayers = 1;
            Online.enabled = true;
        }
        else
        {
            Local.enabled = true;
        }
        Mode.value = previousMode;
    }

    private void LoadPlaceholderText()
    {
        for(int i = 0; i < 6; i++)
        {
            placeholders[i].text = PlayerPrefsManager.GetPlayerName(i + 1);
        }
    }
	
    public void ToggleMode(float value)
    {
        //Online
        if (value < 1)
        {
            Online.enabled = true;
            Local.enabled = false;
            for (int i = 1; i < inputFields.Length; i++)
            {
                inputFields[i].gameObject.SetActive(false);
            }
        }
        //Local
        else
        {
            Online.enabled = false;
            Local.enabled = true;
            if(numberOfPlayers < 2)
            {
                numberOfPlayers = 2;
            }
            for (int i = numberOfPlayers + 1; i < inputFields.Length + 1; i++)
            {
                inputFields[i - 1].gameObject.SetActive(false);
            }
            for (int i = 0; i < numberOfPlayers; i++)
            {
                inputFields[i].gameObject.SetActive(true);
            }
        }
    }

    public void AddPlayer()
    {
        if (numberOfPlayers < 6)
        {
            numberOfPlayers++;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                inputFields[i].gameObject.SetActive(true);
            }
        }
    }

    public void RemovePlayer()
    {
        Debug.Log("About to remove, current player value = " + numberOfPlayers);
        if(numberOfPlayers > 1)
        {
            numberOfPlayers--;
            for (int i = numberOfPlayers + 1; i < inputFields.Length + 1; i++)
            {
                inputFields[i - 1].gameObject.SetActive(false);
            }
        }
    }

    public void Play()
    {
        if(Mode.value == 0)
        {
            levelManager.LoadNice(0);
        }
        else
        {
            levelManager.LoadNice(numberOfPlayers);
        }
    }

    //This is some shit code but unity wont let me pass 2 variables at once with InputFeild so this is my only way to know which input feild the name string came from.. Well this is the easiest way and isnt that what I shoud do as a programmer...
    public void Player1NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(1, name);
    }

    public void Player2NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(2, name);
    }

    public void Player3NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(3, name);
    }

    public void Player4NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(4, name);
    }

    public void Player5NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(5, name);
    }

    public void Player6NameChange(string name)
    {
        PlayerPrefsManager.SetPlayerName(6, name);
    }
}
