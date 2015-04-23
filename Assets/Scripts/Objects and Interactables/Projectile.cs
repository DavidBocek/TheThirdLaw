﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float force, lifetime;

	private int playerOwner;
	private Vector2 curVel, curPos, lastPos;
	private float speed;

	private Rigidbody2D rb;
	private ScoreboardMgr scoreboard;
	private UIEffectsMgr UIEffects;

	void Start(){
		lastPos = new Vector2(transform.position.x, transform.position.y);
	}

	//initialize here
	public void OnFire(Vector3 dir, int owner){
		rb = GetComponent<Rigidbody2D>();
		scoreboard = GameObject.FindWithTag("Scoreboard").GetComponent<ScoreboardMgr>();
		rb.AddForce((new Vector2(dir.x, dir.y)).normalized * force, ForceMode2D.Impulse);
		speed = rb.velocity.magnitude;
		playerOwner = owner;
		UIEffects = GameObject.FindWithTag("UIEffectsMgr").GetComponent<UIEffectsMgr>();
		Destroy(gameObject, lifetime);
	}

	public void FixedUpdate(){
		curPos.x = transform.position.x;
		curPos.y = transform.position.y;
		curVel = curPos - lastPos;
		lastPos = curPos;
	}

	void OnCollisionEnter2D(Collision2D coll){
		//ricochet logic
		Vector2 normal = Vector2.zero;
		foreach (ContactPoint2D contact in coll.contacts){
			normal += contact.normal;
			Debug.DrawRay(contact.point, contact.normal, Color.red);
		}
		normal /= coll.contacts.Length;
		Vector3 newVel = Vector3.Reflect(curVel.normalized, normal);
		rb.velocity = speed * (Vector2) newVel;


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
