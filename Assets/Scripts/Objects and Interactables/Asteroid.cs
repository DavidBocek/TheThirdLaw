using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private Vector3 vel;

	void FixedUpdate(){

	}

	public void InitializeMotion(Vector2 motion){
		vel = new Vector3(motion.x, motion.y);
	}

	void OnTriggerEnter2D(Collision2D coll){
		if (coll.collider.gameObject.CompareTag("Player")){
			coll.collider.GetComponent<Health>().OnImpact();
		} else {
			Projectile p = coll.collider.GetComponent<Projectile>();
			if (p != null){
				Vector2 normal = coll.contacts[0].normal;
				coll.collider.GetComponent<Rigidbody2D>().AddForce(normal * coll.relativeVelocity.magnitude * .9f, ForceMode2D.Impulse);
			}
		}
	}

}
