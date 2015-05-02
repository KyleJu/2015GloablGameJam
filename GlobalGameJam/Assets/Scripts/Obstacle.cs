using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject == Player_Script.instance.gameObject) {
				    Player_Script.instance.die();
				}
	}
}
