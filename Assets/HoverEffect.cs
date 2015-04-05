using UnityEngine;
using System.Collections;

public class HoverEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		renderer.material.color = Color.green;
		renderer.material.SetInt (14, 300);
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;
		}
		
	void OnMouseDown(){
		Debug.Log(gameObject.transform.parent.gameObject);
		}
}
