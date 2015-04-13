using UnityEngine;
using UnityEngine.UI;
using System.Collections;

 public enum Phase {spawnPhase, rotatePhase, battlePhase, endPhase};
public class PhaseHandler : MonoBehaviour {
	//These variables contain the toggle objects which we will use for phase visual and interaction
	Toggle spawnToggle;
	public Toggle rotateToggle;
	Toggle battleToggle;
	Toggle endToggle;
	public Phase currentPhase;
	public GameController go;

	// Use this for initialization
	void Start () {
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	
		rotateToggle = GameObject.Find("RotatePhaseToggle").GetComponent<Toggle>();
		spawnToggle = GameObject.Find("SpawnPhaseToggle").GetComponent<Toggle>();
		battleToggle = GameObject.Find("BattlePhaseToggle").GetComponent<Toggle>();
		endToggle = GameObject.Find("EndPhaseToggle").GetComponent<Toggle>();
		currentPhase = Phase.spawnPhase;



		spawnToggle.isOn = true;
		rotateToggle.isOn = false;
		battleToggle.isOn = false;
		endToggle.isOn = false;

		spawnToggle.interactable = true;
		rotateToggle.interactable = true;
		battleToggle.interactable = false;
		endToggle.interactable = false;
	

	}
	
	// Update is called once per frame
	void Update () { //this is primarily for the AI.

		if(currentPhase == Phase.spawnPhase){
		
		}

		else if(currentPhase == Phase.rotatePhase){
		}

		else if(currentPhase == Phase.battlePhase){
		}

		else if(currentPhase == Phase.endPhase){

		}

	}

	public virtual void disableAllToggles(Player currentPlayer){
		spawnToggle.isOn = false;
		rotateToggle.isOn = false;
		battleToggle.isOn = false;
		endToggle.isOn = false;

		spawnToggle.interactable = false;
		rotateToggle.interactable = false;
		battleToggle.interactable = false;
		endToggle.interactable = false;
		currentPhase = Phase.endPhase;
	}

	public virtual void startNewTurn(Player currentPlayer){
	//	Debug.Log ("This player owns " + AgentUtil.loadPlayerTiles(currentPlayer).Capacity.ToString() + "tiles");

		spawnToggle.isOn = true;
		rotateToggle.interactable = true;
		rotateToggle.isOn = false;	
		battleToggle.isOn = false;
		currentPhase = Phase.spawnPhase;

	}

	public virtual void startRotationPhase(Player currentPlayer){
		battleToggle.interactable = true;
		spawnToggle.isOn = false;
		spawnToggle.interactable = false;
		rotateToggle.isOn = true;
		battleToggle.isOn = false;
		currentPhase= Phase.rotatePhase;

	}

	public virtual void startBattlePhase(Player currentPlayer){
		rotateToggle.isOn = false;
		rotateToggle.interactable = false;
		battleToggle.isOn = true;
		endToggle.interactable = true;
		currentPhase = Phase.battlePhase;
	}

	public virtual void nextPhase(){
		switch(currentPhase){
		
		case Phase.spawnPhase:
			startRotationPhase (go.players[go.currentPlayer]);
			break;

		case Phase.rotatePhase:
			startBattlePhase(go.players[go.currentPlayer]);
			break;
			
		case Phase.battlePhase:
			disableAllToggles(go.players[go.currentPlayer]); //or endTurn
			break;

		case Phase.endPhase:
			go.nextTurnUpdate();
			startNewTurn (go.players[go.currentPlayer]);
			break;
	
		}

	}
}

