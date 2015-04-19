
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
	}

	public Player(int id, int type, int color) {

		playerType = type;
		playerColor = playerColors [color];
		playerColorLight = playerColor + new Color( 0.15f,0.15f,0.15f);
		playerColorText = playerColor + new Color( 0.45f,0.45f,0.45f);
		playerID = id;
		ph = GameObject.FindObjectOfType(typeof(PhaseHandler)) as PhaseHandler;
	}


	void Start() {
		playerID = 0;
		playerColor = Color.gray;
		playerType = -1;
		rotateCards = 0;
		go = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		camera = GameObject.FindObjectOfType(typeof(CameraControll)) as CameraControll;
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

	public void resolve() {
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
	
		attackResolve.isResolving = false;
		attackResolve.renderOwnerColor ();
		showResolveCount = true;		
		defendResolve.isResolving = false;
		defendResolve.renderOwnerColor ();
	}

	public void attack(Tile attacking, Tile defending) {
		Debug.Log ("attacking!");
		Debug.Log ("AttackerID: " + attacking.owner.playerID);
		Debug.Log ("DefenderID: " + defending.owner.playerID);

		if (attacking.owner == defending.owner || attacking.getForces () < 2) {
			return;
		}

		if (attacking.getForces () < 2) {
			return;
		}

		if (defending.getForces () == 0) {
			resolveTileCount = attacking.getForces () - 2;

			attackResolve = attacking;
			defendResolve = defending;
			defending.owner = this;
			
			if (attacking.getForces () > 2) {
				resolve();
				ph.nextPhase ();
			} else {
				attacking.setForces (1);
				defending.setForces (1);
				defending.renderOwnerColor ();
			}

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
			//defender loses
			resolveTileCount = attacking.getForces () - 2;

			attackResolve = attacking;
			defendResolve = defending;
			defending.owner = this;

			if (attacking.getForces () > 2) {
				resolve ();
				ph.nextPhase ();
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
}


