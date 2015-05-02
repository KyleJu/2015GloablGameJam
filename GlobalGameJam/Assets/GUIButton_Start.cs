using UnityEngine;
using System.Collections;

public class GUIButton_Start : MonoBehaviour {
	public Texture btnTexture;
	// Use this for initialization
	void OnGUI() {
		GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (-20, 20, 2000, 2000), btnTexture))
			Application.LoadLevel("Level 1");
	}

}
