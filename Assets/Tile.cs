using UnityEngine;
using System.Collections;


public class Tile : MonoBehaviour {
	public PhaseHandler ph;
	public GameController go;

	private int forces = 0;
	//deprecated...
	private int player = 0;
	public Player owner;
	//GameObject owner;

	//[0:3] - > right, up, left, down
	int[] neighbors = new int[4]; //this must be private, otherwise unity loses it's shit.
	Tile[] tileNeighbors = new Tile[4];
	//1-54
	public int tileID = 0;
	public int face = 0;

	// Use this for initialization
	void Start () {
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	// Update is called once per frame
	void Update () {
		//renderer.material.color = owner.playerColor;
	}

	void OnMouseOver(){
		if (tileID != 0) {
			renderer.material.color = Color.green;
			renderNeighbors(getNeighborTiles ());
			renderer.material.SetInt (14, 300);
			for (int i = 0; i < 4; i++) {
				Debug.Log(tileID + ": " + neighbors[i]);
			}
		}
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
	
	public virtual int getPlayer(){
		return player;
	} 
	

	void OnMouseExit(){
		if (tileID != 0) {
			renderer.material.color = owner.playerColor;
			for ( int i =0; i < tileNeighbors.Length; i++){
				tileNeighbors[i].renderer.material.color = tileNeighbors[i].owner.playerColor;
			}
			//TODO: Revert changes back to original colors.
		}
	}
		
	void OnMouseDown(){

		forces++;
		Debug.Log(forces);
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
		owner = newOwner;
		renderer.material.color = newOwner.playerColor;
	}

	public int getNeighborID(int i) {
		return neighbors[i];
	}

	public Tile[] getNeighborTiles() {
		return tileNeighbors;
	}


}
