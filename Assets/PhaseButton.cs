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
	// Use this for initialization

	void Start () {
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	//	ph = GameObject.Find("PhaseHandler").GetComponent<PhaseHandler>();
		theToggle = GetComponent<Toggle> ();

	}
	


	void Update () {
	//theToggle.onC
	}

	public void OnPointerClick(PointerEventData data)
	{
		switch(phaseType){
			
			
		case PhaseToggle.rotate:
			ph.startRotationPhase();
			Debug.Log ("Starting Rotation Phase");
			break;
			
		case PhaseToggle.battle:
			ph.startBattlePhase();
			Debug.Log ("Starting Battle Phase");
			break;
			
		case PhaseToggle.end:
			ph.disableAllToggles();
			Debug.Log ("Turn End");
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
