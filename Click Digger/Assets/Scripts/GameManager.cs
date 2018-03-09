using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	#region Private Variables
	private double goldEarned = 0;
	private double gold = 0;
	private int goldDisplayed = 0;
	private double goldPerSecond = 0;
	private double clickMultiplier = 1;
	private int currentMineIndex;
	private List<Mine> mines;
	private enum Stage {Mine, Planet, Solarsystem, Galaxy};
	private Stage stage;
	#endregion

	public struct Miner {
		private double gps;
		private int cost;

		public double GPS {
			get { return gps; }
			set { gps = value; }
		}

		public int Cost {
			get { return cost; }
		}

		public Miner(double gps, int cost){
			this.gps = gps;
			this.cost = cost;
		}
	};

	#region Properties
	public int GoldDisplayed{ get { return this.goldDisplayed; } }
	public double GoldPerSecond{ get { return this.goldPerSecond; } }
	public double ClickMultiplier{ get { return this.clickMultiplier; } }
	public Mine CurrentMine{ get { return mines [currentMineIndex]; } }
	#endregion

	public Dictionary<string, Miner> miners;

	#region Miners
	public Dictionary<string, Miner> minersMine = new Dictionary<string, Miner>{
		{"Dwarf", new Miner(1, 10)},
		{"Big Dwarf", new Miner(10, 100)},
		{"Digging Machine", new Miner(100, 1000)}
	};

	public Dictionary<string, Miner> minersPlanet = new Dictionary<string, Miner>{
		{"City Boring Machine", new Miner(1, 10)},
		{"Island Leveler", new Miner(10, 100)},
		{"Continent Eater", new Miner(100, 1000)}
	};

	public Dictionary<string, Miner> minersSolarsystem = new Dictionary<string, Miner>{
		{"Orbital Mining Lazer", new Miner(1, 10)},
		{"Planet Grinder", new Miner(10, 100)},
		{"Anit-Matter Explosive Crew", new Miner(100, 1000)}
	};

	public Dictionary<string, Miner> minersGalaxy = new Dictionary<string, Miner>{
		{"Gravity Wave Extractor", new Miner(1, 10)},
		{"Black Hole", new Miner(10, 100)},
		{"Tesseract", new Miner(100, 1000)}
	};
	#endregion

	// Usethis for initialization
	void Start () {
		stage = Stage.Mine;
		miners = new Dictionary<string, Miner> (minersMine);
		this.mines = new List<Mine> ();
		CreateMine ();
		currentMineIndex = 0;
		CurrentMine.Init ();
		GameObject.Find ("UIManager").GetComponent<UIManager> ().Init ();
	}
	
	// Update is called once per frame
	void Update () {
		goldDisplayed = (int)gold;

		goldPerSecond = 0;
		foreach (Mine m in mines) {
			goldPerSecond += m.GoldPerSecond;
		}

		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.B)){
			CreateMine();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			GoToMineAtIndex(currentMineIndex + 1);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			GoToMineAtIndex(currentMineIndex - 1);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			AdvanceStage();
		}
		#endif
	}

	/// <summary>
	/// Earn the specified earned.
	/// </summary>
	/// <param name="earned">Earned.</param>
	public void Earn(double earned){
		this.goldEarned += earned;
		this.gold += earned;
	}

	/// <summary>
	/// Can the player buy for this cost
	/// if so, pay
	/// </summary>
	/// <param name="cost">Cost.</param>
	public bool Buy(double cost){
		if (cost <= this.gold) {
			this.gold -= cost;
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Gain the money from selling
	/// </summary>
	/// <param name="cost">Cost.</param>
	public void Sell(double cost){
		this.gold += cost;
	}

	public void Click(){
		this.gold += this.clickMultiplier;
	}

	public void CreateMine(){
		Mine m = gameObject.AddComponent<Mine> () as Mine;
		mines.Add (m);
	}

	public void GoToMineAtIndex(int index){
		if (index >= 0 && index < mines.Count)
			currentMineIndex = index;
	}

	public void AdvanceStage(){
		switch (stage) {
		case Stage.Mine:
			stage = Stage.Planet;
			//miners = minersPlanet;
			miners = new Dictionary<string, Miner> (minersPlanet);
			break;
		case Stage.Planet:
			stage = Stage.Solarsystem;
			//miners = minersSolarsystem;
			miners = new Dictionary<string, Miner> (minersSolarsystem);
			break;
		case Stage.Solarsystem:
			stage = Stage.Galaxy;
			//miners = minersGalaxy;
			miners = new Dictionary<string, Miner> (minersGalaxy);
			break;
		case Stage.Galaxy:
			break;
		}

		GameObject.Find ("UIManager").GetComponent<UIManager> ().Init ();
		CurrentMine.Init ();
	}
}
