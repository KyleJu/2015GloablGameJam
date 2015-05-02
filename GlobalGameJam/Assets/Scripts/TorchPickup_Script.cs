using UnityEngine;
using System.Collections;

public class TorchPickup_Script : MonoBehaviour {
	public bool isPrevious = false; 

	void Start() {
		Player_Script.instance.torches.Add (this);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Player_Script.instance.torchPicked = this;
			Player_Script.instance.nearestTorch = this.gameObject;

		}
	}


	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
						Player_Script.instance.torchPicked = null;
						Player_Script.instance.nearestTorch = null;
				}
	}
	
}
