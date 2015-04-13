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
				for (int i = 0; i < 4; i++) {
					if (tile != null) {
						deployArmy(this, tile);
					}
				}
			} else {
				Tile tile = AgentUtil.findSafeTile(ownedTiles);
				for (int i = 0; i < 4; i++) {
					if (tile != null) {
						deployArmy(this, tile);
					}
				}
			}
		}
		//hax
		Tile tile2 = AgentUtil.getTileWithLargestArmyAndEnemy(ownedTiles);
		while (deployableArmies > 0) {
			if (tile2 != null) {
				deployArmy(this, tile2);
			} else {
				//think I just made angry a turtler.
				tile2 = AgentUtil.getTileWithLargestArmy(ownedTiles);
			}

		}

		
	}

	public void startRotatePhase() {
		//ph.nextPhase ();
	}

	public void startAttackPhase() {
		Debug.Log ("ANGRY ATTACK");
		TileValue bestAttack = AgentUtil.findBestAttack (ownedTiles);
		if (bestAttack.value < .6) {
			//best attack sucks. don't attack.
		} else {
			attack (bestAttack.getTiles()[0], bestAttack.getTiles()[1]);
		}
		Tile tile = AgentUtil.findSafeTile (ownedTiles);
		while (tile != null) {
			Tile[] neighbors = tile.getNeighborTiles();
			Tile expandFrom = null;
			for (int i = 0; i < 4; i++) {
				if (neighbors[i].owner == this && neighbors[i].getForces() > 3) {
					expandFrom = neighbors[i];
				}
			}
			attack (expandFrom, tile);
		}

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
