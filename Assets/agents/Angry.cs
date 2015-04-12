using UnityEngine;
using System.Collections;

public class Angry : WarAgent {
	public int agentID;
	//not sure I need this...
	private int gameMode;
	//private void Card[] cards;
	
	//pick a tile to place an army
	public Tile thinkTilePlace() {
	
	}
	
	public Tile thinkTileAttack();
	
	//place start armies (unused)
	public void placeStartArmies();
	
	//think about cards.
	public void thinkCards();
	
	public int transferArmies();
	
	public void thinkFortify();

}
