using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {
	bool RotateLeft = false;
	public GameObject centerrotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.RotateAround (centerrotate.transform.position, Vector3.up, 40 * Time.deltaTime);
		}
	}
}
