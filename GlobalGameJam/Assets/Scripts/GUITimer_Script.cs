using UnityEngine;
using System.Collections;

public class GUITimer_Script : MonoBehaviour {
	
	public float startTime;
	
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		guiText.text = "Timer: 0";
		InvokeRepeating ("increaseTimer", 1, 1);
	}
	
	void increaseTimer()
	{
		guiText.text = "Timer: " + (int)(Time.time - startTime) ;
	}
}
