using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	#region Private Variables
	private double goldEarned = 0;
	private double gold = 0;
	private double goldPerSecond = 0;
	private double clickMultiplier = 1;
	private int progress = 0;
    #endregion
    
	/// <summary>
	/// Miner object, has a cost and gold per second
	/// </summary>
	public struct Miner {
		private string name;
		private int baseGPS;
		private int baseCost;
		private int count;
		private int level;
        
		public double GPS {	get { return this.baseGPS * this.level; } }
		public int Count{ 
			get { return this.count; } 
			set { this.count = value; } 
		}
		//public int Cost { get { return this.baseCost + (int)(this.count * 0.5); } }
		//this.baseCost * (goldPerSecond + this.GPS) / this.GPS;
		public double Cost { get { return this.baseCost + this.count; } }
		public string Name{	get { return this.name; } }
		public int Level{ 
			get { return this.level; } 
			set { this.level = value; }
		}

		public Miner(string name, int baseGPS, int baseCost){
			this.name = name;
			this.baseGPS = baseGPS;
			this.baseCost = baseCost;
			this.count = 0;
			this.level = 1;
		}
	};

	#region Properties
	public int GoldDisplayed{ get { return (int)this.gold; } }
	public double GoldPerSecond{ get { return this.goldPerSecond; } }
	public double ClickMultiplier{ get { return this.clickMultiplier; } }
	public List<Miner> Miners{get{ return this.miners; } }
	public int Progress{
		get{ return this.progress; }
		set{ this.progress = value; }
	}
	#endregion

	#region List of Miners
	private List<Miner> miners = new List<Miner> {
		new Miner("Dwarf", 1, 10),
		new Miner("Big Dwarf", 10, 150),
		new Miner("Digging Machine", 50, 750),
		new Miner("Train Extractor", 100, 1750),
		new Miner("City Boring Machine", 250, 5000),
		new Miner("Island Leveler", 750, 10000),
		new Miner("Continent Eater", 1000, 50000),
		new Miner("Orbital Mining Lazer", 1500, 115000),
		new Miner("Planet Grinder", 2500, 200000),
		new Miner("Anti-Matter Drill", 7500, 350000),
		new Miner("Gravity Waves", 12500, 450000),
		new Miner("Black Hole", 25000, 650000),
		new Miner("Tesseract", 50000, 950000)
	};
    #endregion
    public AudioSource purchase;
    public AudioSource upgrade;

	// Usethis for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Earn ();
	}

	private void Earn(){		
		double gps = 0;
		for (int i = 0; i < this.miners.Count; i++) {
			gps += this.miners [i].Count * this.miners [i].GPS;
		}
		this.goldPerSecond = gps;
		double earned = this.goldPerSecond * Time.deltaTime;
		this.goldEarned += earned;
		this.gold += earned;
	}

	/// <summary>
	/// Can the player buy for this cost
	/// if so, pay
	/// </summary>
	/// <param name="cost">Cost.</param>
	public bool Buy(int index){
		if (this.miners [index].Cost <= this.gold) {
            purchase.Play();
			this.gold -= this.miners [index].Cost;
			Miner m = this.miners [index];
			m.Count++;
			this.miners [index] = m;
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Gain the money from selling
	/// </summary>
	/// <param name="cost">Cost.</param>
	public void Sell(int index){
		if (miners[index].Count > 0) {
            purchase.Play();
			Miner m = this.miners [index];
			m.Count--;
			this.miners [index] = m;
			//get 80% of the cost back
			this.gold += miners[index].Cost * 0.5;
		}
	}

	public void Click(){
		this.gold += this.clickMultiplier;
	}

	public void Upgrade(int index){
        if (this.miners[index].Count > 0)
        {
            if (this.miners[index].Level < 4)
            {
                upgrade.Play();
                Miner m = this.miners[index];
                m.Level++;
                this.miners[index] = m;
            }
        }
		
	}
}
