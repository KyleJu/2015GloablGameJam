using UnityEngine;
using System.Collections;

public class GUILives_Script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		guiText.text = "0";
	}
	
	void setNumDeaths(int deaths)
	{
		guiText.text = "" + deaths;
	}
}
