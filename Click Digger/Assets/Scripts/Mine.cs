using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {
	private Dictionary<string, int> minersCount;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		minersCount = new Dictionary<string, int> ();
		foreach (string type in gameManager.minersGPS.Keys) {
			minersCount.Add (type, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
