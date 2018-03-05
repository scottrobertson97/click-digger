using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour {
	public GameObject amountText;
	public GameObject gpsText;

	private string type;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		amountText.GetComponent<Text> ().text = gameManager.CurrentMine.GetAmountOfType (this.type) + " " + this.type + "s";
		gpsText.GetComponent<Text> ().text = gameManager.miners[this.type].GPS + " Gps";
	}

	public void Initialize (string type){
		this.type = type;
	}

	public void Buy(){
		gameManager.CurrentMine.Buy (type);
	}

	public void Sell(){
		gameManager.CurrentMine.Sell (type);
	}
}
