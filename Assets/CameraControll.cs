using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
	int loc = 1;
	bool rotate =false;
	bool RotateLeft = false;
	bool RotateRight = false;
	public GameObject centerrotate;
	public Transform startMarker;
	public Transform LocA;
	public Transform LocB;
	public Transform LocC;
	public Transform LocD;
	public Transform LocE;
	public Transform LocF;
	private Transform endMarker;
   public float speed = 1.0F;
	
	
	float startVal = 0f; 
	float curVal = 0f;
	// Use this for initialization
	void Start () {
		startVal=transform.eulerAngles.y;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp(KeyCode.LeftArrow)){
			if(loc==1){
				loc=2;}
			else if(loc==2){
				loc=3;}
			else if(loc==3){
				loc=4;}
			else if(loc==4){
				loc=1;}
			else if(loc==5||loc==6){
				loc=2;
			}
		}
		
		if(Input.GetKeyUp(KeyCode.RightArrow)){
			if(loc==1){
				loc=4;}
			else if(loc==2){
				loc=1;}
			else if(loc==3){
				loc=2;}
			else if(loc==4){
				loc=3;}
			else if(loc==5||loc==6){
				loc=4;
			}
		}
		
		if(Input.GetKeyUp(KeyCode.UpArrow)){
			if(loc==1||loc==2||loc==3||loc==4){
				loc=5;
			}
			else if(loc==5){
				loc=3;
			}
			else if(loc==6){
				loc=1;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(loc==1||loc==2||loc==3||loc==4){
				loc=6;
			}
			else if(loc==5){
				loc=1;
			}
			else if(loc==6){
				loc=3;
			}
		}
			 			
		if (Input.GetKey (KeyCode.Alpha1)) {
			loc = 1;
			
			}
			
			if (Input.GetKey (KeyCode.Alpha2)) {
				loc = 2;
					
				}
				
				if (Input.GetKey (KeyCode.Alpha3)) {
					loc = 3;
						
					}
					
					if (Input.GetKey (KeyCode.Alpha4)) {
						loc = 4;
						
						}	
						
						if (Input.GetKey (KeyCode.Alpha5)) {
							loc = 5;
							
							}	
							
							if (Input.GetKey (KeyCode.Alpha6)) {
								loc = 6;
							
								}				
			
		if(loc==1 && startMarker.position!=LocA.position){
			
			transform.position = Vector3.Lerp(startMarker.position,LocA.position,0.1f);
		   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocA.rotation, 0.1f);
		
				}
			
			if(loc==2&& startMarker.position!=LocB.position){
			
				transform.position = Vector3.Lerp(startMarker.position,LocB.position,0.1f);
	 		   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocB.rotation, 0.1f);
		
			}
			
			if(loc==3&& startMarker.position!=LocC.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocC.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocC.rotation, 0.1f);
		
			}
			
			if(loc==4&& startMarker.position!=LocD.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocD.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocD.rotation, 0.1f);
		
			}
			
			if(loc==5&& startMarker.position!=LocE.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocE.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocE.rotation, 0.1f);
		
			}
			
			if(loc==6&& startMarker.position!=LocF.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocF.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocF.rotation, 0.1f);
		
			}
			
	}
	
	public void RotateCam(int mov){
		if(mov==1){
			
			transform.position = Vector3.Lerp(startMarker.position,LocB.position,0.1f);
 		   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocB.rotation, 0.1f);
		
		}
	}
}

