using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class PlayerTurntext : MonoBehaviour {


	string playerString;
	Text whoseTurn;
	GameController go;




	// Use this for initialization
	void Start () {
		whoseTurn = GetComponent<Text>();
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	// Update is called once per frame
	void Update () {
		playerString = "Turn: Player " + go.currentPlayer.ToString ();
		whoseTurn.text = playerString;

	}
}

