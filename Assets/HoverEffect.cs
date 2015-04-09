using UnityEngine;
using System.Collections;

//AKA TILE
public class HoverEffect : MonoBehaviour {
	private int forces = 0;
	private int player = 0;
	private int[] neighbors;

	//1-54
	public int tileID = 0;
	public int face = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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
	
	public virtual int getPlayer(){
		return player;
	} 
	

	void OnMouseExit(){
		renderer.material.color = Color.white;
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

	void setNeighbors(int right, int up, int left, int down) {
		neighbors[0] = right;
		neighbors[1] = up;
		neighbors[2] = left;
		neighbors[3] = down;
	}
	
}
