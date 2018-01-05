using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLoader : MonoBehaviour
{
    public float delay = 4;
    void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));   
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel("Opening");
    }
}
