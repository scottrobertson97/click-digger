using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {
	private Dictionary<string, int> minersCount;
	private GameManager gameManager;
	private double goldPerSecond = 0;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		minersCount = new Dictionary<string, int> ();
		foreach (string type in gameManager.miners.Keys) {
			minersCount.Add (type, 0);
		}
	}

	// Update is called once per frame
	void Update () {
		Earn();
	}

	/// <summary>
	/// Earn this instance.
	/// </summary>
	private void Earn(){
		goldPerSecond = 0;
		foreach (string type in minersCount.Keys) {
			goldPerSecond += minersCount [type] * gameManager.miners [type].GPS;
		}
		gameManager.Earn (goldPerSecond * Time.deltaTime);
	}

	/// <summary>
	/// Buy the specified type.
	/// </summary>
	/// <param name="type">Type.</param>
	private void Buy(string type){
		// base cost * (gps + miner.gps) / miner.gps
		//this is how cookie clicker calculates cost
		double cost = gameManager.miners [type].Cost * (goldPerSecond + gameManager.miners [type].GPS) / gameManager.miners [type].GPS;
		if (gameManager.Buy ((int)cost))
			minersCount [type]++;
	}
}
