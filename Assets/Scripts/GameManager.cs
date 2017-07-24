using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public enum GameState{
	menu,
	instructions,
	inGame,
	gameOver
}

public class GameManager: MonoBehaviour {

	public int collectedEggs = 0;
	public GameObject currentCheckPoint;
	private OstrichPlayerController player;
	public int player1lives = 3;
	public int player2lives = 3;
	public int player1score = 0;
	public int player2score = 0;

	public static GameManager instance;
	public GameState currentGameState;

	public Canvas menuCanvas, instructionsCanvas, inGameCanvas, gameOverCanvas;


	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		currentGameState = GameState.menu;
		player = FindObjectOfType<OstrichPlayerController> ();
		

		Scene currScene = SceneManager.GetActiveScene ();
		Debug.Log (currScene.name);
		if (currScene.name == "RoboticScene") 
		{
			SetGameState (GameState.inGame);
			Debug.Log ("ingame");
		}
	}

	void Update(){
		if (player1lives == 0 || player2lives == 0) {
			SetGameState (GameState.gameOver);
		}
	}

	public void StartRobotWorld () {
		SceneManager.LoadScene ("RoboticScene");
	}

	public void BackToMenu () {
		SetGameState (GameState.menu);
		SceneManager.LoadScene ("MainScene");
	}

	public void Instructions() {
		SetGameState(GameState.instructions);
	}

	void SetGameState (GameState newGameState) {
		
		currentGameState = newGameState;
		if (newGameState == GameState.menu) {
			//setup Unity scene for menu state
			menuCanvas.enabled = true;
			instructionsCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
		} else if (newGameState == GameState.instructions) {
			//setup Unity scene for inGame state
			menuCanvas.enabled = false;
			instructionsCanvas.enabled = true;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
		} else if (newGameState == GameState.inGame) {
			menuCanvas.enabled = false;
			instructionsCanvas.enabled = false;
			inGameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
		} else if (newGameState == GameState.gameOver){
			menuCanvas.enabled = false;
			instructionsCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
		}
			
	}

	public void CollectedEgg() {
		collectedEggs += 1000;
	}

	public void RespawnPlayer(){
		Debug.Log ("Player Respawn");
		player.transform.position = currentCheckPoint.transform.position;

	}


	public void ReduceLives(int whichPlayer)
	{
		if (whichPlayer == 1) {
			player1lives--;
		} else {
			player2lives--;
		}

	}
	
	public void IncreaseScore(int whichPlayer, int amtIncrease){
		if (whichPlayer == 1){
			player1score += amtIncrease;
		}else{
			player2score += amtIncrease;
		}
	}
	

	public string GetP1Lives()
	{
		return player1lives.ToString();
	}
	public string GetP2Lives()
	{
		return player2lives.ToString();
	}
	
	public string GetP1Score()
	{
		return player1score.ToString();
	}
	
	public string GetP2Score()
	{
		return player2score.ToString();
	}
}

