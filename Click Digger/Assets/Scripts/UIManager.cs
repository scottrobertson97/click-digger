using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject clickPanelPrefab;
	public GameObject content;
	public GameObject gpsText;
	public GameObject goldText;
	public GameObject clickText;


	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		gpsText.GetComponent<Text>().text = gameManager.GoldPerSecond + " Gps";
		goldText.GetComponent<Text> ().text = gameManager.GoldDisplayed + " Gold";
		clickText.GetComponent<Text> ().text = "+" + gameManager.ClickMultiplier + " Gold";
	}

	/// <summary>
	/// Delete all the panels, and create new ones
	/// </summary>
	public void Init(){
		foreach (Transform panel in content.transform) {
			GameObject.Destroy(panel.gameObject);
		}

		foreach (string type in gameManager.Miners.Keys) {
			GameObject panel = Instantiate (clickPanelPrefab, content.transform);
			panel.GetComponent<ClickPanel> ().Initialize (type);
		}
	}
}
