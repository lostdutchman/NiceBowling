using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMultiplayer : MonoBehaviour {

    public int numberOfPlayers = 1;
    public GameObject P1Total, P2Total, P3Total, P4Total, P5Total, P6Total, currentPlayerName;
    public RawImage[] Boards;
    int currentPlayer = 1;


	void Start () {
        currentPlayer = 1;
        switch (numberOfPlayers)
        {
            case 2: currentPlayerName.SetActive(true);  P1Total.SetActive(true); P2Total.SetActive(true); break;
            case 3: P3Total.SetActive(true); goto case 2;
            case 4: P4Total.SetActive(true); goto case 3;
            case 5: P5Total.SetActive(true); goto case 4;
            case 6: P6Total.SetActive(true); goto case 5;
            default: break;
        }     
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void NextPlayer()
    {
        ChangeBoards(currentPlayer - 1);
        if (currentPlayer == numberOfPlayers)
        {
            currentPlayer = 0;
        }
        currentPlayer++;
    }

    private IEnumerator ChangeBoards(int index)
    {
        Boards[index].CrossFadeAlpha(0, 1, false);
        index++;
        if (index == numberOfPlayers)
        {
            index = 0;
        }
        yield return new WaitForSecondsRealtime(1f);
        Boards[index].CrossFadeAlpha(1, 1, false);
    }
}
