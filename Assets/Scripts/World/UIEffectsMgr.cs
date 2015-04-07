using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffectsMgr : MonoBehaviour {

	public GameObject plusOneText;
	public AudioClip slowTimeHitClip;

	private bool isSlowing = false;
	private bool isShaking = false;

	public void PlayImpactEffects(Health health, GameObject proj, ScoreboardMgr scoreboard, int playerOwner){
		health.AnimatePreDeath();
		if (!isSlowing){
			StartCoroutine(SlowTime(health, proj));
		}
		//Camera.main.GetComponent<CameraShake>().StartShake();
		StartCoroutine(PlusOnePointEffect(health.gameObject, scoreboard, playerOwner));
	}

	private IEnumerator SlowTime(Health health, GameObject proj){
		isSlowing = true;
		Rigidbody2D rb = health.gameObject.GetComponent<Rigidbody2D>();
		Destroy(proj);
		AudioSource.PlayClipAtPoint(slowTimeHitClip, proj.transform.position, 1f);
		for (float t=0; t<=1f; t+=Time.unscaledDeltaTime){
			rb.velocity = Vector3.zero;
			Time.timeScale = Mathf.Lerp(1f,.2f,t);
			yield return null;
		}
		Time.timeScale = .2f;
		health.OnImpact();
		Time.timeScale = 1f;
		isSlowing = false;
	}

	private IEnumerator PlusOnePointEffect(GameObject hitPlayer, ScoreboardMgr scoreboard, int playerOwner){
		yield return new WaitForSeconds(.75f);
		GameObject textObj = (GameObject) Instantiate(plusOneText, hitPlayer.transform.position+Vector3.up*1f, Quaternion.LookRotation(Vector3.forward, Vector3.up));
		textObj.GetComponent<SpriteRenderer>().color = PersistantData.colors[playerOwner];
		Vector3 initialLoc = textObj.transform.position;
		Vector3 targetLoc = initialLoc + Vector3.up*2f;
		for (float t=0; t<=1f; t+=Time.unscaledDeltaTime){
			textObj.transform.position = Vector3.Lerp(initialLoc, targetLoc, t);
			yield return null;
		}
		Destroy(textObj);
		scoreboard.AddPoint(playerOwner);
	}

}
