using UnityEngine;
using System.Collections;

public abstract class WarAgent //: MonoBehaviour 
{
	//public int agentID;
	//not sure I need this...
	//private int gameMode;
	//private void Card[] cards;

	//pick a tile to place an army
	public abstract Tile thinkTilePlace();

	public abstract Tile thinkTileAttack();

	//place start armies (unused)
	public abstract void placeStartArmies();

	//think about cards.
	public abstract void thinkCards();

	public abstract int transferArmies();

	public abstract void thinkFortify();
}
