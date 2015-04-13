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
		//owner = new Player (); //initialize id = 0, playerColor = grey, playerType = -1 (for nonplayer entity type)
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
			if (Input.GetMouseButton (1) && mousereleased && go.players[go.currentPlayer].rotateCards > 0) {
				if (centerTiles.Contains (tileID)) {
					switch (tileID) {
					case 5:
						m_RotateScript.Rotate (false, 1);
						break;
					case 14:
						m_RotateScript.Rotate (false, 2);
						break;
					case 23:
						m_RotateScript.Rotate (false, 3);
						break;
					case 32:
						m_RotateScript.Rotate (false, 4);
						break;
					case 41:
						m_RotateScript.Rotate (false, 5);
						break;
					case 50:
						m_RotateScript.Rotate (false, 6);
						break;
					}
					go.players[go.currentPlayer].rotateCards--;	
				//	m_PhaseHandler.rotateToggle.isOn = false;
				}
				mousereleased=false;
			}
			if(Input.GetMouseButtonUp(1))
				mousereleased=true;
			break;

				break;
			case Phase.battlePhase:
			if(!tileFocus){
				if (tileID != 0) {
					renderer.material.color = Color.green;
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
			/*if (!tileFocus) {
				if (tileID != 0) {
					renderer.material.color = owner.playerColor;

				}

			}*/
			for (int i = 0; i < 4; i++) {
				if (neighbors[i] == ph.focusedTile) {
					renderer.material.color = Color.gray;
				}
			}
			break;
		/*
		if (tileID != 0) {
			if (playerID != -1) {
				renderer.material.color = owner.playerColor;
				for ( int i =0; i < tileNeighbors.Length; i++){
					tileNeighbors[i].renderer.material.color = tileNeighbors[i].owner.playerColor;
				}
			} else {
				renderer.material.color = Color.white;
			}
			

		}
		*/
		}
	}
		
	void OnMouseDown(){


		switch (ph.currentPhase) {

		case Phase.spawnPhase:
			if (tileID != 0 && go.players[go.currentPlayer].troopSpawnCount > 0) {
				go.players[go.currentPlayer].troopSpawnCount --;
				setOwner( go.players[go.currentPlayer]);
				forces++;
			}
			break;
		case Phase.rotatePhase:
			if (Input.GetMouseButton (0) && go.players[go.currentPlayer].rotateCards > 0) {

				if (centerTiles.Contains (tileID)) {
					switch (tileID) {
					case 5:
						m_RotateScript.Rotate (true, 1);
						break;
					case 14:
						m_RotateScript.Rotate (true, 2);
						break;
					case 23:
						m_RotateScript.Rotate (true, 3);
						break;
					case 32:
						m_RotateScript.Rotate (true, 4);
						break;
					case 41:
						m_RotateScript.Rotate (true, 5);
						break;
					case 50:
						m_RotateScript.Rotate (true, 6);
						break;
					}
					go.players[go.currentPlayer].rotateCards--;	
					//m_PhaseHandler.rotateToggle.isOn = false;

				}
			}
			break;

		case Phase.battlePhase:
			renderer.material.color = Color.green;/*
			if(ph.focusedTile == this){
				ph.focusedTile = this;
				tileFocus = true;}
			else{
				ph.targetTile = this;
				ph.focusedTile.owner.attack(ph.focusedTile,ph.targetTile);

			}
	*/
			if (ph.focusedTile != this) {
				bool isAttack = false;
				for (int i = 0; i < 4; i++) {
					if (tileNeighbors[i] == ph.focusedTile) {
						Debug.Log ("player attacking");
						owner.attack(ph.focusedTile, tileNeighbors[i]);
						isAttack = true;
					}
				}
				if(!isAttack) {
					ph.focusedTile = this;
					battleColors (true);
				}
			
			}



			break;
		}
	}

	public void battleColors(bool on) {
		for (int i = 0; i < 4; i++) {
			tileNeighbors[i].setBattleColor(on);
		}
	}

	public void setBattleColor(bool on) {
		if (on) {
			renderer.material.color = Color.gray;
		} else {
			renderer.material.color = owner.playerColor;
		}
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
		playerID = newOwner.playerID;
		owner = newOwner;
		renderer.material.color = newOwner.playerColor;
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
		if (forces == 1) {
			return false;
		} else {
			forces -= 1;
		}
		return true;
	}

}
