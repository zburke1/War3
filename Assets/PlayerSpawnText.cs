using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class PlayerSpawnText : MonoBehaviour {
	
	
	string playerString;
	Text countText;
	GameController go;
	public PhaseHandler ph;
	
	
	
	
	// Use this for initialization
	void Start () {
		countText = GetComponent<Text>();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	}
	
	// Update is called once per frame
	void Update () {

		if (ph.currentPhase == Phase.spawnPhase) {
			playerString = "Spawn " + go.players [go.currentPlayer].getSpawnCount() + " troops";
			if(go.players [go.currentPlayer].getSpawnCount() == 0)
				playerString = "Proceed to next phase";
			countText.text = playerString;
		} else
			countText.text = "";
		
	}


}

