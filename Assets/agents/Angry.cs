using UnityEngine;
using System.Collections;

public class Angry : Player //: WarAgent 
{
	//public int agentID;
	//not sure I need this...
	//private int gameMode;
	//private void Card[] cards;
	
	public PhaseHandler ph;

	public Angry(int id, int type, int color) {
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
	}

	//pick a tile to place an army
	public Tile thinkTilePlace() {
		return null;
	}
	
	public Tile thinkTileAttack() {
		return null;
	}
	
	//place start armies (unused)
	public void placeStartArmies() {

	}
	
	//think about cards.
	public void thinkCards() {

	}
	
	public int transferArmies() {
		return 0;
	}
	
	public void thinkFortify() {
		
	}

}
