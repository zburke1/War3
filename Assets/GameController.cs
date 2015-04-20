﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject Cube;
	private GameObject clone;
	private int count = 0;
	private int testCount = 0;
	private int blockNum=1;
	public int currentPlayer = 0;
	public GameObject[,] board;
	public GameObject[,] faces;
	private Object temp;
	private Transform tempFace;
	public RotateScript m_RotateScript;
	public PhaseHandler m_PhaseHandler;
	public Player[] players;
	public int numPlayers;
	public Player winner;
	public bool gameOver;
	List<int> centerTiles = new List<int> {5,14,41,32,23,50};

	//*****************************************************************************
	//Hardcoded BlockNumbers to go with Unity Instantiated Cube. DO NOT EDIT!
	private int [] fA = {19, 22, 25, 10, 13, 16,1, 4, 7};
	private int [] fB = {21, 20, 19,12,11,10,3,2,1};
	private int [] fC = {27,24,21,18,15,12,9,6,3};
	private int [] fD = {25,26,27,16,17,18,7,8,9};
	private int [] fE = {21,24,27,20,23,26,19,22,25};
	private int [] fF = {1,4,7,2,5,8,3,6,9};
	////Hardcoded BlockNumbers to go with Unity Instantiated Cube. DO NOT EDIT!
	//*****************************************************************************




	// Use this for initialization
	void Start () {

			 board = new GameObject[7,10];
			 faces = new GameObject[7,10];
			 instantiateVirtualBoard();
			 instantiateLogicBoard(fA,fB,fC,fD,fE,fF);
			 m_RotateScript = GameObject.FindObjectOfType(typeof(RotateScript)) as RotateScript;
			m_PhaseHandler = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
			 m_RotateScript.initializeRotate();
			 winner = new Player();
			 //Debug.Log("SEL_NUMPLAYERS" + Sel_numPlayers);
			 //GameObject sideX = getSide(board[1,1],1);
			 //Destroy(getFace(1,1));

		//todo: need to get the menu items (num players, difficulty, etc) from the menu

		numPlayers = 2; //getNumPlayers();
		spawnPlayers (numPlayers);
		AgentUtil.setGameController ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace)) {  
			Application.LoadLevel (0);  
		}  
		//faces = m_RotateScript.Rotate(true,faces,board,1);

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
	
	private void instantiateLogicBoard(int[] faceA,int[] faceB,int[] faceC,int[] faceD,int[] faceE,int[] faceF){
		//*****************************************************************************
		//Hardcoded BlockNumbers to go with Unity Instantiated Cube. DO NOT EDIT!
		blockNum=-2;
		
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
		int tileID = 1;

		for(int i=1;i<7;i++){
			for(int j=1;j<10;j++){
				faces[i,j] = getSide(board[i,j],i);
				//set tileIDs.
				Tile tile = faces[i,j].gameObject.GetComponent<Tile>();
				tile.setID(tileID);
				tile.setFace(i);
				tile.setOwner(new Player());

				int right, up, left, down;	
				//happy hacking
				right = 0;	
				left = 0;
				up = 0;
				down = 0;
				
				//do left/rights
				if (i < 5) {
					//tile is in the middle column.
					if (tileID % 3 == 2) {
						left = tileID - 1;
						right = tileID + 1;
							
					//tile is in right column
					} else if (tileID % 3 == 0) { 
						left = tileID - 1;
						//the right of the first face is different
						if (i == 1) {
							right = tileID + 25;
						} else {
							right = tileID - 11;
						}
					//tile is left column
					} else { 
						right = tileID + 1;
						//the left of the 4th face is different
						if (i == 4) {
							left = tileID - 25;
						} else {
							left = tileID + 11;
						}
					}
				}

				//set ups/downs
				switch (i) {
					case 1:
						//top row
						if (j < 4) {
							up = tileID + 42;
							down = tileID + 3;
						//bottom row
						} else if (j > 6) {
							up = tileID - 3;
							down = tileID + 39;
						} else {
							up = tileID - 3;
							down = tileID + 3;
						}
					break;

					case 2:
						//top row
						if (j < 4) {
							up = tileID + 27 + (j-1)*3;
							down = tileID + 3;
						//bottom row
						} else if (j > 6) {
							up = tileID - 3;
							down = tileID + 36 - (j-7)*3;
						} else {
							up = tileID - 3;
							down = tileID + 3;
						}
						break;
					case 3:
						if (j < 4) {
							up = tileID + 20 - (j - 1)*2;
							down = tileID + 3;
						} else if (j > 6) {
							up = tileID - 3;
							down = tileID + 29 - (j-2)*2;
						} else {
							up = tileID - 3;
							down = tileID + 3;
						}
						break;
					case 4:
						if (j < 4) {
							up = tileID + 17 - (j - 1)*4;
							down = tileID + 3;
						} else if (j > 6) {
							up = tileID -3;
							down = tileID + 14 + (j - 7)*2;
						} else {
							up = tileID - 3;
							down = tileID + 3;
						}
						break;
				}

				tile.setNeighbors(right, up, left, down);

				tileID++;

				//tileID can be thought of as [1:9] + (faceID - 1)*9 for faces [1:6]
			}
		}

		hardcodeTop();
		hardcodeBottom();
		setAllTileNeighbors ();
		
	}

	public static GameObject getSide(GameObject A,int i){
		//GETS ARRAY OF SIDES. T[1] = Side A, T[2] = Side B, etc...
		//Manipulate Side using T[1].gameObject (i.e. Destroy(T[1].gameObject) will destroy Side A of this block)
		Component [] T = A.GetComponentsInChildren(typeof(Transform));
		//Debug.Log(T[i]);
		return T[i].gameObject;
	}
	
		//*****************************************************************************
		//Get and Set functions for RotateScript START
	public virtual GameObject getSingleFace(int i, int j){
		GameObject tempFace = faces[i,j];
		return tempFace;
	}

	public virtual int getTroops(int x,int y){
		GameObject A = faces [x, y].gameObject;
		Tile B = A.GetComponent<Tile> ();


		Tile Hover = faces[x,y].gameObject.GetComponent<Tile>();

		//Debug.Log(Hover.getForces());
		return Hover.getForces();
	}

	public virtual int getOwner(int x,int y){
		Tile Hover = faces[x,y].gameObject.GetComponent<Tile>();
		return Hover.getPlayerID();
	}

	
	public virtual GameObject getCube(int i, int j){
		GameObject tempBoard = board[i,j];
		return tempBoard;
	}
	
	public virtual GameObject[,] getBoard(){
		return board;
	}
	
	public virtual GameObject[,] getFaceArray(){
		return faces;
	}

	public virtual void nextTurnUpdate(){
		if (currentPlayer == numPlayers-1)
			currentPlayer = 0;
		else 
			currentPlayer++;
	}

	public void pushFace(GameObject[,] temp){
		faces = temp;	
	}
	
	public void pushBoard(GameObject[,] temp){
		board = temp;
		Debug.Log(board[1,1] + "TEST");
	}
	//Get and Set functions for RotateScript END
	//*****************************************************************************


	public void hardcodeTop() {
		for (int i = 1; i < 10; i ++) {
			Tile tile = faces[5,i].gameObject.GetComponent<Tile>();
			int tileID = tile.tileID;

			int right = tileID + 1;
			int up = tileID - 3;
			int left = tileID - 1;
			int down = tileID + 3;

			switch (tileID) {
				case 37: 
					up = 21;
					left = 10;
					break;
				case 38:
					up = 20;
					break;
				case 39:
					up = 19;
					right = 30;
					break;
				case 40:
					left = 11;
					break;
				case 42: 
					right = 29;
					break;
				case 43:
					left = 12;
					down = 1;
					break;
				case 44:
					down = 2;
					break;
				case 45:
					down = 3;
					right = 28;
					break;
				default://centertile
					break;
			}
			tile.setNeighbors(right, up, left, down);
		}
	}

	public void hardcodeBottom() {
		for (int i = 1; i < 10; i ++) {
			Tile tile = faces[6,i].gameObject.GetComponent<Tile>();
			int tileID = tile.tileID;

			int right = tileID + 1;
			int up = tileID - 3;
			int left = tileID - 1;
			int down = tileID + 3;

			switch (tileID) {
				case 42: 
					up = 7;
					left = 18;
					break;
				case 47:
					up = 8;
					break;
				case 48:
					up = 9;
					right = 34;
					break;
				case 49:
					left = 17;
					break;
				case 51: 
					right = 35;
					break;
				case 52:
					left = 16;
					down = 27;
					break;
				case 53:
					down = 26;
					break;
				case 54:
					down = 25;
					right = 36;
					break;
				default://centertile
					break;
			}
			tile.setNeighbors(right, up, left, down);
		}
	}

	public void spawnPlayers(int numPlayers) {
		//programmatic spawn
		//add player to the player global
		players = new Player[numPlayers];
		//only spawns humans at the moment, deal with it
		for (int i = 0; i < numPlayers; i++) {
			Player newPlayer;
			//Player newPlayer = new Player (i, 0, i);
			if (i == 0) {
				newPlayer = new Player (i+1, 0, i);
			} else {
				newPlayer = new Angry(i+1,1,i);
			}
			players[i] = newPlayer;
			for (int j = 0; j<6/numPlayers; j++) {
				Tile tile = faces [j + 1 + i * 6/numPlayers, 5].gameObject.GetComponent<Tile> ();
				tile.setOwner (newPlayer);
				tile.setForces (3);
			}

		}
		Debug.Log ("Player0: " + players[0].playerID);
		Debug.Log ("Player1: " + players[1].playerID);

	}


	//utilities!
	//I'm not proud of this.
	public void setTileNeighbors(Tile tile) {
		Tile[] neighbors = new Tile[4];
		for (int i = 0; i < 4; i++) {
			int neighborID = tile.getNeighborID(i);
			Tile neighbor = findTileFromID(neighborID);
			if (neighbor != null) {
				neighbors[i] = neighbor;
			}
		}
		tile.setTileNeighbors (neighbors);
	}

	public void setAllTileNeighbors() {
		for (int i = 1; i < 7; i++) {
			for (int j = 1; j <10; j++) {
				Tile tile = faces[i,j].gameObject.GetComponent<Tile>();
				setTileNeighbors(tile);
			}
		}
	}

	public Tile findTileFromID(int tileID) {
		for (int i = 1; i < 7; i++) {
			for (int j = 1; j <10; j++) {
				Tile tile = faces[i,j].gameObject.GetComponent<Tile>();
				if (tile.tileID == tileID) {
					return tile;
				}
			}
		}
		return null;
	}

	public Tile findTileFromIndex(int i, int j) {
		return faces[i,j].gameObject.GetComponent<Tile>();
	}

	public void checkWin() {
		bool win = true;

		for (int i = 1; i < 7; i++) {
			Tile tile = faces[i,1].gameObject.GetComponent<Tile>();
			Player owner = tile.getOwner ();

			for (int j = 2; j < 10; j++) {
				Tile nextTile = faces[i,j].gameObject.GetComponent<Tile>();
				if (nextTile.getOwner ().playerID != owner.playerID) {
					win = false;
				}
			}

			if (win) {
				Debug.Log ("A Winner has been absolutely detected!");
				m_PhaseHandler.currentPhase = Phase.victoryPhase;
				winner = owner;
				m_PhaseHandler.nextPhase();
				gameOver = true;
				break;
			}

		}

		return;
	}
	
}
