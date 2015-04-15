using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {

	// time in between asteroids spawning
	public GameObject Asteroid;
	public float timeinbetween;
	public float asteroidspeed;
	private Vector2 passthrough;
	private Vector2 forcedirection;
	private Vector2 normforce;
	// Use this for initialization
	void Start (){
	}
	public void callAsteroids(){
		InvokeRepeating ("Spawn", 1, timeinbetween);
	}
	
	private void Spawn(){
		var randominnerx = Random.Range(-5,5);
		var randominnery = Random.Range(-5,5);
		var randomoutterpoint = Random.onUnitSphere*50;
		forcedirection = new Vector2((randominnerx-randomoutterpoint.x),(randominnery-randomoutterpoint.y));
		normforce = forcedirection.normalized;
		GameObject ast = (GameObject) Instantiate (Asteroid, new Vector3(randomoutterpoint.x, randomoutterpoint.y), Quaternion.LookRotation(Vector3.forward));
		ast.GetComponent<Rigidbody2D>().AddForce((normforce*asteroidspeed), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
