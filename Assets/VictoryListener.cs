﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class VictoryListener: MonoBehaviour {
	
	
	string playerString;
	public Text victoryText;
	GameController go;
	public PhaseHandler ph;
	
	
	
	
	// Use this for initialization
	void Start () {
		victoryText = GetComponent<Text>();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ph.currentPhase == Phase.victoryPhase) {

			playerString = "Player " + go.winner.playerID + " wins";
			victoryText.text = playerString;
		} 
		else {
			playerString = "";
			victoryText.text = playerString;
		}
		
	}	
}

