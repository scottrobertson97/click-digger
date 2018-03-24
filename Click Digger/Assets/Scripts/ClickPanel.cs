using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour {
	public GameObject amountText;
	public GameObject gpsText;
	private static int MINER_INDEX = 0;
	private int minerIndex;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		this.minerIndex = MINER_INDEX;
		MINER_INDEX++;
	}
	
	// Update is called once per frame
	void Update () {
		this.amountText.GetComponent<Text> ().text = gameManager.Miners[minerIndex].Count + " " + gameManager.Miners[minerIndex].Name + "s";
		this.gpsText.GetComponent<Text> ().text = gameManager.Miners[minerIndex].GPS + " Gps";
	}

	public void Buy(){
		gameManager.Buy (minerIndex);
	}

	public void Sell(){
		gameManager.Sell (minerIndex);
	}
}
