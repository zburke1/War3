using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(RawImage))]

public class FaceCardGUI : MonoBehaviour {
	public RawImage faceCardHUD;
	public int rotationCards;
	// Use this for initialization
	void Start () {
		faceCardHUD = GetComponent<RawImage>();
		rotationCards = 16;
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
}
