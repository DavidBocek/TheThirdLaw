using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float force, lifetime;

	private int playerOwner;
	private Vector3 movementDir;

	private Rigidbody2D rb;
	private ScoreboardMgr scoreboard;
	private UIEffectsMgr UIEffects;

	//initialize here
	public void OnFire(Vector3 dir, int owner){
		rb = GetComponent<Rigidbody2D>();
		scoreboard = GameObject.FindWithTag("Scoreboard").GetComponent<ScoreboardMgr>();
		rb.AddForce((new Vector2(dir.x, dir.y)).normalized * force, ForceMode2D.Impulse);
		playerOwner = owner;
		UIEffects = GameObject.FindWithTag("UIEffectsMgr").GetComponent<UIEffectsMgr>();
		Destroy(gameObject, lifetime);
	}

	void OnCollisionEnter2D(Collision2D coll){
		//ricochet logic
		float bounciness = 1f;
		BounceOff b = coll.collider.GetComponent<BounceOff>();
		if (b != null){
			Vector2 normal = coll.contacts[0].normal;
			rb.AddForce(normal * coll.relativeVelocity.magnitude * bounciness, ForceMode2D.Impulse);;
		}
		
		//destroy logic
		Health health = coll.collider.GetComponent<Health>();
		if (health != null){
			//make sure you can't destroy yourself and that the player isn't invincible
			PlayerManager playerManager = health.gameObject.GetComponent<PlayerManager>();
			if (playerManager != null && playerManager.playerNumber != playerOwner){
				if (!health.isInvulnerable){
					UIEffects.PlayImpactEffects(health, gameObject, scoreboard, playerOwner);
				}
			}
		}
	}

}
