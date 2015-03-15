using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

	//multiplayer logic
	private PlayerManager playerManager;

	//firing logic
	public float chargeTime, reboundForce, cooldown;
	public Transform muzzleLocation;
	public GameObject projectile;
	[HideInInspector]
	public bool canFire = true;
	[HideInInspector]
	public bool canMove = true;

	//particles
	public float chargingParticleRate;
	public ParticleSystem chargingParticles;
	public ParticleSystem shotParticles;

	//sound
	public AudioSource chargingSound;
	public AudioClip shotSound;

	//references
	private Health health;

	// Use this for initialization
	void Start () {
		playerManager = GetComponent<PlayerManager>();
		health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerManager.Fire1 && canFire && !health.isDead){
			StartCoroutine(FireEffects());
		}
	}

	private void Fire(){
		GameObject proj = (GameObject) Instantiate(projectile, muzzleLocation.position, muzzleLocation.rotation);
		proj.GetComponent<Projectile>().OnFire(muzzleLocation.up, playerManager.playerNumber);
		playerManager.rb.AddForce(-muzzleLocation.up.normalized * reboundForce, ForceMode2D.Impulse);
	}

	private IEnumerator FireEffects(){
		canFire = false;
		canMove = false;
		chargingParticles.Play(true);
		chargingSound.Play();
		float originalRate = chargingParticles.emissionRate;
		for (float t=0f; t<1f; t+=Time.deltaTime/chargeTime){
			//charge particles and noise
			chargingParticles.emissionRate = Mathf.Lerp(originalRate, chargingParticleRate, t);
			yield return null;
		}
		chargingParticles.emissionRate = originalRate;
		chargingParticles.Stop(true);
		shotParticles.Play();
		AudioSource.PlayClipAtPoint(shotSound, playerManager.PlayerObj.transform.position);
		Fire();
		canMove = true;
		yield return new WaitForSeconds(cooldown);
		canFire = true;
	}
}
