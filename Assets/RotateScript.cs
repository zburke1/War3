using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public GameObject test;
	bool RotateLeft = false;
	public bool RLeftCheck;
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
				RotateLeft = true; 
				startVal = test.transform.eulerAngles.z;
		}
		//if (test.transform.eulerAngles.z < startVal + 90 && RLeftCheck) {
		if (RLeftCheck) {
						transform.RotateAround (test.transform.position, Vector3.forward, 70 * Time.deltaTime);
						print (test.transform.eulerAngles);
				} else if (!RLeftCheck) {
					transform.RotateAround (test.transform.position, Vector3.back, 70 * Time.deltaTime);
				}
		else {	
						RotateLeft = false;
				}

			}
				
	
void OnGui(){
				Event e = Event.current;
				if (e.isKey)
						Debug.Log ("Detected key code: " + e.keyCode);
	
		}

}

