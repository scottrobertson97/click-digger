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

	public Dictionary<string, Miner> miners = new Dictionary<string, Miner>{
		{"basic", new Miner(1, 10)},
		{"advanced", new Miner(10, 100)}
	};

	// Usethis for initialization
	void Start () {
		this.mines = new List<Mine> ();
		mines.Add (new Mine ());
		currentMineIndex = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		
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
		if (cost < this.gold) {
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

	public Mine GetCurrentMine(){
		return mines[currentMineIndex];
	}

	public void Click(){
		
	}

	public void test(string type){
		
	}
}
