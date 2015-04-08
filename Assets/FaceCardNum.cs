using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]

public class FaceCardNum : MonoBehaviour {
	public Text rotateCardNum;
	public int rotationCards;
	// Use this for initialization
	void Start () {
		rotateCardNum = GetComponent<Text>();
		//rotationCards = 25;
		
	}
	
	// Update is called once per frame
	void Update () {
		rotateCardNum.text = rotationCards.ToString();
	}
	
	
}
