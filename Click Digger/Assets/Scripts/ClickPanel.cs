using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour {
	public GameObject amountText;
	public GameObject gpsText;

    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;

    private string type;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		amountText.GetComponent<Text> ().text = gameManager.CurrentMine.GetAmountOfType (this.type) + " " + this.type + "s";
		gpsText.GetComponent<Text> ().text = (gameManager.Miners[this.type].GPS * gameManager.Miners[this.type].Level) + " Gps";
    }

	public void Initialize (string type){
		this.type = type;

        lv1.GetComponent<Toggle>().isOn = false;
        lv2.GetComponent<Toggle>().isOn = false;
        lv3.GetComponent<Toggle>().isOn = false;
    }

	public void Buy(){
		gameManager.CurrentMine.Buy (type);
	}

	public void Sell(){
		gameManager.CurrentMine.Sell (type);
	}

    public void Upgrade(){
        if (gameManager.Miners[type].Level < 4)
        {
            gameManager.Upgrade(type);
        }
        switch (gameManager.Miners[type].Level.ToString())
        {
            case "2":
                lv1.GetComponent<Toggle>().isOn = true;
                break;
            case "3":
                lv2.GetComponent<Toggle>().isOn = true;
                break;
            case "4":
                lv3.GetComponent<Toggle>().isOn = true;
                break;
        }
    }
}
