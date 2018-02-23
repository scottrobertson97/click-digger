using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private double goldEarned = 0;
	private double gold = 0;
	private int goldDisplayed = 0;
	private double goldPerSecond = 1;
	private List<Mine> mines;

	public Dictionary<string, double> minersGPS = new Dictionary<string, double>{
		{"basic", 1},
		{"advanced", 2}
	};
	// Use this for initialization
	void Start () {
		mines = new List<Mine> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Earn(double earned){
		
	}
}
