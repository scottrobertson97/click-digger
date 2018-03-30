using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPanel : MonoBehaviour {
    // panel info
	public GameObject typeText;
    public GameObject costText;
    public GameObject gpsText;

    // audio
    public AudioSource purchase;
    public AudioSource upgrade;

    public GameObject buyButton;
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
        // changes to click panel design... type first, cost per unit/quantity second, gps third
		this.typeText.GetComponent<Text>().text = gameManager.Miners[minerIndex].Name.ToString();
        this.costText.GetComponent<Text>().text = "CpU: " + gameManager.Miners[minerIndex].Cost + " Qty: " + gameManager.Miners[minerIndex].Count;
		this.upLevel.GetComponentInChildren<Text>().text = "x" + gameManager.Miners[minerIndex].Level.ToString();
        this.gpsText.GetComponent<Text> ().text = "Yield: " + gameManager.Miners[minerIndex].GPS + " gps";

		if (gameManager.GoldDisplayed < gameManager.Miners [minerIndex].Cost)
			buyButton.SetActive (false);
		else
			buyButton.SetActive (true);

		switch (gameManager.Miners [minerIndex].Level) {
		case 4:
			this.levels [0].GetComponent<Toggle> ().isOn = true;
			this.levels [1].GetComponent<Toggle> ().isOn = true;
			this.levels [2].GetComponent<Toggle> ().isOn = true;
			break;
		case 3:
			this.levels [0].GetComponent<Toggle> ().isOn = true;
			this.levels [1].GetComponent<Toggle> ().isOn = true;
			this.levels [2].GetComponent<Toggle> ().isOn = false;
			break;
		case 2:
			this.levels [0].GetComponent<Toggle> ().isOn = true;
			this.levels [1].GetComponent<Toggle> ().isOn = false;
			this.levels [2].GetComponent<Toggle> ().isOn = false;
			break;
		default:
			this.levels [0].GetComponent<Toggle> ().isOn = false;
			this.levels [1].GetComponent<Toggle> ().isOn = false;
			this.levels [2].GetComponent<Toggle> ().isOn = false;
			break;
		}
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
	}
}
