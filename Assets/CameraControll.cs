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
	 private Material RedOrange;
	 private Material DSG;
	 private Material DSBWP;
	 private Material StarSkyBox;

	
	float startVal = 0f; 
	float curVal = 0f;
	// Use this for initialization
	void Start () {
		startVal=transform.eulerAngles.y;
	   RedOrange = (Material)Resources.Load("3Skyboxes2/Materials/RedOrangeYellowNebular");
		DSG = (Material)Resources.Load("SkyBox Volume 2/DeepSpaceGreen/DSG");
		DSBWP = (Material)Resources.Load("SkyBox Volume 2/DeepSpaceBlueWithPlanet/DSBWP");
		StarSkyBox = (Material)Resources.Load("SkyBox Volume 2/Stars01/StarSkyBox");
		GetComponent<Skybox>().material = (Material)Resources.Load("SkyBox Volume 2/DeepSpaceGreen/DSG");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyUp(KeyCode.A)){
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
		
		if(Input.GetKeyUp(KeyCode.D)){
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
		
		if(Input.GetKeyUp(KeyCode.W)){
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
		
		if(Input.GetKeyDown(KeyCode.S)){
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
			GetComponent<Skybox>().material = DSG;
		
				}
			
			if(loc==2&& startMarker.position!=LocB.position){
			
				transform.position = Vector3.Lerp(startMarker.position,LocB.position,0.1f);
	 		   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocB.rotation, 0.1f);
				GetComponent<Skybox>().material = (Material)Resources.Load("3Skyboxes2/Materials/BlueGreenNebular");
		
			}
			
			if(loc==3&& startMarker.position!=LocC.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocC.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocC.rotation, 0.1f);
				GetComponent<Skybox>().material = (Material)Resources.Load("SkyBox Volume 2/DeepSpaceBlue/DSB");
				
			}
			
			if(loc==4&& startMarker.position!=LocD.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocD.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocD.rotation, 0.1f);
				GetComponent<Skybox>().material = RedOrange;

				
			}
			
			if(loc==5&& startMarker.position!=LocE.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocE.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocE.rotation, 0.1f);
				GetComponent<Skybox>().material = DSBWP;
		
			}
			
			if(loc==6&& startMarker.position!=LocF.position){
			
			   transform.position = Vector3.Lerp(startMarker.position,LocF.position,0.1f);
			   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocF.rotation, 0.1f);
				GetComponent<Skybox>().material = StarSkyBox;
		
			}
			
	}
	
	public void AIRotateCamera(int side){
		loc = side;
	}
	
	public void RotateCam(int mov){
		if(mov==1){
			
			transform.position = Vector3.Lerp(startMarker.position,LocB.position,0.1f);
 		   transform.rotation = Quaternion.Lerp(startMarker.rotation, LocB.rotation, 0.1f);
		
		}
	}
}

