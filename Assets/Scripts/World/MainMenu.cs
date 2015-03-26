using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//TODO: actually write this
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel(1);
		}
	}
}
