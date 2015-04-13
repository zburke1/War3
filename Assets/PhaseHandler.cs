using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhaseHandler : MonoBehaviour {
	//These variables contain the toggle objects which we will use for phase visual and interaction
	Toggle spawnToggle;
	Toggle rotateToggle;
	Toggle battleToggle;
	Toggle endToggle;
	enum Phase {spawnPhase, rotatePhase, battlePhase, endPhase};
	Phase currentPhase;

	// Use this for initialization
	void Start () {
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
	void Update () {

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
	}

	public virtual void startNewTurn(Player currentPlayer){
		spawnToggle.isOn = true;
		rotateToggle.interactable = true;
		rotateToggle.isOn = false;	
		battleToggle.isOn = false;

	}

	public virtual void startRotationPhase(Player currentPlayer){
		battleToggle.interactable = true;
		spawnToggle.isOn = false;
		spawnToggle.interactable = false;
		rotateToggle.isOn = true;
		battleToggle.isOn = false;

	}

	public virtual void startBattlePhase(Player currentPlayer){
		rotateToggle.isOn = false;
		rotateToggle.interactable = false;
		battleToggle.isOn = true;
		endToggle.interactable = true;
	}

}

