using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float force, lifetime;

	private int playerOwner;
	private Vector3 movementDir;

	private Rigidbody2D rb;
	private ScoreboardMgr scoreboard;
	private UIEffectsMgr UIEffects;
	private Vector2 lastPos, curPos;

	//initialize here
	public void OnFire(Vector3 dir, int owner){
		rb = GetComponent<Rigidbody2D>();
		scoreboard = GameObject.FindWithTag("Scoreboard").GetComponent<ScoreboardMgr>();
		rb.AddForce((new Vector2(dir.x, dir.y)).normalized * force, ForceMode2D.Impulse);
		playerOwner = owner;
		UIEffects = GameObject.FindWithTag("UIEffectsMgr").GetComponent<UIEffectsMgr>();
		Destroy(gameObject, lifetime);
		lastPos = new Vector2(transform.position.x, transform.position.y);
		curPos = new Vector2();
	}

	public void FixedUpdate(){
		curPos.x = transform.position.x;
		curPos.y = transform.position.y;

		//ricochet logic


		lastPos.x = curPos.x;
		lastPos.y = curPos.y;
	}

	void OnCollisionEnter2D(Collision2D coll){
		
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
