using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private Vector3 vel;
	private Transform pictureTrans;

	void FixedUpdate()
	{
		transform.Translate(Time.fixedDeltaTime * vel);
	}

	public void InitializeMotion(Vector2 motion)
	{
		vel = new Vector3(motion.x, motion.y);
		pictureTrans = GetComponentInChildren<Transform>();
		pictureTrans.Rotate(Vector3.forward, Random.Range(360f));
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.GetComponent<Collider>().gameObject.CompareTag("Player"))
		{
			coll.GetComponent<Collider>().GetComponent<Health>().OnImpact();
		}
	}

}
