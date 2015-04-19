
using UnityEngine;
using System.Collections;


public class Player //: MonoBehaviour
{
	//HUMAN=0; AI = 1;
	public int playerType;
	public Color playerColor;
	protected Color[] playerColors = { Color.blue, Color.red, Color.magenta, Color.yellow, Color.cyan, new Color(0.2F, 0.3F, 0.4F)};
	public int playerID;
	public Color playerColorLight;
	protected int totalFaces;
	public GameController go;
	public int rotateCards;
	public int troopSpawnCount;


	protected int deployableArmies;
	protected ArrayList ownedTiles;
	//todo: look into this
	//Tile[] ownedTiles = new Tile[54];
	public PhaseHandler ph;

	public Player (){
		playerID = -1; //Nonplayer
		playerColor = Color.white;
		playerColorLight = new Color32(127,153,255,1);
		playerType = -1; //Nonplayer
	}

	public Player(int id, int type, int color) {
		playerType = type;
		playerColor = playerColors [color];
		playerColorLight = new Color32(127,153,255,1);
		playerID = id;
	}


	void Start() {
		playerID = 0;
		playerColor = Color.gray;
		playerType = -1;
		rotateCards = 0;
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
	}

	void Update() {

	}

	public void setPlayer(int id, int type, int color) {
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
	}

	public  int playerTileCount(Player owner) {
		int tiles = 0;
		Tile tile;
		for (int i = 1; i < 7; i++) {
			for (int j = 1; j <10; j++) {
				tile = go.faces[i,j].gameObject.GetComponent<Tile>();
				if (tile.owner == owner) {
					tiles++;
				}
			}
		}
		return tiles;
	}

	//deployment phase place armies.
	//not to be confused with placeArmy.
	public bool deployArmy(Player player, Tile tile) {
		Debug.Log ("Placing army on tile " + tile.tileID + "(" + deployableArmies + " armies before placement)");
		if (deployableArmies > 0) {
			placeArmy(player, tile);
			deployableArmies--;
		} else {
			return false;
		}
		return true;
	}

	public bool placeArmy(Player player, Tile tile) {
		if (tile.owner != player) {
			return false;
		} else {
			tile.incArmy();
		}
		return true;
	}

	public void attack(Tile attacking, Tile defending) {
		Debug.Log ("attacking!");
		Debug.Log ("AttackerID: " + attacking.owner.playerID);
		Debug.Log ("DefenderID: " + defending.owner.playerID);
		if (attacking.owner == defending.owner) {
			Debug.Log ("Same team!");
			return;
		}
		//if (attacking.owner == this) {

		//	return;
		//}
		if (attacking.getForces () < 2) {
			return;
		}

		if (defending.getForces () == 0) {
			defending.setForces (1);
			defending.owner = this;
			attacking.decArmy();
			defending.renderOwnerColor();
			//ph.startWinBattlePhase();
 			return;
		}

		int[] attackResult = Dice.roll (attacking.getForces (), defending.getForces ());
		int attackerLosses = attackResult [0];
		int defenderLosses = attackResult [1];
		attacking.decArmy (attackerLosses);
		//check if defender has more armies than were lost
		if (defending.getForces () > defenderLosses) {
			defending.decArmy (defenderLosses);
		} else {
			//defender loses. move one army by default to the next 

			defending.setForces (1);
			defending.setOwner (this);
			attacking.decArmy();
			defending.renderOwnerColor();
			//ph.startWinBattlePhase();
			return;
			//ph. start combat win phase
			//viktor: this phase of yours will NEED to update the player ownedTiles member
		}
	
	}

	//overridden by AI:
	public virtual void startDeployPhase () {}
	public virtual void startRotatePhase() {}
	public virtual void startAttackPhase() {}
}


