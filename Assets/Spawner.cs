using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void init(int numPlayers, int numArmies) {

		for (int i = 0; i < numArmies; i++) {
			//for each player
			for (int j = 0; j < numPlayers; j++) {
				int tileMax = 36;
				int tileNum = Random.Range(0, tileMax);
				if (TILE.PLAYER == null) {
					//tile is not owned by a player
					if (playersOnFace(face, player) < 3) {
						//player has less than 3 tiles on that face, place army
						tile.player = player;
					} else {
						//player has more than 3 tiles. reset j and go again.
						j--;
					}
				} else {
					//tile already occupied. reset j and go again.
					j--;
				}
			}//for numPlayers
		}//for numArmies
	}

	int adjacentTiles(Tile t) {
		int adj = 0;
		Player player = t.Player;

		if (Tiles [t.id + 1].Player = player) {
			adj++;
		}
		if (Tiles [t.id - 1].Player = player) {
			adj++;
		}
		if (Tiles [t.id + SCALER].Player = player) {
			adj++;
		}
		if (Tiles [t.id - SCALER].Player = player) {
			adj++;
		}
		return adj;
	}

	int playersOnFace(Face face, Player player) {
		int numTiles = 0;
		for (int i = 0; i < 9; i++) {
			if (face.tiles [i].player == player) {
				numTiles++;
			}
		}
	}
}
