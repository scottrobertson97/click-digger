using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private double goldEarned = 0;
	private double gold = 0;
	private int goldDisplayed = 0;
	private double goldPerSecond = 0;
	private double clickMultiplier = 1;

	private int currentMineIndex;
	private List<Mine> mines;

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

	public int GoldDisplayed{ get { return this.goldDisplayed; } }
	public double GoldPerSecond{ get { return this.goldPerSecond; } }
	public double ClickMultiplier{ get { return this.clickMultiplier; } }
	public Mine CurrentMine{ get { return mines [currentMineIndex]; } }

	public Dictionary<string, Miner> miners = new Dictionary<string, Miner>{
		{"Dwarf", new Miner(1, 10)},
		{"Big Dwarf", new Miner(10, 100)},
		{"Digging Machine", new Miner(100, 1000)}
	};

	// Usethis for initialization
	void Start () {
		this.mines = new List<Mine> ();
		Mine m = gameObject.AddComponent<Mine> () as Mine;
		mines.Add (m);
		currentMineIndex = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		goldDisplayed = (int)gold;
	}

	/// <summary>
	/// Earn the specified earned.
	/// </summary>
	/// <param name="earned">Earned.</param>
	public void Earn(double earned){
		this.goldEarned += earned;
		this.gold += earned;
		goldPerSecond = earned / Time.deltaTime;
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

	public void test(string type){
		
	}
}
