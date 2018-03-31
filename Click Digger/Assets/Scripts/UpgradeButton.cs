using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {
    public GameObject descriptionText;
    public GameObject costText;
    public int upgradeIndex;
    private GameManager gameManager;
    public Button thisButton;
    private static int UPGRADE_INDEX = 0;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.upgradeIndex = UPGRADE_INDEX;
        UPGRADE_INDEX++;
    }
	
	// Update is called once per frame
	void Update () {
        this.descriptionText.GetComponent<Text> ().text = gameManager.ClickUpgrades[upgradeIndex].description;
        this.costText.GetComponent<Text> ().text = gameManager.ClickUpgrades[upgradeIndex].cost.ToString() + " Gold";
    }

    public void ClickUpgrade()
    {
        gameManager.DoClickUpgrade(gameManager.ClickUpgrades[upgradeIndex]);
        thisButton.interactable = false;
    }
}
