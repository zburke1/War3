using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject CurrentMenu;

	public void Start() {
		ShowMenu(CurrentMenu);
	}

	public void ShowMenu(GameObject menu){
		if(CurrentMenu != null)
			CurrentMenu.GetComponent<Menu>().IsOpen = false;
		
		CurrentMenu = menu;
		CurrentMenu.GetComponent<Menu>().IsOpen = true;
	}﻿

	public void Quit() {
		Application.Quit();
	}
	
}
