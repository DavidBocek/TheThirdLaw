using UnityEngine;
using System.Collections;

public class LevelSpawnMgr : MonoBehaviour {

	public GameObject[] levelRoots;

	// Use this for initialization
	void Start () {
		GameObject levelRoot = levelRoots[Random.Range(0,levelRoots.Length)];
		Instantiate(levelRoot);
	}
	

}
