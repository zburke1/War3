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
			//done with deployment
			ph.nextPhase ();
			return;
		} 

		//if armies remain do:
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
		while (deployableArmies > 3) {
			if (Random.Range(0,10) > 7) {
				Tile tile = AgentUtil.findEmptyCorner(this);

				if (tile != null) {
					deployArmy(this, tile);
				}
				
			} else {
				TileValue tile = AgentUtil.findSafeTile(ownedTiles);

				if (tile != null) {
					deployArmy(this, tile.getTiles()[1]);
				}
			}
		}
		//hax

		while (deployableArmies > 0) {
			Tile tile2 = AgentUtil.getTileWithLargestArmyAndEnemy(ownedTiles);
			if (tile2 != null) {
				deployArmy(this, tile2);
			} else {
				//think I just made angry a turtler.
				break;
			}
		}

		while (deployableArmies > 0) {
			TileValue tile = AgentUtil.findSafeTile(ownedTiles);
			
			if (tile != null) {
				deployArmy(this, tile.getTiles()[1]);
			} else {
				break;
			}
		} 
		
	}

	public void startRotatePhase() {
		//ph.nextPhase ();
	}

	public override void startAttackPhase() {
		Debug.Log ("ANGRY ATTACK");
		TileValue bestAttack = AgentUtil.findBestAttack (ownedTiles);
		Debug.Log ("DID BESTATTACK");
		if (bestAttack == null || bestAttack.value < .6) {
			//best attack sucks. don't attack.
			Debug.Log ("Angry: bestAttack null or less than .6");
		} else {
			Debug.Log("Angry attack with best attack chance.");
			attack (bestAttack.getTiles()[0], bestAttack.getTiles()[1]);
		}

		ArrayList largeTiles = AgentUtil.getTilesWithArmiesAtLeast (ownedTiles, 2);

		for (int i = 0; i < largeTiles.Count; i++) {
			TileValue tiles = AgentUtil.findSafeTile (largeTiles);
			if (tiles != null) {
				Debug.Log ("Expanding from " + tiles.getTiles()[0].tileID + " to " + tiles.getTiles ()[1].tileID);
				camera.AIRotateCamera(tiles.getTiles()[0].face);
				//monoB.StartCoroutine(waitAttack(tiles.getTiles ()[0],tiles.getTiles ()[1]));
				attack (tiles.getTiles ()[0], tiles.getTiles ()[1]);
			}
		}

	}
	private IEnumerator waitAttack(Tile x, Tile y){
		yield return new WaitForSeconds(5);
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
