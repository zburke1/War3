
using UnityEngine;
using System.Collections;


public class Player //: MonoBehaviour
{
	//HUMAN=0; AI = 1;
	private int playerType;
	public Color playerColor;
	private Color[] playerColors = { Color.blue, Color.red, Color.magenta, Color.yellow, Color.cyan, new Color(0.2F, 0.3F, 0.4F)};
	public int playerID;
	//todo: look into this
	//Tile[] ownedTiles = new Tile[54];


	public Player (){
		playerID = -1; //Nonplayer
		playerColor = Color.white;
		playerType = -1; //Nonplayer
	}

	public Player(int id, int type, int color) {
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
	}

	void Start() {
		playerID = 0;
		playerColor = Color.gray;
		playerType = -1;
	}

	void Update() {

	}

	public void setPlayer(int id, int type, int color) {
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
	}



}


