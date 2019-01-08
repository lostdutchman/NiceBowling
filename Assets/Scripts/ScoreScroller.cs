using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScroller : MonoBehaviour {

    public GameObject[] ScoreBoards;
    [Tooltip("How far the x pos needs to move to get to next frame")]
    public float MoveDist;
    [Tooltip("How far the x pos needs to move to get to frame 10")]
    public float MoveDist10;
    [Tooltip("How log it takes to make the move")]
    public float LerpTime;
    private LocalMultiplayer multiplayer;

    void Start()
    {
        multiplayer = FindObjectOfType<LocalMultiplayer>();
    }

    public void NextFrame(int bowls, int playerIndex)
    {
        Vector3 startPos = ScoreBoards[playerIndex].transform.localPosition;
        int frames = bowls / multiplayer.numberOfPlayers;

        if(bowls % multiplayer.numberOfPlayers != 0)
        {
            frames++;
        }

        if (frames < 3)
        {
            //Do nothing
        }
        else if (frames == 9)
        {
           StartCoroutine(MoveToPosition(MoveDist10, LerpTime, playerIndex));
        }
        else
        {
            StartCoroutine(MoveToPosition(MoveDist, LerpTime, playerIndex));
        }
        print("____________________________________________________");
    }
    public IEnumerator MoveToPosition(float move, float timeToMove, int playerIndex)
    {
        var startPos = ScoreBoards[playerIndex].transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            ScoreBoards[playerIndex].transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x + move, startPos.y, startPos.z), t);
            yield return null;
        }
    }
}
