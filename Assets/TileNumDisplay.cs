using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]

public class TileNumDisplay : MonoBehaviour {
	GameObject gameControllerObject;
	GameController gc;
	public int face;
	public int tile;
	private Text troopNumText;
	public int troopNum =0;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindObjectOfType (typeof(GameController)) as GameController;
		troopNumText = GetComponent<Text> ();
		troopNum = gc.getTroops (face, tile);
	
	

		//gameControllerObject.Find("GameController");
		//gc = (gc) gameControllerObject.GetComponent(typeof(GameController));
	}
	// Update is called once per frame
	void Update () {
		int num = gc.getTroops (face, tile);
		if (num == 0) {
			troopNumText.text = "";
		} else {
			troopNumText.text = gc.getTroops (face, tile).ToString ();
			troopNumText.color = gc.findTileFromIndex (face, tile).owner.playerColorText;
		}
		//debug.log( gc.randomInt);
	}

}
