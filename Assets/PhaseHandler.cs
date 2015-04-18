using UnityEngine;
using UnityEngine.UI;
using System.Collections;

 public enum Phase {gameStartPhase, spawnPhase, rotatePhase, battlePhase, resolvePhase, endPhase,victoryPhase};
public class PhaseHandler : MonoBehaviour {
	//These variables contain the toggle objects which we will use for phase visual and interaction
	Toggle spawnToggle;
	public Toggle rotateToggle;
	Toggle battleToggle;
	Toggle endToggle;
	public Phase currentPhase;
	public GameController go;
	public Tile focusedTile;
	public Tile targetTile;

	// Use this for initialization
	void Start () {
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	
		rotateToggle = GameObject.Find("RotatePhaseToggle").GetComponent<Toggle>();
		spawnToggle = GameObject.Find("SpawnPhaseToggle").GetComponent<Toggle>();
		battleToggle = GameObject.Find("BattlePhaseToggle").GetComponent<Toggle>();
		endToggle = GameObject.Find("EndPhaseToggle").GetComponent<Toggle>();
		currentPhase = Phase.gameStartPhase;

		spawnToggle.isOn = false;
		rotateToggle.isOn = false;
		battleToggle.isOn = false;
		endToggle.isOn = false;

		spawnToggle.interactable = false;
		rotateToggle.interactable = false;
		battleToggle.interactable = false;
		endToggle.interactable = false;
	

	}
	
	// Update is called once per frame
	void Update () { //this is primarily for the AI.
		if (Input.GetKeyUp (KeyCode.Space)) {
			nextPhase ();
		}
		if (currentPhase == Phase.spawnPhase && go.players [go.currentPlayer].troopSpawnCount <= 0) {
			nextPhase ();
		}

		if (currentPhase == Phase.rotatePhase && go.players [go.currentPlayer].rotateCards <= 0) {
			nextPhase ();
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
		//Debug.Log ("This player owns " + AgentUtil.loadPlayerTiles(currentPlayer).Capacity.ToString() + "tiles");
		go.players [go.currentPlayer].troopSpawnCount = AgentUtil.loadPlayerTiles (go.players [go.currentPlayer]).Count/3;
		spawnToggle.isOn = true;
		rotateToggle.interactable = true;
		rotateToggle.isOn = false;	
		battleToggle.isOn = false;
		currentPhase = Phase.spawnPhase;

		if (currentPlayer.playerType == 1) {
			Debug.Log ("Calling angry start deploy...");
			currentPlayer.startDeployPhase();
		}

	}

	public virtual void startRotationPhase(Player currentPlayer){
		battleToggle.interactable = true;
		spawnToggle.isOn = false;
		spawnToggle.interactable = false;
		rotateToggle.isOn = true;
		battleToggle.isOn = false;
		currentPhase= Phase.rotatePhase;

		if (currentPlayer.playerType == 1) {
			Debug.Log ("Calling angry start rotate...");
			currentPlayer.startRotatePhase();	
		}
	}

	public virtual void startBattlePhase(Player currentPlayer){
		Debug.Log ("STARTING BATTLE PHASE");
		rotateToggle.isOn = false;
		rotateToggle.interactable = false;
		battleToggle.isOn = true;
		endToggle.interactable = true;
		currentPhase = Phase.battlePhase;

		if (currentPlayer.playerType == 1) {
			Debug.Log ("Calling angry start battle...");
			currentPlayer.startAttackPhase();	
		}
	}

	public virtual void startResolvePhase(Player currentPlayer){
		Debug.Log ("STARTING RESOLVE PHASE");
		currentPhase = Phase.resolvePhase;
	}

	public virtual void nextPhase(){
		switch(currentPhase){
		
		case Phase.gameStartPhase:
			startNewTurn ( go.players[go.currentPlayer]);
			break;
				
		case Phase.spawnPhase:
			checkWin ();
			startRotationPhase (go.players[go.currentPlayer]);
			break;

		case Phase.rotatePhase:
			checkWin ();
			startBattlePhase(go.players[go.currentPlayer]);
		
			break;
			
		case Phase.battlePhase:
			checkWin ();
			disableAllToggles(go.players[go.currentPlayer]); //or endTurn
			Debug.Log ("This player has" + go.players[go.currentPlayer].rotateCards + "rotation cards");
			startResolvePhase (go.players[go.currentPlayer]);
			break;

		case Phase.resolvePhase:
			Debug.Log ("Starting resolve phase");
			startBattlePhase(go.players[go.currentPlayer]);
			break;

		case Phase.endPhase:
			checkWin ();
			go.players[go.currentPlayer].rotateCards++;
			Debug.Log ("This player has" + go.players[go.currentPlayer].rotateCards + "rotation cards");
			go.nextTurnUpdate();
			startNewTurn (go.players[go.currentPlayer]);
			break;
	
		}

	}

	public virtual Player checkWin( ){
		return null;
		
	}
}

