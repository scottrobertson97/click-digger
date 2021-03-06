﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {/*
	/// <summary>
	/// Count of each type of miner this mine has
	/// </summary>
	private List<int> minersCount;
	private GameManager gameManager;
	private double goldPerSecond = 0;

	public double GoldPerSecond { get { return goldPerSecond; } }

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//fill this dictionary with the same keys from the gameManager.miners
		this.minersCount = new List<int>();
		for (int i = 0; i < gameManager.Miners.Count; i++) {
			minersCount.Add ( 0);
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
		for (int i = 0; i < gameManager.Miners.Count; i++) {
			minersCount.Add ( 0);
		}
		//summ the GPS of all the miners
		foreach (string type in minersCount.Keys) {
			//try{
				goldPerSecond += minersCount [type] * gameManager.Miners [type].GPS;
			//}
			//catch(KeyNotFoundException e){}
		}
		gameManager.Earn (goldPerSecond * Time.deltaTime);
	}

	/// <summary>
	/// Buy 1 of the specified type.
	/// </summary>
	/// <param name="type">Type.</param>
	public void Buy(string type){
		//can we buy this type for this cost
		//if so the gamemanager will do the gold, and we add to the count
		if (gameManager.Buy (CalculateCost(type)))
			minersCount [type]++;
	}

	/// <summary>
	/// Sell 1 of the specified type.
	/// </summary>
	/// <param name="type">Type.</param>
	public void Sell(string type){
		//if we have miners to sell 
		if (minersCount [type] > 0) {
			//sell it
			minersCount [type]--;
			//get 80% of the cost back
			gameManager.Sell (CalculateCost (type) * 0.5);
		}
	}

	/// <summary>
	/// Calculates the cost of a miner
	/// </summary>
	/// <returns>The cost.</returns>
	/// <param name="type">Type.</param>
	public double CalculateCost(string type){
		// base cost * (gps + miner.gps) / miner.gps
		//this is how cookie clicker calculates cost
		//return gameManager.miners [type].Cost * (goldPerSecond + gameManager.miners [type].GPS) / gameManager.miners [type].GPS;
		return gameManager.Miners [type].Cost * (minersCount[type] + 1) /2;
	}

	public int GetAmountOfType(string type){
		return minersCount [type];
	}
*/}
