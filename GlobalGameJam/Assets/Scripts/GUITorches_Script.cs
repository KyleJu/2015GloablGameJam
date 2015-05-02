using UnityEngine;
using System.Collections;

public class GUITorches_Script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		guiText.text = "1";
	}
	
	void setTorchesUsed(int torches)
	{
		guiText.text = "" + torches;
	}
}