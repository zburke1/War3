using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


[RequireComponent (typeof(Toggle))]

public class PhaseButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	PhaseHandler ph;
	public string hoverText;
	Toggle theToggle; 
	public enum  PhaseToggle {spawn, rotate, battle, end};
	public PhaseToggle phaseType;
	GameController go;
	// Use this for initialization

	void Start () {
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	//	ph = GameObject.Find("PhaseHandler").GetComponent<PhaseHandler>();
		theToggle = GetComponent<Toggle> ();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;

	}
	


	void Update () {
	//theToggle.onC
	}

	public void OnPointerClick(PointerEventData data)
	{
		switch(phaseType){
			
			
		case PhaseToggle.rotate:
			ph.startRotationPhase(go.players[go.currentPlayer]);
			Debug.Log ("Player " + go.currentPlayer.ToString() + " Starting Rotation Phase");
			break;
			
		case PhaseToggle.battle:
			ph.startBattlePhase(go.players[go.currentPlayer]);
			Debug.Log ("Player " + go.currentPlayer.ToString()+" Battle Phase");
			break;
			
		case PhaseToggle.end:
			ph.disableAllToggles(go.players[go.currentPlayer]);
			Debug.Log ("Turn Ends for Player " + go.currentPlayer.ToString());
			go.nextTurnUpdate();
			ph.startNewTurn(go.players[go.currentPlayer]);
			break;
			
		default: 
			break;
			
			//invoke start new turn with]
			//ph.startNewTurn ();
		}
	}

	public void OnPointerEnter(PointerEventData data){
	
	}

	public void OnPointerExit(PointerEventData data){
	}

}
