using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCredits : MonoBehaviour {

	public Text highText, niceText;
	
	public void CreativeCommons(){
		Application.OpenURL("https://creativecommons.org/licenses/by/3.0/");
	}

	public void RottenM(){
		Application.OpenURL("https://soundcloud.com/rottenmatera");
	}

	public void SockM(){
		Application.OpenURL("https://soundcloud.com/the-sock-monkees");
	}

	public void JeremyT(){
		Application.OpenURL("http://jeremytoler.net");
	}

	public void MegA(){
		Application.OpenURL("http://www.MegganAnderson.com ");
	}

	void OnGUI(){
		highText.text = PlayerPrefsManager.GetHighScore().ToString ();
		niceText.text = PlayerPrefsManager.GetNiceScore ().ToString ();
	}
}
