using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(RawImage))]

public class RotateButtonHUD : MonoBehaviour {
	public RawImage button;
	private PhaseHandler ph;
	public GameController go;


	// Use this for initialization
	void Start () {
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		button = GetComponent<RawImage>();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ph.currentPhase == Phase.rotatePhase && go.players[go.currentPlayer].playerType == 0 && go.players[go.currentPlayer].rotateCards > 0) {
			button.enabled = true;
		}
		else {
			button.enabled = false;
		}
		
	}
	
	
}
