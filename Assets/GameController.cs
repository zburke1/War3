using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Cube;
	private GameObject clone;
	private int count = 0;
	// Use this for initialization
	void Start () {
		//Builds bottom,forwards,up
		for (float y = 0; y < 3; y++) {
			count = 0;
		        for (float x = 0; x < 3; x++) {
					  for(float z=0;z<3;z++){
					  	 Instantiate(Cube, new Vector3(x*1.2f, y*1.2f, z*1.2f), Quaternion.identity);
						//clone.position(x*1.2f, y*1.2f, z*1.2f);
					  }
					  count++;
				  }
			 }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
