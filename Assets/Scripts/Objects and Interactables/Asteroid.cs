using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.gameObject.CompareTag("Player")){
			coll.collider.GetComponent<Health>().OnImpact();
		}
	}

}
