using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {

	//TODO

	void Start(){
		PersistantData.playersToSpawn.Clear();

		//testing
		PersistantData.playersToSpawn.Add(0);
		PersistantData.playersToSpawn.Add(1);
		Application.LoadLevel(2);
	}

}
