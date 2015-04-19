using UnityEngine;
using System.Collections;

public class Angry : Player //: WarAgent 
{
	//public int agentID;
	//not sure I need this...
	//private int gameMode;
	//private void Card[] cards;



	public Angry(int id, int type, int color) {
		Debug.Log ("INIT ANGRY");
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
		playerColorLight = playerColor + new Color( 0.15f,0.15f,0.15f);
		playerColorText = playerColor + new Color( 0.5f,0.5f,0.5f);
	}

	public override void startDeployPhase() {
		
		//call AgentUtil.howManyArmies();
		//sooooper important to set armies!
		Debug.Log ("ANGRY STARTING DEPLOY");
		deployableArmies = troopSpawnCount;
		ownedTiles = AgentUtil.loadPlayerTiles(this);

		//first, check for any urgent tiles.
		deployUrgent();
		if (deployableArmies == 0) {
			Debug.Log ("No urgent tiles");
			//done with deployment
			ph.nextPhase ();
			return;
		} 

		//if armies remain do:
		Debug.Log ("Angry deploying");
		deployArmies ();

		ph.nextPhase ();

	}

	public void deployUrgent() {
		Tile tile = AgentUtil.checkUrgentTile(ownedTiles);
		while (tile != null && deployableArmies > 0) {
			deployArmy(this, tile);
			tile = AgentUtil.checkUrgentTile(ownedTiles);
		}
	}

	//default behaviour for angry
	public void deployArmies() {
		while (deployableArmies > 2) {
			Debug.Log ("Angry deploying corner");
			Tile tile = AgentUtil.findEmptyCorner(this);

			if (tile != null) {
				deployArmy(this, tile);
			}
		}
		//hax
		Debug.Log ("Angry deploying safe");
		while (deployableArmies > 0) {
			Tile tile = AgentUtil.getTileWithLargestArmyAndEnemy(ownedTiles);
			if (tile != null) {
				deployArmy(this, tile);
			} else {
				//think I just made angry a turtler.
				break;
			}
		}

		int face = AgentUtil.getFaceWithMostTiles(ownedTiles);
		ArrayList tiles = AgentUtil.getTilesOnFace(ownedTiles, face);
		int least = 100;
		while (deployableArmies > 0) {
			Tile placementTile = null;
			for (int i = 0; i < tiles.Count; i++) {
				Tile tmpTile = (Tile)tiles[i];
				if (tmpTile.getForces() <= least) {
					placementTile = tmpTile;
					least = tmpTile.getForces ();
				}
			}
			if (placementTile != null) {
				deployArmy(this, placementTile);
			} 
		}
	}

	public void startRotatePhase() {
		//ph.nextPhase ();
	}

	public override void startAttackPhase() {
		Debug.Log ("ANGRY ATTACK");
		//finds tile with adjacent enemy
		TileValue bestAttack = AgentUtil.findBestAttack (ownedTiles);
		if (bestAttack != null) {
			//stupid to have to check this condition twice
			while (bestAttack != null && bestAttack.value > .6) {
				Debug.Log ("BestAttack: " + bestAttack.getTiles () [0].tileID);
				camera.AIRotateCamera(bestAttack.getTiles()[0].face);
				//monoB.StartCoroutine(waitAttack(bestAttack.getTiles ()[0],bestAttack.getTiles ()[1],2));
				//attack (bestAttack.getTiles () [0], bestAttack.getTiles () [1]);
				bestAttack = AgentUtil.findBestAttack (ownedTiles);
				Debug.Log ("new best attack");
				//this is so stupid
				if (bestAttack == null) {
					break;
				}
			}
		} else {
			Debug.Log ("BestAttack null");
		}

		ArrayList largeTiles = AgentUtil.getTilesWithArmiesAtLeast (ownedTiles, 2);
		//used to halt the loop via debug.
		bool masterKey = true;
		//loop until no safe tiles found	
		while(masterKey) {
			Debug.Log ("startloop");
			if (largeTiles.Count == 0) {
				break;
			}
			//find safe tile will no longer return if attacking tile forces is one...
			TileValue tiles = AgentUtil.findSafeExpandTile (largeTiles);
			if (tiles != null) {
				Debug.Log ("Expanding from " + tiles.getTiles()[0].tileID + " to " + tiles.getTiles ()[1].tileID);
				camera.AIRotateCamera(tiles.getTiles()[0].face);
				//monoB.StartCoroutine(waitAttack(tiles.getTiles ()[0],tiles.getTiles ()[1],2));
				delay (1);
				while (Waiter.wait) {
					//more waiting!
				}
				Debug.Log ("Han shot first");
				attack (tiles.getTiles ()[0], tiles.getTiles ()[1]);
				largeTiles = AgentUtil.getTilesWithArmiesAtLeast (ownedTiles, 2);
				break;
			} else {
				Debug.Log ("No safe tiles found!");
				break;
			}

		} 
		Debug.Log ("Angry ends attack");
		ph.nextPhase ();
	}
	//just pass the number of seconds you want to wait.
	private void delay(int t) {
		Debug.Log ("starting delay");
		Waiter.StartTimer (t);
		while (Waiter.wait) {
			//wait...
		}
		Debug.LogWarning ("done with delay");
	}
	private IEnumerator waitAttack(Tile x, Tile y,int t){
		//t = seconds
		yield return new WaitForSeconds(t);
		Debug.Log ("Done waiting");
		attack (x, y);
		
	}

	public Tile thinkTileAttack() {
		return null;
	}

	//think about cards.
	public void thinkCards() {

	}
	
	public int transferArmies() {
		return 0;
	}
	
}
