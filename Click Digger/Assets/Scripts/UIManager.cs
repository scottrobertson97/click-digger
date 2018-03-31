using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject clickPanelPrefab;
    public GameObject clickButtonPrefab;
	public GameObject content;
    public GameObject content2;
	//gold per second text
	public GameObject gpsText;
	//displayed gold
	public GameObject goldText;
	//text on click button
	public GameObject clickText;
	private List<GameObject> clickPanels;
    public List<GameObject> clickUpgrades;

    private bool active;

	private GameManager gameManager;
	private int previousProgress;
    private int previousProgressUpgrades;

    // Use this for initialization
    void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		clickPanels = new List<GameObject> ();
		this.previousProgress = gameManager.Progress;
		for (int i = 0; i < gameManager.Miners.Count; i++) {
			//create click panel
			GameObject panel = Instantiate (clickPanelPrefab, content.transform);
			//turn off all but the first one
			if (i > gameManager.Progress) {
				panel.SetActive (false);
			}
			//add it to the list
			clickPanels.Add (panel);
		}

        //Instantiate buttons if revealed
        clickUpgrades = new List<GameObject> ();
        for(int i = 0; i < gameManager.ClickUpgrades.Count; i++){
            GameObject button = Instantiate(clickButtonPrefab, content2.transform);
            if (!gameManager.ClickUpgrades[i].active)
            {
                button.SetActive(true);
            }
            clickUpgrades.Add(button);
        }

        active = false;
	}
	
	// Update is called once per frame
	void Update () {
		gpsText.GetComponent<Text>().text = gameManager.GoldPerSecond + " Gps";
		goldText.GetComponent<Text> ().text = gameManager.GoldDisplayed + " Gold";
		clickText.GetComponent<Text> ().text = "+" + gameManager.ClickMultiplier + " Gold";

		//if a new thing was bought, then the progress would increment
		//so now reveal the next click panel
		if (this.previousProgress != gameManager.Progress) {
			this.previousProgress = gameManager.Progress;
			for (int i = 0; i < this.clickPanels.Count; i++) {
				if (i <= gameManager.Progress)
					this.clickPanels [i].SetActive (true);
				else
					this.clickPanels [i].SetActive (false);
			}
		}

        //Show upgrade Buttons
        for (int i = 0; i < this.clickUpgrades.Count; i++){
            if (gameManager.ClickUpgrades[i].active)
                this.clickUpgrades[i].SetActive(true);
            else
                this.clickUpgrades[i].SetActive(false);
        }

        content.SetActive(!active);
        content2.SetActive(active);

    }

    //Switch content for scroll panel
    public void SwitchContent(){
        active = !active;
    }
}
