using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {

	// time in between asteroids spawning
	public GameObject Asteroid;
	public float timeInBetween;
	public float asteroidSpeed;
	private Vector2 passThrough;
	private Vector2 forceDirection;
	private Vector2 normForce;
	// Use this for initialization
	void Start (){
	}
	public void StartSpawningAsteroids(){
		InvokeRepeating ("Spawn", 1, timeInBetween);
	}
	
	private void Spawn(){
		var randomInnerX = Random.Range(-10f,10f);
		var randomInnerY = Random.Range(-7.5f,7.5f);
		var randomOutterPoint = Random.onUnitSphere*50;
		forceDirection = new Vector2((randomInnerX-randomOutterPoint.x),(randomInnerY-randomOutterPoint.y));
		normForce = forceDirection.normalized;
		GameObject ast = (GameObject) Instantiate (Asteroid, new Vector3(randomOutterPoint.x, randomOutterPoint.y), Quaternion.LookRotation(Vector3.forward));
		ast.GetComponent<Rigidbody2D>().AddForce((normForce*asteroidSpeed), ForceMode2D.Impulse);
	}
}
