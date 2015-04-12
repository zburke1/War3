using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour{
	public GameController m_gameController;
	private GameObject[,] rotateBoard = new GameObject[7,10];
	private GameObject[,] rotateFace = new GameObject[7,10];
	private Vector3[,] BoardLocation = new Vector3[7,10];
	public int u = 0;
	private bool init;
	
	void Start(){
		m_gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	void Update(){
		//This code is checking to see if the board has been initialized in the GameController script
		if(m_gameController.getSingleFace(1,1)!=null&& !init){
			Debug.Log("Initialize Logic.......");
			init = true;
		}
		
		if(Input.GetKey(KeyCode.Space)){
				//m_RotateScript.Rotate(true,board,1);
				//Destroy(board[1,testCount+1]);
				u++;
			}
		
		if(u == 1){
			//Rotate(true,rotateBoard,1);
			rotateBoard[2, 3].transform.position = Vector3.Lerp(rotateBoard[2, 3].transform.position,BoardLocation[5, 9],0.1f);
			rotateBoard[2, 6].transform.position = Vector3.Lerp(rotateBoard[2, 6].transform.position,BoardLocation[5, 8],0.1f);
			rotateBoard[2, 9].transform.position = Vector3.Lerp(rotateBoard[2, 9].transform.position,BoardLocation[5, 7],0.1f);
		
			rotateBoard[5, 7].transform.position = Vector3.Lerp(rotateBoard[5, 7].transform.position,BoardLocation[4, 1],0.1f);
			rotateBoard[5, 8].transform.position = Vector3.Lerp(rotateBoard[5, 8].transform.position,BoardLocation[4, 4],0.1f);
			rotateBoard[5, 9].transform.position = Vector3.Lerp(rotateBoard[5, 9].transform.position,BoardLocation[4, 7],0.1f);
		
			rotateBoard[4, 1].transform.position = Vector3.Lerp(rotateBoard[4, 1].transform.position,BoardLocation[6, 3],0.1f);
			rotateBoard[4, 4].transform.position = Vector3.Lerp(rotateBoard[4, 4].transform.position,BoardLocation[6, 2],0.1f);
			rotateBoard[4, 7].transform.position = Vector3.Lerp(rotateBoard[4, 7].transform.position,BoardLocation[6, 1],0.1f);
			
			rotateBoard[6, 1].transform.position = Vector3.Lerp(rotateBoard[6, 1].transform.position,BoardLocation[2, 3],0.1f);
			rotateBoard[6, 2].transform.position = Vector3.Lerp(rotateBoard[6, 2].transform.position,BoardLocation[2, 6],0.1f);
			rotateBoard[6, 3].transform.position = Vector3.Lerp(rotateBoard[6, 3].transform.position,BoardLocation[2, 9],0.1f);
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
			
public void Rotate(bool clockwise, GameObject[,] faces, int side) {
	switch(side){
		case 1:
		if(clockwise){
			Debug.Log("TEST!");
		RotateDirectCW(faces,1);
		rotateBoard[2, 3] = faces[5, 9];
		rotateBoard[2, 6] = faces[5, 8];
		rotateBoard[2, 9] = faces[5, 7];
		
		rotateBoard[5, 7] = faces[4, 1];
		rotateBoard[5, 8] = faces[4, 4];
		rotateBoard[5, 9] = faces[4, 7];
		
		rotateBoard[4, 1] = faces[6, 3];
		rotateBoard[4, 4] = faces[6, 2];
		rotateBoard[4, 7] = faces[6, 1];
		
		rotateBoard[6, 1] = faces[2, 3];
		rotateBoard[6, 2] = faces[2, 6];
		rotateBoard[6, 3] = faces[2, 9];
	}
		break;
	}
			
		}
			public void RotateDirectCW(GameObject[,] faces, int side) {
				rotateBoard[side, 1] = faces[side, 3];
				rotateBoard[side, 2] = faces[side, 6];
				rotateBoard[side, 3] = faces[side, 9];
				rotateBoard[side, 4] = faces[side, 2];
				rotateBoard[side, 5] = faces[side, 5];
				rotateBoard[side, 6] = faces[side, 8];
				rotateBoard[side, 7] = faces[side, 1];
				rotateBoard[side, 8] = faces[side, 4];
				rotateBoard[side, 9] = faces[side, 7];
			}
 
			public void RotateDirectCCW(GameObject[,] faces, int side) {
				rotateBoard[side, 1] = faces[side, 7];
				rotateBoard[side, 2] = faces[side, 4];
				rotateBoard[side, 3] = faces[side, 1];
				rotateBoard[side, 4] = faces[side, 8];
				rotateBoard[side, 5] = faces[side, 5];
				rotateBoard[side, 6] = faces[side, 2];
				rotateBoard[side, 7] = faces[side, 9];
				rotateBoard[side, 8] = faces[side, 6];
				rotateBoard[side, 9] = faces[side, 3];
			}		
public void initializeLocations(){
	for(int i = 1;i<7;i++){
		for(int j = 1;j<10;j++){
			BoardLocation[i,j] = rotateBoard[i,j].transform.position;
		}
	}
		
}			
			
			
			
		}
			
