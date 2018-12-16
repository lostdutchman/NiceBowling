using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayer : MonoBehaviour {

    public int numberOfPlayers = 1;
    [Tooltip("looking for the score total on UI Canvas")]
    public GameObject P1, P2, P3, P4, P5, P6;
    int currentPlayer = 1;


	void Start () {
        currentPlayer = 1;
        switch (numberOfPlayers)
        {
            case 1: break;
            case 2: P1.SetActive(true); P2.SetActive(true); break;
            case 3: P1.SetActive(true); P2.SetActive(true); P3.SetActive(true); break;
            case 4: P1.SetActive(true); P2.SetActive(true); P3.SetActive(true); P4.SetActive(true); break;
            case 5: P1.SetActive(true); P2.SetActive(true); P3.SetActive(true); P4.SetActive(true); P5.SetActive(true); break;
            case 6: P1.SetActive(true); P2.SetActive(true); P3.SetActive(true); P4.SetActive(true); P5.SetActive(true); P6.SetActive(true); break;
            default: break;
        }     
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void NextPlayer()
    {
        if (currentPlayer == numberOfPlayers)
        {
            currentPlayer = 0;
        }
        currentPlayer++;
    }
}
