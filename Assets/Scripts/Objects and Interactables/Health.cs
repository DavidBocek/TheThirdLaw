using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float respawnTime, invulnerableTime;
	public ParticleSystem deathEmitter;
	public AudioClip deathClip;

	[HideInInspector]
	public bool isDead = false;
	[HideInInspector]
	public bool isInvulnerable = false;

	public void OnImpact(){
		if (!isInvulnerable && !isDead){
			StartCoroutine(Hit());
		}
	}

	private IEnumerator Hit(){
		isDead = true;
		deathEmitter.Play();
		GetComponent<DirectionForceMove>().thrustEmitter.enableEmission = false;
		AudioSource.PlayClipAtPoint(deathClip, transform.position);
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()){
			sr.enabled = false;
		}
		foreach (Collider coll in GetComponentsInChildren<Collider>()){
			coll.enabled = false;
		}
		yield return new WaitForSeconds(respawnTime);
		StartCoroutine(Respawn());

	}

	private IEnumerator Respawn(){
		isDead = false;
		isInvulnerable = true;
		GetComponent<DirectionForceMove>().thrustEmitter.enableEmission = true;
		foreach (Collider coll in GetComponentsInChildren<Collider>(true)){
			coll.enabled = true;
		}
		for (int i=0; i<6; i++){
			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true)){
				sr.enabled = true;
			}
			yield return new WaitForSeconds(invulnerableTime/12f);
			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()){
				sr.enabled = false;
			}
			if (i!= 5){
				yield return new WaitForSeconds(invulnerableTime/12f);
			}
		}
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true)){
			sr.enabled = true;
		}
		isInvulnerable = false;
	}
}
