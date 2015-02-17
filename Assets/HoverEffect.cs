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
		renderer.material.color = Color.red;
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;
		}
}
