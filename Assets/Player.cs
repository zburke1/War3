
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
	public Color playerColorText;
	protected int totalFaces;
	public GameController go;
	public CameraControll camera;
	public int rotateCards;
	public int troopSpawnCount;
	public int resolveTileCount;
	public Tile attackResolve;
	public Tile defendResolve;
	public ResolveGUI rsGUI;
	public bool showResolveCount;
	public MonoBehaviour monoB;

	protected int deployableArmies;
	protected ArrayList ownedTiles;
	//todo: look into this
	//Tile[] ownedTiles = new Tile[54];
	public PhaseHandler ph;

	public Player (){
		playerID = -1; //Nonplayer
		playerColor = Color.white;
		//playerColorLight = new Color32(127,153,255,1);
		//playerColorText = Color.white;
		playerType = -1; //Nonplayer
		camera = GameObject.FindObjectOfType(typeof(CameraControll)) as CameraControll;
		rsGUI = GameObject.FindObjectOfType(typeof(ResolveGUI)) as ResolveGUI;
		monoB= GameObject.FindObjectOfType<MonoBehaviour>();

		ownedTiles = new ArrayList ();
	}


	public Player(int id, int type, int color) {

		playerType = type;
		playerColor = playerColors [color];
		playerColorLight = playerColor + new Color( 0.15f,0.15f,0.15f);
		playerColorText = playerColor + new Color( 0.45f,0.45f,0.45f);
		playerID = id;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;

		ownedTiles = new ArrayList ();
	}

	//this doesn't do anything.
	void Start() {
		playerID = 0;
		playerColor = Color.gray;
		playerType = -1;
		rotateCards = 0;
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		camera = GameObject.FindObjectOfType(typeof(CameraControll)) as CameraControll;
	}

	//neither does this
	void Update() {

	}

	public void setPlayer(int id, int type, int color) {
		playerType = type;
		playerColor = playerColors [color];
		playerID = id;
	}

	public  int playerTileCount() {
		return ownedTiles.Count;
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

	public void dropTile(Tile tile) {
		if (ownedTiles.Contains (tile)) {
			ownedTiles.Remove (tile);
		} else {
			Debug.Log ("Not my tile, dog.");
		}
	}

	public void addTile(Tile tile) {
		if (ownedTiles.Contains (tile)) {
			Debug.Log (playerID + " already owns tile " + tile.tileID);
		} else {
			ownedTiles.Add (tile);
		}
	}

	public virtual void resolve() {
		Color tempColor = attackResolve.owner.playerColorLight;

		attackResolve.setForces (1);
		attackResolve.isResolving = true;
		showResolveCount = true;
		attackResolve.renderer.material.color = tempColor;

		defendResolve.setForces (1);
		defendResolve.isResolving = true;
		defendResolve.renderer.material.color = tempColor;
	}

	public void stopResolving() {

		ph.nextPhase ();
		attackResolve.isResolving = false;
		attackResolve.renderOwnerColor ();
		showResolveCount = true;		
		defendResolve.isResolving = false;
		defendResolve.renderOwnerColor ();
	}

	public void attack(Tile attacking, Tile defending) {
		Debug.Log ("attacking!");
		Debug.Log ("AttackerID: " + attacking.tileID);
		Debug.Log ("DefenderID: " + defending.tileID);

		if (attacking.owner == defending.owner || attacking.getForces () < 2) {
			Debug.Log ("Friendly fire, or too few armies");
			return;
		}
		//tile is unoccupied
		if (defending.getForces () == 0) {
			resolveTileCount = attacking.getForces () - 2;

			attackResolve = attacking;
			defendResolve = defending;
			defending.setOwner (this);

			if (attacking.getForces () > 2) {
				//human resolves.
				if (attacking.owner.playerType == 0) {
					Debug.Log("Entering human ResolvePhase");
					resolve();
					ph.nextPhase ();
				} else {
					//AI resolves
					int numSurvivingArmies = attacking.getForces ();
					//move at least half of the surviving armies to the new tile. integer division ftw.
					defending.setForces(numSurvivingArmies - numSurvivingArmies/2);
					attacking.setForces(numSurvivingArmies/2);
				}
			} else {
				attacking.setForces (1);
				defending.setForces (1);
				defending.renderOwnerColor ();

			}
			//attack won
			return;
		}

		int[] attackResult = Dice.roll (attacking.getForces (), defending.getForces ());
		int attackerLosses = attackResult [0];
		int defenderLosses = attackResult [1];
		attacking.decArmy (attackerLosses);
		//check if defender has more armies than were lost
		if (defending.getForces () > defenderLosses) {
			defending.decArmy (defenderLosses);
			//attacker lost.
			return;
		} else {
			//defender loses
			resolveTileCount = attacking.getForces () - 2;

			attackResolve = attacking;
			defendResolve = defending;
			//removes the old owner, and the tile from old owner's ownedTile AList
			//sets the new owner, and adds the tile to the new owner's ownedTile AList
			defending.setOwner (this);


			if (attacking.getForces () > 2) {
				if (attacking.owner.playerType == 0) {
					resolve ();
					ph.nextPhase ();
				} else {
					//AI resolves
					int numSurvivingArmies = attacking.getForces ();
					//move at least half of the surviving armies to the new tile. integer division ftw.
					defending.setForces(numSurvivingArmies - numSurvivingArmies/2);
					attacking.setForces(numSurvivingArmies/2);
				}

			} else {
				attacking.setForces (1);
				defending.setForces (1);
				defending.renderOwnerColor ();

			}
			return;

		}
	
	}

	//overridden by AI:
	public virtual void startDeployPhase () {}
	public virtual void startRotatePhase() {}
	public virtual void startAttackPhase() {}
	public virtual void startResolvePhase() {}
}


