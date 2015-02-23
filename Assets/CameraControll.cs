using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
	bool RotateLeft = false;
	bool RotateRight = false;
	public GameObject centerrotate;
	
	float startVal = 0f; 
	float curVal = 0f;
	// Use this for initialization
	void Start () {
		startVal=transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.LeftArrow)) 
			RotateLeft=true;
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			if(startVal==0){
				curVal=360;
			startVal=360;}
			RotateRight=true;
		}
		
			if(RotateLeft){
				if(curVal<startVal+90){
					transform.RotateAround (centerrotate.transform.position, Vector3.up, 90 * Time.deltaTime);
					curVal=transform.eulerAngles.y;
				}
				else{
					RotateLeft = false;
					startVal=curVal;
			}
		}
		if(RotateRight){
			if(curVal>startVal-90){
				transform.RotateAround (centerrotate.transform.position, Vector3.down, 90 * Time.deltaTime);
				curVal=transform.eulerAngles.y;
			}
			else{
				RotateRight = false;
				startVal=curVal;
		}
	}
	}
}
