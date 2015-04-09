using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject CurrentMenu;
	public static int Sel_NumPlayers;
	public static int Sel_Difficulty;
	public static bool Sel_AI;
	//{0,1} -> {faceCapture, Domination}
	public static int Sel_GameMode;

	//TODO: DEFINE THESE IN THEIR PROPER PLACE
	

	public void Start() {
		ShowMenu(CurrentMenu);
	}

	public void ShowMenu(GameObject menu){
		if(CurrentMenu != null) {
			CurrentMenu.GetComponent<Menu>().IsOpen = false;
		}
		
		CurrentMenu = menu;
		CurrentMenu.GetComponent<Menu>().IsOpen = true;
	}﻿



	public void Quit() {
		Application.Quit();
	}
	
	public void SetAI(int enabled) {
		Sel_AI = enabled == 0 ? true : false;
	}

	public void SetGameMode(int mode) {
		Sel_GameMode = mode;
	}

	public void SetDifficulty(int diff) {
		Sel_Difficulty = diff;
	}

	public void SetPlayers(int num) {
		Sel_NumPlayers = num;
	}

	public void StartGame() {
		Application.LoadLevel("WarScene0");
	}
}
