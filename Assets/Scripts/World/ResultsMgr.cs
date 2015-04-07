using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultsMgr : MonoBehaviour {
	
	//the view of the MVC for the character selection screen
	
	public Sprite[] shipPreviewSprites;

	public GameObject[] selectImages;
	public GameObject[] placeTexts;
	public GameObject[] scoreTexts;
	public GameObject[] bgpList;

	List<KeyValuePair<int, int>> scoresList = new List<KeyValuePair<int, int>>{};
	
	// Use this for initialization
	void Start () {
		foreach (KeyValuePair<int, int> kvp in PersistantData.mostRecentScores) {
			scoresList.Add(kvp);
		}

		scoresList.Sort(CompareScores);

		// TODO: Remove after testing
		foreach (KeyValuePair<int, int> kvp in scoresList) {
			Debug.Log ("kvp: " + kvp.ToString());
		}

		if (shipPreviewSprites.Length != 6){
			Debug.LogError("Improper preview sprites length. Include the 6 ships in the proper order.");
		}

		int prevPlace = 0;
		int prevScore = -1;

		// For loop assumes scoresList is sorted to work correctly
		for (int i=0; i<scoresList.Count; i++){

			bgpList[i].SetActive(true);

			KeyValuePair<int, int> score = scoresList[i];
			int place = -1;

			if (score.Value == prevScore) {
				place = prevPlace;
			} else {
				place = ++prevPlace;
			}

			prevScore = score.Value;
			prevPlace = place;


			// Display place
			placeTexts[i].SetActive(true);
			placeTexts[i].GetComponent<Text>().text = place.ToString();

			// Display Ship
			int playerNum = score.Key;
			int shipIndex = PersistantData.playersToSpawn[playerNum];
			selectImages[i].SetActive(true);
			selectImages[i].GetComponent<Image>().sprite = shipPreviewSprites[shipIndex];

			// Display score
			scoreTexts[i].SetActive(true);
			scoreTexts[i].GetComponent<Text>().text = "Score: " + score.Value.ToString();
		}
	}

	// Used to sort scores in decreasing order
	private static int CompareScores(KeyValuePair<int, int> score1, KeyValuePair<int, int> score2) {
		return score2.Value - score1.Value;
	}
	
	public void Join(int joiningIndex, int startCharIndex){
		placeTexts[joiningIndex].SetActive(false);
		selectImages[joiningIndex].GetComponent<Image>().sprite = shipPreviewSprites[startCharIndex];
	}

	public void Quit(int quittingIndex){
		placeTexts[quittingIndex].SetActive(true);
	}
	
	public void Unlock(int index){
		scoreTexts[index].SetActive(false);
	}
	
	public void Lock(int index){
		scoreTexts[index].SetActive(true);
	}
}

