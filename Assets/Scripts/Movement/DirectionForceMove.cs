using UnityEngine;
using System.Collections;

public class DirectionForceMove : MonoBehaviour {

	public float thrustForce;
	public float maxSpeed;
	public float lerpFractionPerUpdate;

	public ParticleSystem thrustEmitter;
	public ParticleSystem deathEmitter;

	private Rigidbody2D rb;
	private Vector2 force;
	private Vector3 inputDir;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		force = new Vector2();
		inputDir = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
		inputDir.x = Input.GetAxis("Horizontal");
		inputDir.y = Input.GetAxis("Vertical");

		if (inputDir.sqrMagnitude != 0){
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, inputDir), lerpFractionPerUpdate);
		}

		thrustEmitter.enableEmission = inputDir.sqrMagnitude > .1f;
	}

	void FixedUpdate(){
		//compute force based on player input
		force.x = Input.GetAxis("Horizontal");
		force.y = Input.GetAxis("Vertical");
		
		if (force.x != 0f && force.y != 0f){
			force.Normalize();
		}
		
		force *= thrustForce;

		rb.AddForce(force, ForceMode2D.Force);
		//max speed check
		if (rb.velocity.sqrMagnitude > maxSpeed*maxSpeed){
			Vector2 temp = new Vector2(rb.velocity.x, rb.velocity.y);
			rb.velocity = temp.normalized*maxSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		float bounciness = .75f;
		BounceOff b = coll.collider.GetComponent<BounceOff>();
		if (b != null){
			bounciness = b.bounciness;
		}

		Vector2 normal = coll.contacts[0].normal;
		rb.AddForce(normal * coll.relativeVelocity.magnitude * bounciness, ForceMode2D.Impulse);
	}
}
