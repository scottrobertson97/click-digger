using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject clickPanelPrefab;
	public GameObject content;
	//gold per second text
	public GameObject gpsText;
	//displayed gold
	public GameObject goldText;
	//text on click button
	public GameObject clickText;


	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		for (int i = 0; i < gameManager.Miners.Count; i++) {
			Instantiate (clickPanelPrefab, content.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//
		gpsText.GetComponent<Text>().text = gameManager.GoldPerSecond + " Gps";
		goldText.GetComponent<Text> ().text = gameManager.GoldDisplayed + " Gold";
		clickText.GetComponent<Text> ().text = "+" + gameManager.ClickMultiplier + " Gold";
	}
}
