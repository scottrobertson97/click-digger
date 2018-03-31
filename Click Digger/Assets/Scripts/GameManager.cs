using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {
	#region Private Variables
	private double goldEarned = 0;
	private double gold = 0;
	private double goldPerSecond = 0;
	private double clickMultiplier = 1;
	private int progress = 0;
    private float timeBetweenSaves = 0;
    private double goldAddon;
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
        private double addon;

        public double Add{
            get { return addon; }
            set { addon = value; }
        }
		public double GPS {	get { return this.addon + this.baseGPS * this.level; } }
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
            this.addon = 0;
		}
	};

    /// <summary>
    /// ClickUpgrade object that is used to create/reveal buttons
    /// </summary>
    public struct ClickUpgrade{
        public string description;
        public float cost;
        public char id;//Type of upgrade
        public string data;
        public bool active;//Should button be shown?

        public ClickUpgrade(char id, string data, string description, int cost){
            this.id = id;
            this.data = data;
            this.description = description;
            this.cost = cost;
            this.active = false;
        }
    }

    //List of Upgrades
    public List<ClickUpgrade> ClickUpgrades = new List<ClickUpgrade>
    {
        new ClickUpgrade('A',"2","Double click efficiency", 5000),//Type, Multiplier, Description, Cost
        new ClickUpgrade('B',"021","For each Dwarf, Digging Machines +0.5", 500),//Type, index of counted miner | index of affected | multilier for addon ( x * 0.5), Description, Cost
        new ClickUpgrade('C',"2","Total GPS +2%", 10000)//Type, Percentage, Description, Cost
    };

    //Rules for upgrades
    public List<String> UpgradeRules = new List<String>
    {
        "A,200",//Type, needed GPS
        "B,0,20",//Type, index of miner checked, needed count to unlock
        "C,400"//Type, needed GPS
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
		Load ();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.S))
			Save();
		if(Input.GetKeyDown(KeyCode.L))
			Load();
		#endif

		Earn ();

		this.timeBetweenSaves -= Time.deltaTime;
		if (this.timeBetweenSaves <= 0) {
			this.timeBetweenSaves = 10;
			Save ();
			Debug.Log ("Save");
		}
	}

	private void Earn(){		
		double gps = 0;
		for (int i = 0; i < this.miners.Count; i++) {
			gps += this.miners [i].Count * this.miners [i].GPS;
		}
		this.goldPerSecond = gps + this.goldAddon;
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
			this.gold -= this.miners [index].Cost;
			Miner m = this.miners [index];
			m.Count++;
			this.miners [index] = m;

            CheckUpgradeRules();//Check to see if Upgrade is unlocked

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
		if (this.miners [index].Level < 4 && this.miners[index].Cost * 5 * this.miners[index].Level <= this.gold) {
            this.gold -= this.miners[index].Cost * 5 * this.miners[index].Level;
            Miner m = this.miners [index];
			m.Level++;
			this.miners [index] = m;
		}
	}


    /// <summary>
    /// Check to see if Upgrades have been unlocked
    /// </summary>
    public void CheckUpgradeRules(){
        for(int i = 0; i < UpgradeRules.Count; i++){
            String[] vars = UpgradeRules[i].Split(',');
            switch (UpgradeRules[i][0])
            {
                case 'A':
                    if (this.goldPerSecond >= int.Parse(vars[1])){
                        ClickUpgrade c = ClickUpgrades[i];
                        c.active = true;
                        ClickUpgrades[i] = c;
                    };
                    break;
                case 'B':
                    if(this.miners[int.Parse(vars[1])].Count >= int.Parse(vars[2])){
                        ClickUpgrade c = ClickUpgrades[i];
                        c.active = true;
                        ClickUpgrades[i] = c;
                    }
                    break;
                case 'C':
                    if (this.goldPerSecond >= int.Parse(vars[1]))
                    {
                        ClickUpgrade c = ClickUpgrades[i];
                        c.active = true;
                        ClickUpgrades[i] = c;
                    };
                    break;
            }
        }
    }

    /// <summary>
    /// Does effect of an Upgrade
    /// </summary>
    /// <param name="up"></param>
    public void DoClickUpgrade(ClickUpgrade up){
        switch (up.id)
        {
            case 'A':
                clickMultiplier *= int.Parse(up.data); 
                break;
            case 'B':
                Miner m = this.miners[int.Parse(up.data[1].ToString())];
                m.Add = this.miners[int.Parse(up.data[0].ToString())].Count * (int.Parse(up.data[2].ToString()) * 0.5);
                this.miners[int.Parse(up.data[1].ToString())] = m;
                break;
            case 'C':
                this.goldAddon = this.goldPerSecond * up.data[0];
                break;
        }
    }

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.gold = this.gold;
		data.goldEarned = this.goldEarned;
		data.progress = this.progress;
		data.miners = new int[this.miners.Count * 2];
		for (int i = 0; i < this.miners.Count; i++) {
			data.miners [i * 2] = this.miners [i].Count;
			data.miners [i * 2 + 1] = this.miners [i].Level;
		}

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			this.gold = data.gold;
			this.goldEarned = data.goldEarned;
			this.progress = data.progress;
			for (int i = 0; i < this.miners.Count; i ++) {
				Miner m = this.miners [i];
				m.Count = data.miners [i * 2];
				m.Level = data.miners [i * 2 + 1];
				this.miners [i] = m;
			}
		}
	}

	public void Reset(){
		this.gold = 0;
		this.goldEarned = 0;
		this.progress = 0;
		for (int i = 0; i < this.miners.Count; i ++) {
			Miner m = this.miners [i];
			m.Count = 0;
			m.Level = 1;
			this.miners [i] = m;
		}
	}
}

[Serializable]
class PlayerData{
	public double gold;
	public double goldEarned;
	public int[] miners;
	public int progress;
}