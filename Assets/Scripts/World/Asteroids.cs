using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {

	// time in between asteroids spawning
	public GameObject Asteroid;
	public float timeinbetween;
	public float asteroidspeed;
	private Vector2 passthrough;
	// Use this for initialization
	void Start (){
	}
	public void callAsteroids(){
		InvokeRepeating ("Spawn", 1, timeinbetween);
	}
	
	private void Spawn(){
		var randominnerx = Random.Range(-5,5);
		var randominnery = Random.Range(-5,5);
		var randomoutterpoint = Random.onUnitSphere*25;
		passthrough = new Vector2(randominnerx,randominnery);
		GameObject ast = (GameObject) Instantiate (Asteroid, new Vector3(randomoutterpoint.x, randomoutterpoint.y), Quaternion.LookRotation(Vector3.forward));
		ast.GetComponent<Rigidbody2D>().velocity = (passthrough * asteroidspeed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
