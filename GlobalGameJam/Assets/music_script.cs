using UnityEngine;
using System.Collections;

public class music_script : MonoBehaviour {

	public static music_script instance;

	void Awake() {

		// make sure we survive going to different scenes
		if(instance)
		{
			Destroy (gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
