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
		theToggle = GetComponent<Toggle> ();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	


	void Update () {
	}

	public void OnPointerClick(PointerEventData data)
	{
		if (theToggle.isActiveAndEnabled) {
			switch (phaseType) {

			case PhaseToggle.rotate:

				if(ph.currentPhase == Phase.spawnPhase){
					Debug.Log ("Player " + go.currentPlayer.ToString () + " Starting Rotation Phase");
					ph.nextPhase();	
				}
				break;

			case PhaseToggle.battle:
				if(ph.currentPhase == Phase.rotatePhase){
					ph.nextPhase();
					Debug.Log ("Player " + go.currentPlayer.ToString () + " Battle Phase");
				}
				break;
			
			case PhaseToggle.end:
				if(ph.currentPhase == Phase.battlePhase){
					ph.nextPhase();
					Debug.Log ("Turn Ends for Player " + go.currentPlayer.ToString ());
				}
				break;

			default: 
				break;
			
			//invoke start new turn with]
			//ph.startNewTurn ();
			}
		}
	}

	public void OnPointerEnter(PointerEventData data){
	
	}

	public void OnPointerExit(PointerEventData data){
	}

}
