using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public Rect buttonRect = new Rect(Screen.width/4, 50, 50, 30);
	public GameObject test;
	bool RotateLeft = false;
	float startVal = 0f; 
	private Vector3 Rotation = new Vector3 (0,0,1);
	//Rotation.Transform.eulerAngles = test.Transform.eulerAngles;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.forward, Time.deltaTime*15,Space.Self);
		if (Input.GetKey (KeyCode.RightArrow)) {
			
			//RotateLeft = true; 
			
		}
		//if (test.transform.eulerAngles.z < startVal + 90 && RLeftCheck) {
		if (RotateLeft) {
			startVal = test.transform.eulerAngles.z;
				GameObject.Find("Slice1").transform.RotateAround (test.transform.position, Vector3.forward, 70 * Time.deltaTime);
				print (test.transform.eulerAngles);
				} 
				//		else if (!RotateLeft) {
				//					transform.RotateAround (test.transform.position, Vector3.back, 70 * Time.deltaTime);
				//				}
				//		else {	
				//						RotateLeft = false;
				//				}
				
				}
				
				
				void OnGUI(){
				if (GUI.Button (buttonRect, "PLEASE!!!!!")) {
					RotateLeft = true;
				}
				Event e = Event.current;
				if (e.isKey)
					Debug.Log ("Detected key code: " + e.keyCode);
				
			}
			
			}
			
