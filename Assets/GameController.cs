using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Cube;
	private GameObject clone;
	private int count = 0;
	private int blockNum=1;
	public GameObject[,] board;
	public GameObject[,] faces;
	private Object temp;
	private Transform tempFace;
	
	
	
	// Use this for initialization
	void Start () {
			 board = new GameObject[7,10];
			 faces = new GameObject[7,10];
			 instantiateVirtualBoard();
			 instantiateLogicBoard();
			 //GameObject sideX = getSide(board[1,1],1);
			 //Destroy(getFace(1,1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void instantiateVirtualBoard(){
	//Builds bottom,forwards,up
	for (float y = 0; y < 3; y++) {
		count = 0;
	        for (float x = 0; x < 3; x++) {
				  for(float z=0;z<3;z++){
					  
					 GameObject obj = Instantiate(Cube, new Vector3(x*1.2f, y*1.2f, z*1.2f), Quaternion.identity) as GameObject;
					 obj.name = "Block"+blockNum;
					 blockNum++;
				  }
				  count++;
			  }
		 }
	 }
	
	private void instantiateLogicBoard(){
		//*****************************************************************************
		//Hardcoded BlockNumbers to go with Unity Instantiated Cube. DO NOT EDIT!
		blockNum=-2;
		int [] faceA = {19, 22, 25, 10, 13, 16,1, 4, 7};
		int [] faceB = {21, 20, 19,12,11,10,3,2,1};
		int [] faceC = {27,24,21,18,15,12,9,6,3};
		int [] faceD = {25,26,27,16,17,18,7,8,9};
		int [] faceE = {21,24,27,20,23,26,19,22,25};
		int [] faceF = {1,4,7,2,5,8,3,6,9};
		////Hardcoded BlockNumbers to go with Unity Instantiated Cube. DO NOT EDIT!
		//*****************************************************************************
		
		//Instatiates board array using hardcoded faceX array's
		for(int i=1;i<10;i++)
		board[1,i]=GameObject.Find("Block"+faceA[i-1]);
		
		for(int i=1;i<10;i++)
		board[2,i]=GameObject.Find("Block"+faceB[i-1]);
		
		for(int i=1;i<10;i++)
		board[3,i]=GameObject.Find("Block"+faceC[i-1]);
		
		for(int i=1;i<10;i++)
		board[4,i]=GameObject.Find("Block"+faceD[i-1]);
		
		for(int i=1;i<10;i++)
		board[5,i]=GameObject.Find("Block"+faceE[i-1]);
		
		for(int i=1;i<10;i++)
		board[6,i]=GameObject.Find("Block"+faceF[i-1]);
		
		//Instantiate face array using board array;
		for(int i=1;i<7;i++){
			for(int j=1;j<10;j++){
				faces[i,j] = getSide(board[i,j],i);
			}
		}
		
	}
	
	public static GameObject getSide(GameObject A,int i){
		//GETS ARRAY OF SIDES. T[1] = Side A, T[2] = Side B, etc...
		//Manipulate Side using T[1].gameObject (i.e. Destroy(T[1].gameObject) will destroy Side A of this block)
		Component [] T = A.GetComponentsInChildren(typeof(Transform));
		Debug.Log(T[i]);
		return T[i].gameObject;
	}
	
	public virtual GameObject getFace(int i, int j){
		GameObject tempFace = faces[i,j];
		return tempFace;
	}
	
	public virtual GameObject getBoard(int i, int j){
		GameObject tempBoard = board[i,j];
		return tempBoard;
	}

	
	private void deleteTest(){
		// Destroy(T[1].gameObject);
// 		for(int i=1;i<10;i++){
// 			 //Destroy(board[5,i]);
// 		}

	}
}
