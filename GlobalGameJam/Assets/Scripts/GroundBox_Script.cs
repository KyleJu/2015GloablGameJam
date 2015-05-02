using UnityEngine;
using System.Collections;

public class GroundBox_Script : MonoBehaviour {
	void Start() {
		//Player_Script.instance.groundBoxScript.Add (this);
	}
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ground") {
			Player_Script.instance.grounded = true;		
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ground") {
			Player_Script.instance.grounded = false;		
		}
	}
}
