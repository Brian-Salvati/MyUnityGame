using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
		gameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			gameManager.RespawnPlayer ();
		}
	}


}
