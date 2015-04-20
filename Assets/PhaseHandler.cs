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
		if (currentPhase == Phase.spawnPhase && go.players [go.currentPlayer].troopSpawnCount <= 0 && go.players [go.currentPlayer].playerType == 0) {
			nextPhase ();
		}

		if (currentPhase == Phase.rotatePhase && go.players [go.currentPlayer].rotateCards <= 0 && go.players [go.currentPlayer].playerType == 0) {
			nextPhase ();
		}

		if (currentPhase == Phase.resolvePhase && go.players [go.currentPlayer].resolveTileCount <=0 && go.players [go.currentPlayer].rotateCards <= 0 && go.players [go.currentPlayer].playerType == 0) {
			nextPhase ();
		}


	}

	public virtual void disableAllToggles(){
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

	public virtual void startNewTurn(){
		//Debug.Log ("This player owns " + AgentUtil.loadPlayerTiles(currentPlayer).Capacity.ToString() + "tiles");
		//go.players [go.currentPlayer].troopSpawnCount = AgentUtil.loadPlayerTiles (go.players [go.currentPlayer]).Count/3;
		go.players [go.currentPlayer].troopSpawnCount = go.players [go.currentPlayer].playerTileCount()/3;

		spawnToggle.isOn = true;
		rotateToggle.interactable = true;
		rotateToggle.isOn = false;	
		battleToggle.isOn = false;
		currentPhase = Phase.spawnPhase;

		if (go.players [go.currentPlayer].playerType == 1) {
			Debug.Log ("Calling angry start deploy...");
			go.players [go.currentPlayer].startDeployPhase();
		}

	}
	public virtual void endTurn(){

		currentPhase = Phase.endPhase;
		nextPhase ();
	}

	public virtual void startRotationPhase(){
		battleToggle.interactable = true;
		spawnToggle.isOn = false;
		spawnToggle.interactable = false;
		rotateToggle.isOn = true;
		battleToggle.isOn = false;
		currentPhase= Phase.rotatePhase;

		if (go.players [go.currentPlayer].playerType == 1) {
			Debug.Log ("Calling angry start rotate...");
			go.players [go.currentPlayer].startRotatePhase();	
		}
	}

	public virtual void startBattlePhase(){
		Debug.Log ("STARTING BATTLE PHASE");
		rotateToggle.isOn = false;
		rotateToggle.interactable = false;
		battleToggle.isOn = true;
		endToggle.interactable = true;
		currentPhase = Phase.battlePhase;

		if (go.players [go.currentPlayer].playerType == 1) {
			Debug.Log ("Calling angry start battle...");
			go.players [go.currentPlayer].startAttackPhase();	
		}
	}

	public virtual void startResolvePhase(){
		Debug.Log ("STARTING RESOLVE PHASE");
		currentPhase = Phase.resolvePhase;

		if (go.players [go.currentPlayer].playerType == 1) {
			go.players [go.currentPlayer].startResolvePhase();
		}
	}

	public virtual void nextPhase(){
		switch(currentPhase){
		
		case Phase.gameStartPhase:
			startNewTurn ( );
			break;
				
		case Phase.spawnPhase:
			go.checkWin ();
			startRotationPhase ();
			break;

		case Phase.rotatePhase:
			go.checkWin ();
			startBattlePhase();
		
			break;
			
		case Phase.battlePhase:
			go.checkWin ();
			//Debug.Log ("This player has" + go.players[go.currentPlayer].rotateCards + "rotation cards");
			startResolvePhase ();
			break;

		case Phase.resolvePhase:
			Debug.Log ("Starting resolve phase");
			startBattlePhase();
			break;

		case Phase.endPhase:
			disableAllToggles(); //or endTurn
			go.players[go.currentPlayer].rotateCards++;
			Debug.Log ("Turn Ends for Player " + go.currentPlayer.ToString ());
			Debug.Log ("This player has" + go.players[go.currentPlayer].rotateCards + "rotation cards");
			go.nextTurnUpdate();
			startNewTurn ();
			break;

		case Phase.victoryPhase:
			spawnToggle.isOn = false;
			rotateToggle.isOn = false;
			battleToggle.isOn = false;
			endToggle.isOn = false;
			
			spawnToggle.interactable = false;
			rotateToggle.interactable = false;
			battleToggle.interactable = false;
			endToggle.interactable = false;
			break;
		}

	}

	public virtual Player checkWin( ){
		return null;
		
	}
}

