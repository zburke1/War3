using UnityEngine;
using System.Collections;

public class HoverEffect : MonoBehaviour {
	private int forces = 0;
	private int player = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		renderer.material.color = Color.green;
		renderer.material.SetInt (14, 300);
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
	
}
