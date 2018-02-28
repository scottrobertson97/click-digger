using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
	public GameObject clickPanelPrefab;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Buy(){
		//
		GameObject button = EventSystem.current.currentSelectedGameObject;
		string type = button.transform.parent.gameObject.name;
		//gameManager.GetCurrentMine.Buy (type);
	}
}
