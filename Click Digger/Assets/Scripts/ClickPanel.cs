﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour {
	public GameObject amountText;
	public GameObject gpsText;
	public GameObject buyButton;
    public GameObject costText;
    public GameObject upLevel;
	public List<GameObject> levels;
	private static int MINER_INDEX = 0;
	private int minerIndex;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		this.minerIndex = MINER_INDEX;
		MINER_INDEX++;
		foreach (GameObject l in this.levels) {
			l.GetComponent<Toggle> ().isOn = false;
		}

        this.upLevel.GetComponentInChildren<Text>().text = "x" + gameManager.Miners[minerIndex].Level.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		this.amountText.GetComponent<Text> ().text = gameManager.Miners[minerIndex].Count + " " + gameManager.Miners[minerIndex].Name + "s";
		this.gpsText.GetComponent<Text> ().text = gameManager.Miners[minerIndex].GPS + " Gps";
        this.costText.GetComponent<Text>().text = gameManager.Miners[minerIndex].Cost + "Gold";
		if (gameManager.GoldDisplayed < gameManager.Miners [minerIndex].Cost)
			buyButton.SetActive (false);
		else
			buyButton.SetActive (true);
	}

	public void Buy(){
		if(gameManager.Buy (minerIndex)){
		//reveal the next thing
			gameManager.Progress = minerIndex + 1;
		}
	}

	public void Sell(){
		gameManager.Sell (minerIndex);
	}

	public void Upgrade(){
		gameManager.Upgrade (minerIndex);
        this.upLevel.GetComponentInChildren<Text>().text = "x" + gameManager.Miners[minerIndex].Level.ToString();

        switch (gameManager.Miners [minerIndex].Level) {
		case 4:
			this.levels [2].GetComponent<Toggle> ().isOn = true;
			goto case 3;
		case 3:
			this.levels [1].GetComponent<Toggle> ().isOn = true;
			goto case 2;
		case 2:
			this.levels [0].GetComponent<Toggle> ().isOn = true;
			break;
		}
	}
}
