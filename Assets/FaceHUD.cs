using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(RawImage))]

public class FaceHUD : MonoBehaviour {
	public RawImage faceHUD;
	// Use this for initialization
	void Start () {
		faceHUD = GetComponent<RawImage>();
	
	}
	
	// Update is called once per frame
	void Update () {
		//If the User presses tab, it toggles the image.
		if (Input.GetKeyDown (KeyCode.Tab)){
			if(faceHUD.enabled == true){
				Debug.Log(faceHUD.IsActive());
				faceHUD.enabled = false;
			}
			else{
				faceHUD.enabled = true;	
			}
		}
	}


}
