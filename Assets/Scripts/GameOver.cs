using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public int player1lives = 3;
	public int player2lives = 3;
	bool gameOver = false;
	public Text player1;
	public Text player2;

	void Start(){
		StartCoroutine(BlinkText());
	}

	void Update(){
		//Only update text in background while game isn't over
		//Once game is over, text shouldn't be updated
		//Only start blinking the text once the gamestate is changed
		//only call BlinkText() once.
		if (!gameOver) {
			player1.text = GameManager.instance.GetP1Lives ();
			player2.text = GameManager.instance.GetP2Lives ();
		}
	}

	IEnumerator BlinkText()
	{
		while (GameManager.instance.currentGameState == GameState.gameOver) {
			player1.enabled = false;
			player2.enabled = false;
			yield return new WaitForSeconds (0.5f);
			player1.enabled = true;
			player2.enabled = true;
			yield return new WaitForSeconds (0.5f);
		}
	}
}
