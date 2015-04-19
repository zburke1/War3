using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class ResolveGUI : MonoBehaviour {
	
	
	string playerString;
	public Text spawnText;
	GameController go;
	public PhaseHandler ph;
	
	
	
	
	// Use this for initialization
	void Start () {
		spawnText = GetComponent<Text>();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	}
	
	// Update is called once per frame
	void Update () {
			
		if (go.players [go.currentPlayer].showResolveCount && go.players [go.currentPlayer].resolveTileCount != 0) {
			spawnText.enabled = true;
			playerString = "Place " + go.players [go.currentPlayer].resolveTileCount + " troop(s)";
			spawnText.text = playerString;
		}
		else {
			spawnText.enabled = false;
		}
	
	}	
}

