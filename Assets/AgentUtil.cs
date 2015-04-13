using UnityEngine;
using System.Collections;

public class AgentUtil //: MonoBehaviour 
{

	private static GameController m_gamecontroller;
	private static double[,] probabilities = {
			{0.417, 0.106, 0.027, 0.007, 0.002, 0.000, 0.000, 0.000, 0.000, 0.000},
			{0.754, 0.363, 0.206, 0.091, 0.049, 0.021, 0.011, 0.005, 0.003, 0.001},
			{0.916, 0.656, 0.470, 0.315, 0.206, 0.134, 0.084, 0.054, 0.033, 0.021},
			{0.972, 0.785, 0.642, 0.477, 0.359, 0.253, 0.181, 0.123, 0.086, 0.057},
			{0.990, 0.890, 0.769, 0.638, 0.506, 0.397, 0.297, 0.224, 0.162, 0.118},
			{0.997, 0.934, 0.857, 0.745, 0.638, 0.521, 0.423, 0.329, 0.258, 0.193},
			{0.999, 0.967, 0.910, 0.834, 0.736, 0.640, 0.536, 0.446, 0.357, 0.287},
			{1.000, 0.980, 0.947, 0.888, 0.818, 0.730, 0.643, 0.547, 0.464, 0.380},
			{1.000, 0.990, 0.967, 0.930, 0.873, 0.808, 0.726, 0.646, 0.558, 0.480},
			{1.000, 0.994, 0.981, 0.954, 0.916, 0.861, 0.800, 0.724, 0.650, 0.568}
		};

	public static void setGameController() {
		m_gamecontroller = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/

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

		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			//iterate through tile neighbors
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[i].owner && neighbors[i].owner.playerID != -1) {
					if (tmpTile.getForces() > max) {
						tile = tmpTile;
						max = tmpTile.getForces();
					}
				}
			}
			
		}

		return tile;
	}

	public static ArrayList getTileWithEnemy(ArrayList tiles) {
		ArrayList tileList = new ArrayList();

		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			//iterate through tile neighbors
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[i].owner && neighbors[i].owner.playerID != -1) {
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

	//returns faceID of the face with the most controlled tiles
	public static int getFaceWithMostTiles(ArrayList tiles) {
		int[] faceValues = {0,0,0,0,0,0};
		for (int i = 0; i < tiles.Count; i++) {
			Tile tile = (Tile)tiles[i];
			faceValues[tile.face]++;
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
		return probabilities[attacker, defender];
	}
	
	//returns the attacking and defending tiles
	//returns empty array if none found...
	public static Tile[] findBestAttack(ArrayList tiles) {
		ArrayList tileList = getTileWithEnemy(tiles);
		//attack is 0, defend is 1
		Tile[] result = new Tile[2];
		double bestChance = 0;
		for (int i = 0; i < tiles.Count; i++) {
			Tile tmpTile = (Tile)tiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			for (int j = 0; j < 4; j++) {
				//check if neighbor of tile is enemy
				if (tmpTile.owner != neighbors[i].owner && neighbors[i].owner.playerID != -1) {
					double probVictory = calcAttackSuccess(tmpTile.getForces(), neighbors[i].getForces());
					if (probVictory > bestChance) {
						bestChance = probVictory;
						result[0] = tmpTile;
						result[1] = neighbors[i];
					}
				}
			}
		}
		return result;
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
				if (neighbors[i].owner.playerID == -1) { //might be zero! check!
					tileList.Add(neighbors[i]);
				}
			}
		}
		return tileList;
	}

	//takes full list of player tiles, and finds the safest tile on the best face to expand
	//note, function is biased towards tiles that are alone.
	public static Tile findSafeTile(ArrayList tiles) {
		//get player owner of tile set
		Player self = ((Tile)tiles[0]).owner;
		ArrayList tilevalues = new ArrayList();
		//get all tiles that are adjacent to an owned tile and are empty
		ArrayList emptyTiles = findEmptyAdjacentTiles(tiles);
		int mostOwnedFace = getFaceWithMostTiles(tiles);
		//int mostOwnedFace = getFaceWithMostArmies(tiles);
		for (int i = 0; i < emptyTiles.Count; i++) {
			//get the tile's neighbors.
			Tile tmpTile = (Tile)emptyTiles[i];
			Tile[] neighbors = tmpTile.getNeighborTiles();
			TileValue tv = new TileValue(tmpTile, 0);
			for (int j = 0; j < 4; j++) {
				//check to make sure the neighboring tile isn't also empty and is not owned by self
				if (tmpTile.owner != neighbors[i].owner && neighbors[i].owner != self) {
					//increment every time the empty tile of interest has an empty neighboring tile.
					tv.inc();
				}
			}
			//add the empty tile (now with a count of empty neighbors)
			tilevalues.Add(tv);
		}
		double best = 0;
		Tile bestTile = null;
		for (int i = 0; i < tilevalues.Count; i++) {
			TileValue tmptv = (TileValue)tilevalues[i];
			if (tmptv.value > best) {
				best = tmptv.value;
				bestTile = tmptv.getTile();
			}
		}
		return bestTile;
	}


}
