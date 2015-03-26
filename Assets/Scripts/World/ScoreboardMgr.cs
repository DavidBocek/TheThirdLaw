using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreboardMgr : MonoBehaviour {

	public int winningPointValue = -1;	//set to -1 to have no victory by point value
	public Text[] scoresTexts; 

	private Dictionary<int, int> scoresDict = new Dictionary<int, int>();

	private GameMgr gameMgr;

	void Start(){
		gameMgr = GameObject.FindWithTag("GameMgr").GetComponent<GameMgr>();
	}

	public void Initialize(int playerNum1, int playerNum2, int playerNum3 = -1, int playerNum4 = -1){
		scoresDict.Add(playerNum1, 0);
		scoresTexts[0].color = PersistantData.colors[playerNum1];

		scoresDict.Add(playerNum2, 0);
		scoresTexts[1].color = PersistantData.colors[playerNum2];

		if (playerNum3 >= 0){
			scoresDict.Add(playerNum3, 0);
			scoresTexts[2].gameObject.SetActive(true);
			scoresTexts[2].color = PersistantData.colors[playerNum3];
		}

		if (playerNum4 >= 0){
			scoresDict.Add(playerNum4, 0);
			scoresTexts[3].gameObject.SetActive(true);
			scoresTexts[3].color = PersistantData.colors[playerNum4];
		}

		foreach (KeyValuePair<int,int> kvp in scoresDict){
			scoresDict[kvp.Key] = 0;
		}
	}

	void Update(){
		for (int i=0; i<scoresDict.Count; i++){
			Text txt = scoresTexts[i];
			txt.text = ""+scoresDict[PersistantData.indexToPlayer[i]];
		}
	}

	public void AddPoint(int playerNumber){
		scoresDict[playerNumber]++;
		if (winningPointValue > 0 && scoresDict[playerNumber] >= winningPointValue){
			gameMgr.EndGame();
		}
	}

	public Dictionary<int, int> GetScores(){
		return scoresDict;
	}
}
