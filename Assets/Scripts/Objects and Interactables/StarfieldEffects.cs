using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarfieldEffects : MonoBehaviour {

	public float[] twinkleRate;
	public GameObject[] starfieldPlanes;

	// Use this for initialization
	void Start () {
		for (int i=0; i<starfieldPlanes.Length; i++){
			StartCoroutine(TwinkleObject(starfieldPlanes[i], twinkleRate[i]));
		}
	}

	private IEnumerator TwinkleObject(GameObject obj, float rate){
		SpriteRenderer rdr = obj.GetComponent<SpriteRenderer>();
		Color orig = rdr.color;
		Color cur = rdr.color;
		float t = Random.Range(0f,1f);
		while (true){
			t += Time.smoothDeltaTime/rate;
			t %= 2;
			if (t <= 1){
				cur.a = Mathf.LerpAngle(orig.a, orig.a/3f, t);
			} else {
				cur.a = Mathf.LerpAngle(orig.a/3f, orig.a, t-1);
			}
			rdr.color = cur;
			yield return null;
		}
	}
}
