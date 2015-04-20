using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour{
	public GameController m_gameController;
	public PhaseHandler ph;
	public GameObject[,] rotateBoard = new GameObject[7,10];
	private GameObject[,] rotateFace = new GameObject[7,10];
	private Vector3[,] BoardLocation = new Vector3[7,10];
	private Quaternion[,] BoardRotation = new Quaternion[7,10];
	private Vector3 RotateDirection = new Vector3(0,0,0);
	public int u = 0;
	private bool init;
	public CameraControll m_CameraControll;
	private GameObject[,] tempBoard = new GameObject[7,10];
	private GameObject[,] tempFace = new GameObject[7,10];
	private int logicRotate = 0;
	private int rotate = 0;
	private int sideRotate = 0;
	int[,] armies = new int[7,10];
	Player[,] owners = new Player[7,10];
	GameObject tileNumberGUI;
	
	void Start(){
		m_gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		m_CameraControll = GameObject.FindObjectOfType(typeof(CameraControll)) as CameraControll;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		tileNumberGUI = GameObject.Find ("Face_Num_GUI");

	}
	
	public void initializeRotate(){
		rotateBoard = m_gameController.getBoard();
		Debug.Log("Rotate Board Logic Created");
		rotateFace = m_gameController.getFaceArray();
		Debug.Log("Rotate Face Logic Created");
		initializeLocations();
	}
	
	public void PhysicalRotateSpecs(){
		sideRotate = m_CameraControll.GetSide();
		/*Debug.Log(m_CameraControll.GetSide());
		if(sideRotate==1){
			RotateDirection = Vector3.back;
		}
		if(sideRotate==2){
			RotateDirection = Vector3.down;
		}
		*/
		
	}
	
	void Update(){
		if (ph.currentPhase == Phase.rotatePhase && m_gameController.players [m_gameController.currentPlayer].playerType == 0 && m_gameController.players [m_gameController.currentPlayer].rotateCards > 0) {
			if (Input.GetKeyDown (KeyCode.E)) {
				StartCoroutine(turnOffText());
				//PhysicalRotateSpecs();
				sideRotate = m_CameraControll.GetSide ();
				Rotate (true, m_CameraControll.GetSide ());
				m_gameController.players [m_gameController.currentPlayer].rotateCards --;
				m_gameController.checkWin();
			}

		
			if (Input.GetKeyDown (KeyCode.Q)) {
				//PhysicalRotateSpecs();
				StartCoroutine(turnOffText());
				sideRotate = m_CameraControll.GetSide ();
				Rotate (false, m_CameraControll.GetSide ());
				m_gameController.players [m_gameController.currentPlayer].rotateCards --;
				m_gameController.checkWin();
			}
		}
		//This code is checking to see if the board has been initialized in the GameController script
		/*if(m_gameController.getSingleFace(1,1)!=null&& !init){
			Debug.Log("Initialize Logic.......");
			init = true;
		}*/
		//SIDE 1---------------------------------------------------------------------
		//Side1 Clockwise
		if(rotate == 1){
			if(rotateBoard[1,1].transform.eulerAngles.z ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.z >=270f ){
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.z !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.z <=270f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
			logicRotate = 1;
		}
	}
	//Side1 CounterClockwise
	if(rotate == 2){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.z ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.z <=90f ){
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.z !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.z >90f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
	}
}
		//SIDE 2---------------------------------------------------------------------
		//Side2 Clockwise
		if(rotate == 3){
			if(rotateBoard[sideRotate,1].transform.eulerAngles.x ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.x !=270f ){
				Debug.Log(rotateBoard[sideRotate,1].transform.eulerAngles.x);
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.x !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.x ==270f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
		}
	}
	//Side2 CounterClockwise
	if(rotate == 4){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.x ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.x !=90f ){
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.x !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.x ==00f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
		}
		}
		//SIDE 3---------------------------------------------------------------------
		//Side3 Clockwise
		if(rotate == 5){
			if(rotateBoard[sideRotate,1].transform.eulerAngles.z ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.z <=90f ){
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.forward,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.z !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.z >90f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
		}
	}
	//Side3 CounterClockwise
	if(rotate == 6){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.z ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.z >=270f ){
			Debug.Log(rotateBoard[sideRotate,1].transform.eulerAngles.z);
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.back,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.z !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.z <270f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
		}
		}
		//SIDE 4---------------------------------------------------------------------
		//Side4 Clockwise
		if(rotate == 7){
			if(rotateBoard[sideRotate,1].transform.eulerAngles.x ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.x !=90f ){
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.right,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.x !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.x ==90f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
		}
	}
	//Side4 CounterClockwise
	if(rotate == 8){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.x ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.x !=270f ){
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.left,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.x !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.x ==270f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
		}
		}
		//SIDE 5---------------------------------------------------------------------
		//Side5 Clockwise
		if(rotate == 9){
			if(rotateBoard[sideRotate,1].transform.eulerAngles.y ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.y <=90f ){
			Debug.Log(rotateBoard[sideRotate,1].transform.eulerAngles.y);
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.y !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.y >90f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
		}
	}
	//Side5 CounterClockwise
	if(rotate == 10){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.y ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.y >=270f ){
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.y !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.y <270f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
		}
		}
		//SIDE 6---------------------------------------------------------------------
		//Side6 Clockwise
		if(rotate == 11){
			if(rotateBoard[sideRotate,1].transform.eulerAngles.y ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.y >=270f ){
			rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
			rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);	
			rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.down,100 * Time.deltaTime);
		}
		else if(rotateBoard[sideRotate,1].transform.eulerAngles.y !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.y <270f ){
			rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
			rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
			rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
			rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
			rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
			rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
			rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
			rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
			rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
			rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
			rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
			rotate =0;
		}
	}
	//Side6 CounterClockwise
	if(rotate == 12){
		if(rotateBoard[sideRotate,1].transform.eulerAngles.y ==0 || rotateBoard[sideRotate,1].transform.eulerAngles.y <=90f ){
		rotateBoard[sideRotate,1].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,2].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,3].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,4].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,5].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,6].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,7].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
		rotateBoard[sideRotate,8].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);	
		rotateBoard[sideRotate,9].transform.RotateAround(rotateBoard[sideRotate,5].transform.GetChild(6).position, Vector3.up,100 * Time.deltaTime);
	}
	else if(rotateBoard[sideRotate,1].transform.eulerAngles.y !=0 ||rotateBoard[sideRotate,1].transform.eulerAngles.y >90f ){
		rotateBoard[sideRotate,1].transform.position = BoardLocation[sideRotate,1];
		rotateBoard[sideRotate,1].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,2].transform.position = BoardLocation[sideRotate,2];
		rotateBoard[sideRotate,2].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,3].transform.position = BoardLocation[sideRotate,3];
		rotateBoard[sideRotate,3].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,4].transform.position = BoardLocation[sideRotate,4];
		rotateBoard[sideRotate,4].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,5].transform.position = BoardLocation[sideRotate,5];
		rotateBoard[sideRotate,5].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,6].transform.position = BoardLocation[sideRotate,6];
		rotateBoard[sideRotate,6].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,7].transform.position = BoardLocation[sideRotate,7];
		rotateBoard[sideRotate,7].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,8].transform.position = BoardLocation[sideRotate,8];
		rotateBoard[sideRotate,8].transform.localRotation = Quaternion.Euler(0,0,0);
		rotateBoard[sideRotate,9].transform.position = BoardLocation[sideRotate,9];
		rotateBoard[sideRotate,9].transform.localRotation = Quaternion.Euler(0,0,0);
		rotate =0;
		}
		}
		
		
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
				rotate = 1;
	
				
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
				rotate=2;
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
				rotate = 3;
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
				rotate = 4;
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
				rotate = 5;
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
				rotate = 6;
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
				rotate = 7;
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
				rotate = 8;
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
				rotate = 9;
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
				rotate = 10;
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
				rotate = 11;
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
				rotate = 12;
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

		//ph.checkWin();
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
		Debug.Log("Getting static locations for rotate logic");
		for(int i = 1;i<7;i++){
			for(int j = 1;j<10;j++){
				BoardLocation[i,j] = rotateBoard[i,j].transform.position;
				//BoardRotation[i,j] = rotateBoard[i,j].transform.eulerAngles;
			}
		}
			
	}			

	IEnumerator turnOffText() {
		Debug.Log("Before Waiting 2 seconds");
		tileNumberGUI.SetActive (false);
		yield return new WaitForSeconds(1.2f);
		tileNumberGUI.SetActive (true);		
		Debug.Log("After Waiting 2 Seconds");
	}
			
			
}