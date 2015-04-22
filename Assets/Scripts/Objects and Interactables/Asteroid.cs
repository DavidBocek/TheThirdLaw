using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private Vector3 vel;

	void FixedUpdate()
	{
		transform.Translate(Time.fixedDeltaTime * vel);
	}

	public void InitializeMotion(Vector2 motion)
	{
		vel = new Vector3(motion.x, motion.y);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.GetComponent<Collider>().gameObject.CompareTag("Player"))
		{
			coll.GetComponent<Collider>().GetComponent<Health>().OnImpact();
		}
	}

}
