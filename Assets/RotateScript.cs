using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour{
	public GameController m_gameController;
	private GameObject[,] rotateBoard = new GameObject[7,10];
	private GameObject[,] rotateFace = new GameObject[7,10];
	private bool init;
	
	void Start(){
		m_gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	void Update(){
		//This code is checking to see if the board has been initialized in the GameController script
		if(m_gameController.getSingleFace(1,1)!=null&& !init){
			Debug.Log("Initialize Logic.......");
			initializeRotate();
			init = true;
		}
		
		if(Input.GetKey(KeyCode.Space)){
			//Using for testing
			m_gameController.pushBoard(rotateFace);
			}
		}
	
		private void initializeRotate(){
			rotateBoard = m_gameController.getBoard();
			Debug.Log("Rotate Board Logic Created");
			rotateFace = m_gameController.getFaceArray();
			Debug.Log("Rotate Face Logic Created");
			}
		}
			
