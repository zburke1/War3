using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour{
	public GameController m_gameController;
	private GameObject[,] rotateBoard = new GameObject[7,10];
	private GameObject[,] rotateFace = new GameObject[7,10];
	private Vector3[,] BoardLocation = new Vector3[7,10];
	public int u = 0;
	private bool init;
	private GameObject[,] tempBoard = new GameObject[7,10];
	private GameObject[,] tempFace = new GameObject[7,10];

	int[,] armies = new int[7,10];
	Player[,] owners = new Player[7,10];
	
	void Start(){
		m_gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	void Update(){
		//This code is checking to see if the board has been initialized in the GameController script
		if(m_gameController.getSingleFace(1,1)!=null&& !init){
			Debug.Log("Initialize Logic.......");
			init = true;
		}

		
	}
	public void initializeRotate(){
		rotateBoard = m_gameController.getBoard();
		Debug.Log("Rotate Board Logic Created");
		rotateFace = m_gameController.getFaceArray();
		Debug.Log("Rotate Face Logic Created");
		initializeLocations();
	}
			
	public void deleteBoard(){
		Destroy(rotateBoard[1,3]);
	}
			
	public void Rotate(bool clockwise, int side) {


		for(int i = 1; i < 7; i++){
			for(int j = 1; j < 10; j++){
				Tile faceTile = m_gameController.faces[i,j].gameObject.GetComponent<Tile>();
				armies[i,j] = faceTile.getForces();
				owners[i,j] = faceTile.getOwner();
				//tempFace[i,j] = m_gameController.faces[i,j];
			}
		}
	
		switch(side){
		case 1:
			if(clockwise){
				RotateDirectCW(1);
				
				transfer(2, 3, 6, 1, false);
				transfer(2, 6, 6, 2, false);
				transfer(2, 9, 6, 3, false);
				
				transfer(5, 7, 2, 9, false);
				transfer(5, 8, 2, 6, false);
				transfer(5, 9, 2, 3, false);
				
				transfer(4, 1, 5, 7, false);
				transfer(4, 4, 5, 8, false);
				transfer(4, 7, 5, 9, false);
				
				transfer(6, 1, 4, 7, false);
				transfer(6, 2, 4, 4, false);
				transfer(6, 3, 4, 1, false);
				
			} else {
				RotateDirectCCW(1);
				
				transfer(2, 3, 6, 1, true);
				transfer(2, 6, 6, 2, true);
				transfer(2, 9, 6, 3, true);
				
				transfer(5, 7, 2, 9, true);
				transfer(5, 8, 2, 6, true);
				transfer(5, 9, 2, 3, true);
				
				transfer(4, 1, 5, 7, true);
				transfer(4, 4, 5, 8, true);
				transfer(4, 7, 5, 9, true);
				
				transfer(6, 1, 4, 7, true);
				transfer(6, 2, 4, 4, true);
				transfer(6, 3, 4, 1, true);
			}
			break;
		case 2:
			if(clockwise){
				RotateDirectCW(2);
				
				transfer(1, 1, 5, 1, false);
				transfer(1, 4, 5, 4, false);
				transfer(1, 7, 5, 7, false);
				
				transfer(6, 1, 1, 1, false);
				transfer(6, 4, 1, 4, false);
				transfer(6, 7, 1, 7, false);
				
				transfer(3, 9, 6, 1, false);
				transfer(3, 6, 6, 4, false);
				transfer(3, 3, 6, 7, false);
				
				transfer(5, 1, 3, 9, false);
				transfer(5, 4, 3, 6, false);
				transfer(5, 7, 3, 3, false);
				
			} else {
				RotateDirectCCW(2);
				
				transfer(1, 1, 5, 1, true);
				transfer(1, 4, 5, 4, true);
				transfer(1, 7, 5, 7, true);
				
				transfer(6, 1, 1, 1, true);
				transfer(6, 4, 1, 4, true);
				transfer(6, 7, 1, 7, true);
				
				transfer(3, 9, 6, 1, true);
				transfer(3, 6, 6, 4, true);
				transfer(3, 3, 6, 7, true);
				
				transfer(5, 1, 3, 9, true);
				transfer(5, 4, 3, 6, true);
				transfer(5, 7, 3, 3, true);
			}
			break;
		case 3:
			if(clockwise){
				RotateDirectCW(3);
				
				transfer(2, 7, 5, 1, false);
				transfer(2, 4, 5, 2, false);
				transfer(2, 1, 5, 3, false);
				
				transfer(1, 3, 2, 7, false);
				transfer(1, 2, 2, 4, false);
				transfer(1, 1, 2, 1, false);
				
				transfer(4, 3, 1, 3, false);
				transfer(4, 6, 1, 2, false);
				transfer(4, 9, 1, 1, false);
				
				transfer(5, 1, 4, 3, false);
				transfer(5, 2, 4, 6, false);
				transfer(5, 3, 4, 9, false);
				
			} else {
				RotateDirectCCW(3);
				
				transfer(2, 7, 5, 1, true);
				transfer(2, 4, 5, 2, true);
				transfer(2, 1, 5, 3, true);
				
				transfer(1, 3, 2, 7, true);
				transfer(1, 2, 2, 4, true);
				transfer(1, 1, 2, 1, true);
				
				transfer(4, 3, 1, 3, true);
				transfer(4, 6, 1, 2, true);
				transfer(4, 9, 1, 1, true);
				
				transfer(5, 1, 4, 3, true);
				transfer(5, 2, 4, 6, true);
				transfer(5, 3, 4, 9, true);
			}
			break;
		case 4:
			if(clockwise){
				RotateDirectCW(4);
				
				transfer(3, 7, 5, 3, false);
				transfer(3, 4, 5, 6, false);
				transfer(3, 1, 5, 9, false);
				
				transfer(6, 3, 3, 7, false);
				transfer(6, 6, 3, 4, false);
				transfer(6, 9, 3, 1, false);
				
				transfer(1, 3, 6, 3, false);
				transfer(1, 6, 6, 6, false);
				transfer(1, 9, 6, 9, false);
				
				transfer(5, 3, 1, 3, false);
				transfer(5, 6, 1, 6, false);
				transfer(5, 9, 1, 9, false);
				
			} else {
				RotateDirectCCW(4);
				
				transfer(3, 7, 5, 3, true);
				transfer(3, 4, 5, 6, true);
				transfer(3, 1, 5, 9, true);
				
				transfer(6, 3, 3, 7, true);
				transfer(6, 6, 3, 4, true);
				transfer(6, 9, 3, 1, true);
				
				transfer(1, 3, 6, 3, true);
				transfer(1, 6, 6, 6, true);
				transfer(1, 9, 6, 9, true);
				
				transfer(5, 3, 1, 3, true);
				transfer(5, 6, 1, 6, true);
				transfer(5, 9, 1, 9, true);
			}
			break;
		case 5:
			if(clockwise){
				RotateDirectCW(5);
				
				transfer(4, 1, 3, 1, false);
				transfer(4, 2, 3, 2, false);
				transfer(4, 3, 3, 3, false);
				
				transfer(1, 1, 4, 1, false);
				transfer(1, 2, 4, 2, false);
				transfer(1, 3, 4, 3, false);
				
				transfer(2, 1, 1, 1, false);
				transfer(2, 2, 1, 2, false);
				transfer(2, 3, 1, 3, false);
				
				transfer(3, 1, 2, 1, false);
				transfer(3, 2, 2, 2, false);
				transfer(3, 3, 2, 3, false);
				
			} else {
				RotateDirectCCW(5);
				
				transfer(4, 1, 3, 1, true);
				transfer(4, 2, 3, 2, true);
				transfer(4, 3, 3, 3, true);
				
				transfer(1, 1, 4, 1, true);
				transfer(1, 2, 4, 2, true);
				transfer(1, 3, 4, 3, true);
				
				transfer(2, 1, 1, 1, true);
				transfer(2, 2, 1, 2, true);
				transfer(2, 3, 1, 3, true);
				
				transfer(3, 1, 2, 1, true);
				transfer(3, 2, 2, 2, true);
				transfer(3, 3, 2, 3, true);
			}
			break;
		case 6:
			if(clockwise){
				RotateDirectCW(6);
				
				transfer(4, 7, 1, 7, false);
				transfer(4, 8, 1, 8, false);
				transfer(4, 9, 1, 9, false);
				
				transfer(3, 7, 4, 7, false);
				transfer(3, 8, 4, 8, false);
				transfer(3, 9, 4, 9, false);
				
				transfer(2, 7, 3, 7, false);
				transfer(2, 8, 3, 8, false);
				transfer(2, 9, 3, 9, false);
				
				transfer(1, 7, 2, 7, false);
				transfer(1, 8, 2, 8, false);
				transfer(1, 9, 2, 9, false);
				
			} else {
				RotateDirectCCW(6);
				
				transfer(4, 7, 1, 7, true);
				transfer(4, 8, 1, 8, true);
				transfer(4, 9, 1, 9, true);
				
				transfer(3, 7, 4, 7, true);
				transfer(3, 8, 4, 8, true);
				transfer(3, 9, 4, 9, true);
				
				transfer(2, 7, 3, 7, true);
				transfer(2, 8, 3, 8, true);
				transfer(2, 9, 3, 9, true);
				
				transfer(1, 7, 2, 7, true);
				transfer(1, 8, 2, 8, true);
				transfer(1, 9, 2, 9, true);
			}
			break;
		}
		
		for(int i = 1;i<7;i++){
			for(int j = 1; j <10; j++){
				Tile destination = m_gameController.faces[i,j].gameObject.GetComponent<Tile>();
				destination.setForces (armies[i,j]);
				destination.setOwner (owners[i,j]);
			}
		}
		
		return;
	}
	
	
	public void RotateDirectCCW(int side) {
		transfer(side, 1, side, 3, false);
		transfer(side, 2, side, 6, false);
		transfer(side, 3, side, 9, false);
		transfer(side, 4, side, 2, false);
		transfer(side, 5, side, 5, false);
		transfer(side, 6, side, 8, false);
		transfer(side, 7, side, 1, false);
		transfer(side, 8, side, 4, false);
		transfer(side, 9, side, 7, false);
	}
	
	public void RotateDirectCW(int side) {
		transfer(side, 1, side, 7, false);
		transfer(side, 2, side, 4, false);
		transfer(side, 3, side, 1, false);
		transfer(side, 4, side, 8, false);
		transfer(side, 5, side, 5, false);
		transfer(side, 6, side, 2, false);
		transfer(side, 7, side, 9, false);
		transfer(side, 8, side, 6, false);
		transfer(side, 9, side, 3, false);
	}
	
	public void transfer(int s1, int s2, int d1, int d2, bool reverse) {
		if (reverse) {
			Tile source = m_gameController.faces[s1,s2].gameObject.GetComponent<Tile>();
			armies[d1,d2] = source.getForces ();
			owners[d1,d2] = source.getOwner ();
		} else {
			Tile source = m_gameController.faces[d1,d2].gameObject.GetComponent<Tile>();
			armies[s1,s2] = source.getForces ();
			owners[s1,s2] = source.getOwner ();
		}
		return;
	}

	public void initializeLocations(){
		for(int i = 1;i<7;i++){
			for(int j = 1;j<10;j++){
				BoardLocation[i,j] = rotateBoard[i,j].transform.position;
			}
		}
			
	}			
			
			
}
			
