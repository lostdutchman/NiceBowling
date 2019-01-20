using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour {
    private int previousMode = 1; //0 for online, 1 for local
    private int previousPlayers = 1;
    private int numberOfPlayers = 1;
    public Slider Mode;
    public Shadow Online, Local;
    public InputField[] inputFields;

    // Use this for initialization
    void Start () {
        previousPlayers = PlayerPrefsManager.GetGameMode();
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
        //for(int i = previousPlayers; i < inputFields.Length + 1; i++)
        //{
        //    Debug.Log("Initializing:" + (i - 1));
        //    inputFields[i - 1].gameObject.SetActive(false);
        //}
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
        numberOfPlayers++;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            inputFields[i].gameObject.SetActive(true);
        }
    }

    public void RemovePlayer()
    {
        numberOfPlayers--;
        for (int i = numberOfPlayers + 1; i < inputFields.Length + 1; i++)
        {
            inputFields[i - 1].gameObject.SetActive(false);
        }
    }
}
