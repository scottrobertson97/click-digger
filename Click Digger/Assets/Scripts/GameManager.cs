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
	private int currentMineIndex = 0;
	private List<Mine> mines = new List<Mine> ();
	private enum Stage {Mine, Planet, Solarsystem, Galaxy};
	private Stage stage = Stage.Mine;
	#endregion

	/// <summary>
	/// Miner object, has a cost and gold per second
	/// </summary>
	public struct Miner {
		private double gps;
		private int cost;
        private double level;

		public double GPS {
			get { return gps; }
			set { gps = value; }
		}

		public int Cost {
			get { return cost; }
		}

        public double Level
        {
            get { return level; }
        }

        public Miner(double gps, int cost, double level){
			this.gps = gps;
			this.cost = cost;
            this.level = level;
		}

        public void LevelUp()
        {
            if(level <= 3)level++;
        }
	};

	#region Properties
	public int GoldDisplayed{ get { return this.goldDisplayed; } }
	public double GoldPerSecond{ get { return this.goldPerSecond; } }
	public double ClickMultiplier{ get { return this.clickMultiplier; } }
	public Mine CurrentMine{ get { return mines [currentMineIndex]; } }
	#endregion

	#region Miners
	public Dictionary<string, Miner> minersMine = new Dictionary<string, Miner>{
		{"Dwarf", new Miner(1, 10, 1)},
		{"Big Dwarf", new Miner(10, 100, 1)},
		{"Digging Machine", new Miner(100, 1000, 1)},
		{"Train Extractor", new Miner(1000, 10000, 1)}
	};

	public Dictionary<string, Miner> minersPlanet = new Dictionary<string, Miner>{
		{"City Boring Machine", new Miner(1, 10, 1)},
		{"Island Leveler", new Miner(10, 100, 1)},
		{"Continent Eater", new Miner(100, 1000, 1)}
	};

	public Dictionary<string, Miner> minersSolarsystem = new Dictionary<string, Miner>{
		{"Orbital Mining Lazer", new Miner(1, 10, 1)},
		{"Planet Grinder", new Miner(10, 100, 1)},
		{"Anit-Matter Explosive Crew", new Miner(100, 1000, 1)}
	};

	public Dictionary<string, Miner> minersGalaxy = new Dictionary<string, Miner>{
		{"Gravity Wave Extractor", new Miner(1, 10, 1)},
		{"Black Hole", new Miner(10, 100, 1)},
		{"Tesseract", new Miner(100, 1000, 1)}
	};
	#endregion

	/// <summary>
	/// Gets the miners.
	/// </summary>
	/// <value>The miners.</value>
	public Dictionary<string, Miner> Miners{
		get{ 
			switch (this.stage) {
			case Stage.Mine:
				return this.minersMine;
			case Stage.Planet:
				return this.minersPlanet;
			case Stage.Solarsystem:
				return this.minersSolarsystem;
			case Stage.Galaxy:
				return this.minersGalaxy;
			}
			return null;
		}
	}

	// Usethis for initialization
	void Start () {
		this.stage = Stage.Mine;
		CreateMine ();
		//CurrentMine.Init ();
		//GameObject.Find ("UIManager").GetComponent<UIManager> ().Init ();
	}
	
	// Update is called once per frame
	void Update () {
		this.goldDisplayed = (int)this.gold;

		this.goldPerSecond = 0;
		foreach (Mine m in mines) {
			this.goldPerSecond += m.GoldPerSecond;
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

    /// <summary>
    /// Upgrade a type of Miner
    /// </summary>
    public void Upgrade(string type)
    {
        Miners[type] = new Miner(1, 10, Miners[type].Level + 1);
    }

    public void Click(){
		this.gold += this.clickMultiplier;
	}

	public void CreateMine(){
		Mine m = gameObject.AddComponent<Mine> () as Mine;
		this.mines.Add (m);
	}

	public void GoToMineAtIndex(int index){
		if (index >= 0 && index < mines.Count)
			currentMineIndex = index;
	}

	/// <summary>
	/// Advances the stage of the game
	/// </summary>
	public void AdvanceStage(){
		switch (this.stage) {
		case Stage.Mine:
			this.stage = Stage.Planet;
			break;
		case Stage.Planet:
			this.stage = Stage.Solarsystem;
			break;
		case Stage.Solarsystem:
			this.stage = Stage.Galaxy;
			break;
		case Stage.Galaxy:
			return;
		}
		//destorys all mines
		mines.Clear ();
		//cretaes 1
		CreateMine ();
		//set index
		currentMineIndex = 0;
		//init UI
		GameObject.Find ("UIManager").GetComponent<UIManager> ().Init ();
	}
}
