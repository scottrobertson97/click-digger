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
	#endregion

	/// <summary>
	/// Miner object, has a cost and gold per second
	/// </summary>
	public struct Miner {
		private string name;
		private int baseGPS;
		private int baseCost;
		private int count;
		private double multiplier;

		public double GPS {
			get { return this.baseGPS * this.multiplier; }
			set { this.multiplier = value; }
		}

		public int Count{
			get{ return this.count; }
			set{ this.count = value; }
		}

		public int Cost {
			get { 
				return this.baseCost * (this.count + 1) /2;
			}
		}

		public string Name{
			get {return this.name;}
		}

		public Miner(string name, int baseGPS, int baseCost){
			this.name = name;
			this.baseGPS = baseGPS;
			this.baseCost = baseCost;
			this.count = 0;
			this.multiplier = 1.0;
		}
	};

	#region Properties
	public int GoldDisplayed{ get { return this.goldDisplayed; } }
	public double GoldPerSecond{ get { return this.goldPerSecond; } }
	public double ClickMultiplier{ get { return this.clickMultiplier; } }
	public List<Miner> Miners{get{ return miners; } }
	#endregion

	#region List of Miners
	private List<Miner> miners = new List<Miner> {
		new Miner("Dwarf", 1, 10),
		new Miner("Big Dwarf", 10, 100),
		new Miner("Digging Machine", 100, 1000),
		new Miner("Train Extractor", 1000, 10000),
		new Miner("City Boring Machine", 10000, 100000),
		new Miner("Island Leveler", 100000, 1000000),
		new Miner("Continent Eater", 1000000, 10000000),
		new Miner("Orbital Mining Lazer", 1000000, 10000000),
		new Miner("Planet Grinder", 1000000, 10000000),
		new Miner("Anit-Matter Explosive Crew", 1000000, 10000000),
		new Miner("Gravity Wave Extractor", 1000000, 10000000),
		new Miner("Black Hole", 1000000, 10000000),
		new Miner("Tesseract", 1000000, 10000000)
	};
	#endregion

	// Usethis for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Earn ();
		this.goldDisplayed = (int)this.gold;
	}

	private void Earn(){
		this.goldPerSecond = 0;
		for (int i = 0; i < this.miners.Count; i++) {
			this.goldPerSecond += this.miners [i].Count * this.miners [i].GPS;
		}
		double earned = this.goldPerSecond * Time.deltaTime;
		this.goldEarned += earned;
		this.gold += earned;
	}

	/// <summary>
	/// Can the player buy for this cost
	/// if so, pay
	/// </summary>
	/// <param name="cost">Cost.</param>
	public void Buy(int index){
		if (this.miners [index].Cost <= this.gold) {
			this.gold -= this.miners [index].Cost;
			Miner m = this.miners [index];
			m.Count++;
			this.miners [index] = m;
		}
	}

	/// <summary>
	/// Gain the money from selling
	/// </summary>
	/// <param name="cost">Cost.</param>
	public void Sell(int index){
		if (miners[index].Count > 0) {
			//sell it
			Miner m = miners[index];
			m.Count--;
			miners [index] = m;
			//get 80% of the cost back
			this.gold += miners[index].Cost * 0.5;
		}
	}

	public void Click(){
		this.gold += this.clickMultiplier;
	}
}
