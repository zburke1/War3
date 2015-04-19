using UnityEngine;
using System.Collections;

public class AgentUtil //: MonoBehaviour 
{

	private static GameController m_gamecontroller;
	private static double[,] probabilities = {
		//rows: num attacking armies
		//cols: num defending armies
			{0.000,	0.000, 0.000, 0.000, 0.000, 0.000, 0.000, 0.000, 0.000, 0.000, 0.000},
			{1.000, 0.417, 0.106, 0.027, 0.007, 0.002, 0.000, 0.000, 0.000, 0.000, 0.000},
			{1.000, 0.754, 0.363, 0.206, 0.091, 0.049, 0.021, 0.011, 0.005, 0.003, 0.001},
			{1.000, 916, 0.656, 0.470, 0.315, 0.206, 0.134, 0.084, 0.054, 0.033, 0.021},
		    {1.000, 0.972, 0.785, 0.642, 0.477, 0.359, 0.253, 0.181, 0.123, 0.086, 0.057},
			{1.000, 0.990, 0.890, 0.769, 0.638, 0.506, 0.397, 0.297, 0.224, 0.162, 0.118},
			{1.000, 0.997, 0.934, 0.857, 0.745, 0.638, 0.521, 0.423, 0.329, 0.258, 0.193},
			{1.000, 0.999, 0.967, 0.910, 0.834, 0.736, 0.640, 0.536, 0.446, 0.357, 0.287},
			{1.000, 1.000, 0.980, 0.947, 0.888, 0.818, 0.730, 0.643, 0.547, 0.464, 0.380},
			{1.000, 1.000, 0.990, 0.967, 0.930, 0.873, 0.808, 0.726, 0.646, 0.558, 0.480},
			{1.000, 1.000, 0.994, 0.981, 0.954, 0.916, 0.861, 0.800, 0.724, 0.650, 0.568}
		};

	public static void setGameController() {
		m_gamecontroller = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}

	//this is run before every other function at the start of the turn (and potentially every player action)
	public static ArrayList loadPlayerTiles(Player owner) {
		ArrayList tiles = new ArrayList();
		for (int i = 1; i < 7; i++) {
			for (int j = 1; j <10; j++) {
				Tile tile = m_gamecontroller.faces[i,j].gameObject.GetComponent<Tile>();
				if (tile.owner == owner) {
					tiles.Add(tile);
				}
			}
		}
		return tiles;
	}

	public static ArrayList loadPlayerTiles(int playerID) {
		ArrayList tiles = new ArrayList();
		for (int i = 1; i < 7; i++) {
			for (int j = 1; j <10; j++) {
				Tile tile = m_gamecontroller.faces[i,j].gameObject.GetComponent<Tile>();
				if (tile.getPlayerID() == playerID) {
					tiles.Add(tile);
				}
			}
		}
		return tiles;
	}

	public static Tile getTileWithLargestArmy(ArrayList tiles) {
		int max = 0;
		Tile tile = null;

		for (int i = 0; i < tiles.Count; i++) {
			if (((Tile)tiles[i]).getForces() > max) {
				tile = (Tile)tiles[i];
				max = tile.getForces();
			}
		}

		return tile;
	}

	public static Tile getTileWithLargestArmyAndEnemy(ArrayList tiles) {
		int max = 0;
		Tile tile = null;
		if (tiles.Count == 0) {
			Debug.Log ("getTileWithLargestArmyAndEnemy: empty tiles parameter");
		}
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			//iterate through tile neighbors
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[j].owner && neighbors[j].owner.playerID != -1) {
					if (tmpTile.getForces() > max) {
						tile = tmpTile;
						max = tmpTile.getForces();
					}
				}
			}
			
		}

		return tile;
	}

	public static Tile getTileWithLargestArmyNoEnemy(ArrayList tiles) {
		int max = 0;
		Tile tile = null;
		if (tiles.Count == 0) {
			Debug.Log ("getTileWithLargestArmyAndEnemy: empty tiles parameter");
		}
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			if (tileHasEnemies(tmpTile)) {
				continue;
			} else {
				if (tmpTile.getForces() > max) {
					max = tmpTile.getForces ();
					tile = tmpTile;
				}
			}
		}
		if (tile == null) {
			Debug.Log ("Attempted unsuccessfully to find largestArmyTileWithNoEnemy");
		}
		return tile;
	}

	public static bool tileHasEnemies(Tile tile) {
		Tile[] neighbors = tile.getNeighborTiles ();
		for (int i = 0; i < 4; i++) {
			if (areEnemies (tile, neighbors[i])) {
				return true;
			}
		}
		return false;
	}

	public static bool areEnemies(Tile a, Tile b) {
		if (a.owner != b.owner && b.owner.playerID != -1) {
			return true;
		} else {
			return false;
		}
	}
	public static ArrayList getTileWithEnemy(ArrayList tiles) {
		ArrayList tileList = new ArrayList();

		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			//iterate through tile neighbors
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[j].owner && neighbors[j].owner.playerID != -1) {
					tileList.Add(tmpTile);
				}
			}
			
		}

		return tileList;
	}

	public static ArrayList getTilesOnFace(ArrayList tiles, int face) {
		ArrayList faceTiles = new ArrayList();
		for (int i = 0; i < tiles.Count; i++) {
			Tile tile = (Tile)tiles[i];
			if (tile.face == face) {
				faceTiles.Add(tile);
			}
		}
		return faceTiles;
	}

	//returns faceID of the face the player occupies the most.
	public static int getFaceWithMostTiles(ArrayList tiles) {
		int[] faceValues = {0,0,0,0,0,0};
		for (int i = 0; i < tiles.Count; i++) {
			Tile tile = (Tile)tiles[i];
			faceValues[tile.face-1]++;
		}
		int max = 0;
		int maxi = 0;
		for (int i = 0; i < 6; i++) {
			if (faceValues[i] > max) {
				max = faceValues[i];
				maxi = i+1; //increment by one, because faceIDs start from 1.
			}
		}

		return maxi;
	}



	public static int getFaceWithMostArmies(ArrayList tiles) {
		int[] faceValues = {0,0,0,0,0,0};
		for (int i = 0; i < tiles.Count; i++) {
			Tile tile = (Tile)tiles[i];
			faceValues[tile.face]+= tile.getForces();
		}
		int max = 0;
		int maxi = 0;
		for (int i = 0; i < 6; i++) {
			if (faceValues[i] > max) {
				max = faceValues[i];
				maxi = i;
			}
		}
		return maxi;
	}

	//calculates the probability of a successful full attack
	public static double calcAttackSuccess(int attacker, int defender) {
		//why is this -1?
		//return probabilities[attacker-1, defender-1];
		return probabilities [attacker, defender];
	}
	
	//returns the attacking and defending tiles. defending tiles must have an enemy army on them.
	//returns empty array if none found...
	public static TileValue findBestAttack(ArrayList tiles) {
		ArrayList tileList = getTileWithEnemy(tiles);
		if (tileList.Count == 0) {
			return null;
		}
		//attack is 0, defend is 1
		TileValue pair = new TileValue ();
		double bestChance = 0;
		for (int i = 0; i < tileList.Count; i++) {
			//Tile tmpTile = (Tile)tiles[i];
			Tile tmpTile = (Tile)tileList[i];
			if (tmpTile.getForces () == 1) {
				//only one army on this tile. move on.
				continue;
			}
			Tile[] neighbors = tmpTile.getNeighborTiles();
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[j].owner && neighbors[j].owner.playerID != -1) {
					double probVictory = calcAttackSuccess(tmpTile.getForces(), neighbors[j].getForces());
					if (probVictory > bestChance) {
						bestChance = probVictory;
						pair.setTiles(tmpTile, neighbors[j]);
						pair.value = probVictory;

					}
				}
			}
		}
		return pair;
	}

	//TODO: see below check!
	//returns an arraylist uncontrolled tiles adjacent to any owned tiles
	public static ArrayList findEmptyAdjacentTiles(ArrayList tiles) {
		ArrayList tileList = new ArrayList();
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (neighbors[j].owner.playerID == -1) { //might be zero! check!
					tileList.Add(neighbors[j]);
				}
			}
		}
		return tileList;
	}

	public static ArrayList findEmptyAdjacentTiles(Tile tile) {
		ArrayList tileList = new ArrayList();
		Tile[] neighbors = tile.getNeighborTiles();
		for (int j = 0; j < 4; j++) {
			//check if neighbor of tile is enemy
			if (neighbors[j].owner.playerID == -1) { //might be zero! check!
				tileList.Add(neighbors[j]);
			}
		}

		return tileList;
	}


	//returns the same as the above, but returns a TileValue pair.
	public static ArrayList findEmptyAdjacentTilesWithPairing(ArrayList tiles) {
		ArrayList tileList = new ArrayList();
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (neighbors[j].owner.playerID == -1) { //might be zero! check!
					//tileList.Add(neighbors[i]);
					//not necessarily number of armies, but that's what I needed this function for so nyeah
					TileValue tv = new TileValue(tmpTile, neighbors[j], (double)tmpTile.getForces());
					tileList.Add (tv);
				}
			}
		}
		return tileList;
	}
	
	//takes full list of player tiles, and finds the safest tile on the best face to expand
	//note, function is biased towards tiles that are alone.
	public static TileValue findSafeExpandTile(ArrayList tiles) {
		//get player owner of tile set
		Player self = ((Tile)tiles[0]).owner;
		ArrayList tilevalues = new ArrayList();
		//get all tiles that are adjacent to an owned tile and are empty
		ArrayList emptyTiles = findEmptyAdjacentTiles(tiles);
		//int mostOwnedFace = getFaceWithMostTiles(tiles);
		//int mostOwnedFace = getFaceWithMostArmies(tiles);
		for (int i = 0; i < emptyTiles.Count; i++) {
			//get the tile's neighbors.
			Tile tmpTile = (Tile)emptyTiles[i];
			//Tile[] neighbors = tmpTile.getNeighborTiles();
			int numEmptyNeighbors = (findEmptyAdjacentTiles(tmpTile)).Count;
			TileValue tv = new TileValue(tmpTile, 0);
			//for (int j = 0; j < 4; j++) {
				//check to make sure the neighboring tile isn't also empty and is not owned by self
				//if (tmpTile.owner != neighbors[j].owner && neighbors[j].owner != self) {
					//increment every time the empty tile of interest has an empty neighboring tile.
					//tv.inc();
				//}
			tv.value = numEmptyNeighbors;
			//}
			//add the empty tile (now with a count of empty neighbors)
			tilevalues.Add(tv);
		}
		double best = 0;
		//Tile bestTile = null;
		TileValue bestTV = null;
		reshuffle (tilevalues);
		for (int i = 0; i < tilevalues.Count; i++) {
			TileValue tmptv = (TileValue)tilevalues[i];
			if (tmptv.value > best) {
				best = tmptv.value;
				bestTV = tmptv;
			}
		}
		Tile defendingTile = bestTV.getTile ();
		//found best tile to attack (=defendingTile). now find the owned tile touching it.

		ArrayList attackingTiles = findNeighboringOwnedTiles (self, defendingTile);
		if (attackingTiles.Count == 0) {
			Debug.Log ("safeExpand: Something went terribly wrong. No owned tiles adjacent to tile " + defendingTile.tileID);
		}
		Tile attackingTile = getTileWithLargestArmyNoEnemy (attackingTiles);

		if (attackingTile == null) {
			//there are no owned tiles adjacent to the target tile (=defendingTile).
			//just use the one with the most armies.
			attackingTile = getTileWithLargestArmy(attackingTiles);
		}
		if (attackingTile == null || attackingTile.getForces() < 2) {
			Debug.Log ("safeExpand: No tiles with enough armies, or null");
			return null;
		}

		return new TileValue (attackingTile, defendingTile, 0);
	}

	public static ArrayList findNeighboringOwnedTiles(Player player, Tile tile) {
		ArrayList result = new ArrayList();
		Tile[] neighbors = tile.getNeighborTiles();
		ArrayList tiles = new ArrayList ();
		for (int i = 0; i < 4; i++) {
			if (neighbors[i].owner == player) {
				tiles.Add (neighbors[i]);
			}
		}
		if (tiles.Count == 0) {
			Debug.Log ("No owned neighbors found for tile " + tile.tileID);
			return null;
		} else if (tiles.Count == 1) {
			Debug.Log ("One neighbor found for tile " + tile.tileID + ": " + ((Tile)tiles[0]).tileID); 
			return tiles;
		} else {

			//all tiles are touching an enemy tile.
			//todo: make this not hardcoded.
			//return checkTile;
			return tiles;
		}
	}
	




	//an agressive placement function for Angry. Finds a empty corner on a face an opponent owns.
	//unfortunately, this is always going to attack the same face, because it starts counting from the same place.
	public static Tile findEmptyCorner(Player self, int faceOption = -1) {
		ArrayList corners = new ArrayList();
		//faceOption override allows specification of face. untested...
		if (faceOption != -1) {
			for (int j = 1; j <10; j++) {
				//is a corner tile
				if (j == 1 || j == 3 || j == 7 || j == 9) {
					Tile tile = m_gamecontroller.faces[faceOption,j].gameObject.GetComponent<Tile>();
					//check if selected corner tile is empty
					//todo: check id!
					if (tile.getPlayerID() == -1) {
						Tile center = m_gamecontroller.faces[faceOption,5].gameObject.GetComponent<Tile>();
						//check if the center is an enemy
						if (center.owner != self) {
							corners.Add(tile);
						}
					}
				}
			}
		} else {
			for (int i = 1; i < 7; i++) {
				for (int j = 1; j <10; j++) {
					//is a corner tile
					if (j == 1 || j == 3 || j == 7 || j == 9) {
						Tile tile = m_gamecontroller.faces[i,j].gameObject.GetComponent<Tile>();
						//check if selected corner tile is empty
						//todo: check id!
						if (tile.getPlayerID() == -1) {
							Tile center = m_gamecontroller.faces[i,5].gameObject.GetComponent<Tile>();
							//check if the center is an enemy
							if (center.owner != self) {
								corners.Add(tile);
							}
						}
					}
				}
			}
		} 
		
		//check the corner tiles for any that are adjacent to a friendly tile, and which are close to faces with the most forces
		//priority given to adjacent tile
		ArrayList tilevalues = new ArrayList();
		for (int i = 0; i < corners.Count; i++) {
			Tile tile = (Tile)corners[i];
			Tile[] neighbors = tile.getNeighborTiles();
			//check neighbors for friendly tiles, and their face values.
			for (int j = 0; j < 4; j++) {
				TileValue tv = new TileValue(tile, 0);
				if (neighbors[j].owner == self) {
					//note: 5 is a weight
					tv.inc(5);
				}
				int faceArmies = getNumOwnedTilesOnFace(self, neighbors[j].face);
				tv.inc(faceArmies);
				tilevalues.Add(tv);
			}
		}
		//now find the best corner to place an army.
		double maxi = 0;
		Tile bestTile = null;
		//shuffle tiles
		reshuffle(tilevalues);
		for (int i = 0; i < tilevalues.Count; i++) {
			TileValue tv = (TileValue)tilevalues[i];
			if (tv.value > maxi) {
				maxi = tv.value;
				bestTile = tv.getTile();
			}
		}
		return bestTile;
	}

	public static int getNumOwnedTilesOnFace(Player player, int face) {
		int count = 0;
		for (int i = 1; i < 10; i++) {
			Tile tile = m_gamecontroller.faces[face,i].gameObject.GetComponent<Tile>();
			if (tile.owner == player) {
				count++;
			}
		}
		return count;
	}

	//gets the total number of enemy armies adjacent to this tile.
	public static int getNumEnemiesAdjacent(Tile tile) {
		Tile[] neighbors = tile.getNeighborTiles();
		int num = 0;
		for (int i = 0; i < 4; i++) {
			Tile neighbor = neighbors[i];
			//check tile occupied.
			if (neighbor.owner != tile.owner && neighbor.getPlayerID() != -1) {
				num += neighbor.getForces();
			}
		}
		return num;
	}

	//search for a tile with a disparate number of enemy forces
	public static Tile checkUrgentTile(ArrayList tiles) {
		ArrayList tileList = new ArrayList();
		ArrayList tilevalues = new ArrayList();
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			TileValue tv = new TileValue(tmpTile, 0.0);
			//compare the number of tiles
			int numArmies = getNumEnemiesAdjacent(tmpTile);
			tv.inc(tmpTile.getForces() - numArmies);
		}
		double mini = 0;
		Tile tile = null;
		reshuffle(tilevalues);
		for (int i = 0; i < tilevalues.Count; i++) {
			TileValue tv = (TileValue)tilevalues[i];
			if (tv.value < -3) {
				if (tv.value < mini) {
					mini = tv.value;
					tile = tv.getTile();
				}
			}
		}
		return tile;
	}

	//returns a faceID for a face if an enemy has more than 4 occupied tiles on that face
	//incomplete
	public static int denseFace() {
		ArrayList faces = new ArrayList();
		Player[] players = m_gamecontroller.players;

		for (int i = 0; i < players.Length; i ++) {
			
		}
		return 0;
	}

	//finds a random empty tile
	public static Tile findEmptyTile() {
		ArrayList tiles = loadPlayerTiles (-1);
		reshuffle (tiles);
		return (Tile)tiles [0];
	}

	//finds potential rotations.
	public void scoutRotate(ArrayList tiles) {

	}

	public static ArrayList getTilesWithArmiesAtLeast(ArrayList tiles, int armies) {
		ArrayList tmp = new ArrayList();
		for (int i = 0; i < tiles.Count; i++) {
			Tile tile = (Tile) tiles[i];
			if (tile.getForces() > armies) {
				tmp.Add (tile);
			}
		}
		return tmp;
	}

	static void reshuffle(ArrayList tiles)
    {
		/*
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < tiles.Count; t++ )
        {
			if ((Tile)(tiles[t]) != null) {
				Tile tmp = (Tile)(tiles[t]);
				int r = Random.Range(t, tiles.Count);
				tiles[t] = tiles[r];
				tiles[r] = tmp;
			}
            
        }
        */
    }
	
	/*
	//takes full list of player tiles, and finds the safest tile on the best face to expand, but only if there are more than 3 armies
	//note, function is biased towards tiles that are alone.
	public static Tile findExpansionTile(ArrayList tiles) {
		//get player owner of tile set
		Player self = ((Tile)tiles[0]).owner;
		ArrayList tilevalues = new ArrayList();

		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			if (tmpTile.getForces() < 4) {
				tiles.Remove(i);
			}
		}
		if (tiles.Count == 0) {
			Tile ret = null;
			return ret;
		}
		//get all tiles that are adjacent to an owned tile and are empty
		ArrayList emptyTiles = findEmptyAdjacentTilesWithPairing(tiles);
		int mostOwnedFace = getFaceWithMostTiles(tiles);

		for (int i = 0; i < emptyTiles.Count; i++) {
			//get the tile's neighbors.
			//Tile tmpTile = (Tile)emptyTiles[i];
			TileValue tmptv = (TileValue)emptyTiles[i];
			Tile ownedTile = tmptv.getTiles()[0];
			Tile emptyTile = tmptv.getTiles ()[1];
			Tile[] emptyTileNeighbors = (tmptv.getTiles()[1]).getNeighborTiles();
			//TileValue tv = new TileValue(tmpTile, 0);
			//TileValue focusNeighbor = new TileValue(focusTile, 0
			ArrayList focusNeighbors = new ArrayList();
			for (int j = 0; j < 4; j++) {
				TileValue tv = new TileValue(focusTile, neighbors[i], 0);
				int numEmpty = 0;
				//check to make sure the neighboring tile isn't also empty and is not owned by self
				if (ownedTile.owner != emptyTileNeighbors[i].owner && emptyTileNeighbors[i].owner != self) {
					//increment every time the empty tile of interest has an empty neighboring tile.
					//tv.inc ();
					numEmpty++;
				}
				if (numEmpty > 2) {
					//return
				}
			}
			//add the empty tile (now with a count of empty neighbors)
			tilevalues.Add(tv);
		}
		double best = 0;
		Tile bestTile = null;
		reshuffle (tilevalues);
		for (int i = 0; i < tilevalues.Count; i++) {
			TileValue tmptv = (TileValue)tilevalues[i];
			if (tmptv.value > best) {
				best = tmptv.value;
				bestTile = tmptv.getTile();
			}
		}
		return bestTile;
	}
*/
}
