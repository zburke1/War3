using UnityEngine;
using System.Collections;

//AKA TILE
public class Tile : MonoBehaviour {
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
		//owner = GameObject.Instantiate (Player);
		//owner = new Player (); //initialize id = 0, playerColor = grey, playerType = -1 (for nonplayer entity type)
	}
	
	// Update is called once per frame
	void Update () {
		//renderer.material.color = owner.playerColor;
	}

	void OnMouseOver(){
		if (tileID != 0) {
			renderer.material.color = Color.green;
			renderer.material.SetInt (14, 300);
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

	public void setTileNeighbors(int right, int up, int left, int down) {
		neighbors[0] = right;
		neighbors[1] = up;
		neighbors[2] = left;
		neighbors[3] = down;
	}

	public void setOwner(Player newOwner) {
		owner = newOwner;
		renderer.material.color = newOwner.playerColor;
	}


}
