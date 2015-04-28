using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private Vector3 vel;
	private Transform pictureTrans;

	void FixedUpdate()
	{
		transform.Translate(Time.fixedDeltaTime * vel, Space.World);
	}

	public void InitializeMotion(Vector2 motion)
	{
		vel = new Vector3(motion.x, motion.y);
		pictureTrans = GetComponentInChildren<Transform>();
		pictureTrans.Rotate(Vector3.forward, Random.Range(0f, 360f));
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("Player"))
		{
			coll.GetComponent<Health>().OnImpact();
		}
	}

}
