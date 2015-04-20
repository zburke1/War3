using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tile : MonoBehaviour {
	public GameController go;
	public PhaseHandler ph;
	public PhaseHandler m_PhaseHandler;
	public RotateScript m_RotateScript;
	private int forces = 0;
	//deprecated...
	private int playerID = 0;
	public Player owner;
	//GameObject owner;
	private bool mousereleased = false;
	//[0:3] - > right, up, left, down
	int[] neighbors = new int[4]; //this must be private, otherwise unity loses it's shit.
	Tile[] tileNeighbors = new Tile[4];
	//1-54
	public int tileID = 0;
	public int face = 0;
	public bool tileFocus;
	public bool isAttacking;
	public bool isResolving;

	List<int> centerTiles = new List<int> {5,14,41,32,23,50};

	// Use this for initialization
	void Start () {
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	//	ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		ph = GameObject.Find("PhaseHandlerGO").GetComponent<PhaseHandler>();

		List<int> centerTiles = new List<int> {5,14,41,32,23,50};

		m_RotateScript = GameObject.FindObjectOfType(typeof(RotateScript)) as RotateScript;
		m_PhaseHandler = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		//owner = GameObject.Instantiate (Player);
		if (owner == null) {
			owner = new Player (); //initialize id = 0, playerColor = grey, playerType = -1 (for nonplayer entity type)
		}
		Debug.Log ("Why am I starting again?");

	}
	
	// Update is called once per frame
	void Update () {
		//ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		//renderer.material.color = owner.playerColor;
	}

	void OnMouseOver(){
		switch (ph.currentPhase) {
			case Phase.spawnPhase:
				if (tileID != 0) {
					renderer.material.color = Color.green;
				}
				break;
			case Phase.rotatePhase:

				break;
			case Phase.battlePhase:
				if (tileID != 0) {
					if (owner.playerID == 1) {
						renderer.material.color = owner.playerColorLight;
					}
				}
				break;

		}
		
		/*}

		//	renderNeighbors(getNeighborTiles ());
			renderer.material.SetInt (14, 300);
		/*	for (int i = 0; i < 4; i++) {
				Debug.Log(tileID + ": " + neighbors[i]);
			}
			*/
		//}

	}

	void renderNeighbors( Tile[] neighbors) {
		for (int i = 0; i < neighbors.Length; i++) {
			neighbors[i].renderer.material.color = Color.gray;
		}
	}

	public virtual int getForces(){
		return forces;
	} 

	public virtual void setForces(int armies){
		forces = armies;
	} 

	public virtual int getPlayerID() {
		return playerID;
	}
	
	public Player getOwner(){
		return owner;
	}

	void OnMouseExit(){

		
		switch (ph.currentPhase) {

		case Phase.spawnPhase:
			if (tileID != 0) {
				renderer.material.color = owner.playerColor;
			}
			break;

		case Phase.rotatePhase:
			if (tileID != 0) {
				renderer.material.color = owner.playerColor;
			}
			break;

		case Phase.battlePhase:

			if (tileID != 0) {
				if (owner.playerID == 1) {
					renderer.material.color = owner.playerColor;	
				}
			}
			
			break;
		
		
		

		}
	}
		
	void OnMouseDown(){


		switch (ph.currentPhase) {

		case Phase.spawnPhase:
			if (tileID != 0 && go.players[go.currentPlayer].troopSpawnCount > 0 && go.players[go.currentPlayer].playerID == owner.playerID) {
				go.players[go.currentPlayer].troopSpawnCount --;
				setOwner( go.players[go.currentPlayer]);
				forces++;
				//go.checkWin();
			}
			break;
		case Phase.rotatePhase:
			break;

		case Phase.battlePhase:

			if (isAttacking) {
				if (owner.playerID == 1) {
					renderer.material.color = owner.playerColor;
					setNeighborColors(false, this);
					isAttacking = false;
				}
			} else {
				if (owner.playerID == 1) {
					renderer.material.color = owner.playerColorLight;
					setNeighborColors(true, this);
					isAttacking = true;
				} else {
					for (int i = 0; i < 4; i++) {
						Tile neighbor = tileNeighbors[i];
						if (neighbor.isAttacking) {
							neighbor.owner.attack(neighbor, this);
							setNeighborColors(false, neighbor);
							neighbor.isAttacking = false;
						}
					}
				}
			}
		


			break;

		case Phase.resolvePhase:
			if (isResolving && go.players [go.currentPlayer].playerType == 0) {
				if (owner.resolveTileCount > 0) {
					forces += 1;
					owner.resolveTileCount--;

					if (owner.resolveTileCount == 0 && go.players [go.currentPlayer].playerType == 0) {
						owner.stopResolving ();

					}

				} else {
					owner.stopResolving ();

				}
			}

			break;
		}
	}

	public void setNeighborColors(bool highlight, Tile tile) {
		Tile[] neighbors = tile.getNeighborTiles();

		for (int i = 0; i < 4; i++) {
			if (highlight)
				neighbors[i].renderer.material.color = Color.gray;
			else {
				if (neighbors[i].isResolving) {
					neighbors[i].renderer.material.color = neighbors[i].owner.playerColorLight;
				} else {
					neighbors[i].renderer.material.color = neighbors[i].owner.playerColor;
				}
			}
		}
	}


	public void renderOwnerColor() {
		renderer.material.color = owner.playerColor;
	}

	public void setID(int id) {
		tileID = id;
	}

	public void setFace(int face) {
		this.face = face;
	}

	public void setNeighbors(int right, int up, int left, int down) {
		neighbors[0] = right;
		neighbors[1] = up;
		neighbors[2] = left;
		neighbors[3] = down;
	}

	public void setTileNeighbors(Tile[] tiles) {
		tileNeighbors = tiles;
	}

	public void setOwner(Player newOwner) {
		//if not an unoccupied tile
		//if (owner != null && playerID != -1) {
			//remove tile from loser ownedTile AList
		//	owner.dropTile(this);
		//}
		//add tile to winner ownedTile AList
		if (newOwner.playerType != -1) {
			newOwner.addTile (this);
			owner = newOwner;
		}

		playerID = newOwner.playerID;

		renderer.material.color = newOwner.playerColor;
		//ph.checkWin();
	}

	public int getNeighborID(int i) {
		return neighbors[i];
	}

	public Tile[] getNeighborTiles() {
		return tileNeighbors;
	}

	public void incArmy(int i = 1) {
		forces += i;
	}

	public bool decArmy(int i = 1) {
		forces -= i;
		return true;
	}


}
