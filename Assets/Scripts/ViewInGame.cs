using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour {

	public int player1lives = 3;
	public int player2lives = 3;
	public Text player1;
	public Text player2;
	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text waveLabel;

	// Update is called once per frame
	void Update () {
		player1.text = GameManager.instance.GetP1Lives ();
		player2.text = GameManager.instance.GetP2Lives ();
		player1ScoreText.text = GameManager.instance.GetP1Score();
		player2ScoreText.text = GameManager.instance.GetP2Score();
		waveLabel.text = EnemySpawner.instance.GetWave ();
	}
}
