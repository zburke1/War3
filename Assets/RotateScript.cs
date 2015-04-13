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
				tempFace[i,j] = m_gameController.faces[i,j];
			}
		}
	
		switch(side){
		case 1:
			if(clockwise){
				RotateDirectCW(1);
				
				//transfer(2, 3, 6, 1);
				//transfer(2, 6, 6, 2);
				//transfer(2, 9, 6, 3);
				
				//transfer(5, 7, 2, 9);
				//transfer(5, 8, 2, 6);
				//transfer(5, 9, 2, 3);
				
				//transfer(4, 1, 5, 7);
				//transfer(4, 4, 5, 8);
				//transfer(4, 7, 5, 9);
				
				//transfer(6, 1, 4, 7);
				//transfer(6, 2, 4, 4);
				//transfer(6, 3, 4, 1);
				
			} else {
				RotateDirectCCW(1);
				
				transfer(2, 3, 5, 9);
				transfer(2, 6, 5, 8);
				transfer(2, 9, 5, 7);
				
				transfer(5, 7, 4, 1);
				transfer(5, 8, 4, 4);
				transfer(5, 9, 4, 7);
				
				transfer(4, 1, 6, 3);
				transfer(4, 4, 6, 2);
				transfer(4, 7, 6, 1);
				
				transfer(6, 1, 2, 3);
				transfer(6, 2, 2, 6);
				transfer(6, 3, 2, 9);
			}
			break;
		case 2:
			if(clockwise){
				RotateDirectCW(2);
				
				transfer(1, 1, 5, 1);
				transfer(1, 4, 5, 4);
				transfer(1, 7, 5, 7);
				
				transfer(6, 1, 1, 1);
				transfer(6, 4, 1, 4);
				transfer(6, 7, 1, 7);
				
				transfer(3, 9, 6, 1);
				transfer(3, 6, 6, 4);
				transfer(3, 3, 6, 7);
				
				transfer(5, 1, 3, 9);
				transfer(5, 4, 3, 6);
				transfer(5, 7, 3, 3);
				
			} else {
				RotateDirectCCW(2);
				
				transfer(5, 1, 1, 1);
				transfer(5, 4, 1, 4);
				transfer(5, 7, 1, 7);
				
				transfer(1, 1, 6, 1);
				transfer(1, 4, 6, 4);
				transfer(1, 7, 6, 7);
				
				transfer(6, 1, 3, 9);
				transfer(6, 4, 3, 6);
				transfer(6, 7, 3, 3);
				
				transfer(3, 9, 5, 1);
				transfer(3, 6, 5, 4);
				transfer(3, 3, 5, 7);
			}
			break;
		}

		for(int i = 1;i<7;i++){
			for(int j = 1; j <10; j++){
				Tile destination = m_gameController.faces[i,j].gameObject.GetComponent<Tile>();
				Tile source = tempFace[i,j].gameObject.GetComponent<Tile>();
				
				destination.setForces (source.getForces ());
				destination.setOwner (source.getOwner ());

				//m_gameController.faces[i,j] = tempFace[i,j]; 
			}
		}

		return;
	}


	public void RotateDirectCCW(int side) {
		transfer(side, 1, side, 3);
		transfer(side, 2, side, 6);
		transfer(side, 3, side, 9);
		transfer(side, 4, side, 2);
		transfer(side, 5, side, 5);
		transfer(side, 6, side, 8);
		transfer(side, 7, side, 1);
		transfer(side, 8, side, 4);
		transfer(side, 9, side, 7);
	}
	
	public void RotateDirectCW(int side) {
		transfer(side, 1, side, 7);
		//transfer(side, 2, side, 4);
		transfer(side, 3, side, 1);
		//transfer(side, 4, side, 8);
		//transfer(side, 5, side, 5);
		//transfer(side, 6, side, 2);
		transfer(side, 7, side, 9);
		//transfer(side, 8, side, 6);
		transfer(side, 9, side, 3);
	}

	public void transfer(int s1, int s2, int d1, int d2) {
		Tile source = m_gameController.faces[d1,d2].gameObject.GetComponent<Tile>();
		Tile destination = tempFace[s1,s2].gameObject.GetComponent<Tile>();

		destination.setForces (source.getForces ());
		destination.setOwner (source.getOwner ());
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
			
