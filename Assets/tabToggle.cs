using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class tabToggle : MonoBehaviour {
	public Text text;
	// Use this for initialization
	void Start () {
	text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Tab)){
			if(text.enabled == true){
				Debug.Log(text.IsActive());
				text.enabled = false;
			}
			else{
				text.enabled = true;	
			}
			
	}
}
}
