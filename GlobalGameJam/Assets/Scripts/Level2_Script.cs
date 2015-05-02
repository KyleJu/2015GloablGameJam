using UnityEngine;
using System.Collections;


public class Level2_Script : MonoBehaviour {
		
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			if (Player_Script.instance.endLevel != false) {
				Application.LoadLevel("Level 2");
			}
		}
	}

}