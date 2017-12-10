using UnityEngine;
using System.Collections;

public class InGameLevelManager : MonoBehaviour {
	
	public void LoadLevel(string name){
		Application.LoadLevel(name);
	}	
}
