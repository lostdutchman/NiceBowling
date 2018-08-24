using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScroller : MonoBehaviour {

    public GameObject ScoreBoard;
    [Tooltip("How far the x pos needs to move to get to next frame")]
    public float MoveDist;
    [Tooltip("How far the x pos needs to move to get to frame 10")]
    public float MoveDist10;
    [Tooltip("How log it takes to make the move")]
    public float LerpTime;

    public void NextFrame(int frame)
    {
        Vector3 startPos = ScoreBoard.transform.localPosition;

        if (frame <= 3)
        {
            //Do nothing
        }
        else if (frame == 10)
        {
           StartCoroutine(MoveToPosition(MoveDist10, LerpTime));
        }
        else
        {
            StartCoroutine(MoveToPosition(MoveDist, LerpTime));
        }
    }
    public IEnumerator MoveToPosition(float move, float timeToMove)
    {
        var startPos = transform.localPosition;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            ScoreBoard.transform.localPosition = Vector3.Lerp(startPos, new Vector3(startPos.x + move, startPos.y, startPos.z), t);
            yield return null;
        }
    }
}
