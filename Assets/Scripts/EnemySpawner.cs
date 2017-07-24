using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance;
	public GameObject EnemyGO;
	int waveNum = 0;
	float maxSpawnRate = 8f;
	private double time = 0;
	private float minYForSpawn = 227f;

	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		Invoke ("SpawnEnemy", maxSpawnRate);

		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.currentGameState == GameState.gameOver) {
			StopAllCoroutines ();
		}
	}

	//Function to spawn an enemy
	public void SpawnEnemy()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject anEnemy = (GameObject)Instantiate (EnemyGO);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), Random.Range (minYForSpawn, max.y));

		ScheduleNextEnemySpawn ();


	}

	void ScheduleNextEnemySpawn(){
		float spawnInNSec;

		if (maxSpawnRate > 1f) {

			spawnInNSec = Random.Range (maxSpawnRate-5f, maxSpawnRate);

		} else
			spawnInNSec = 1f;

		Invoke ("SpawnEnemy", spawnInNSec);
	}

	void IncreaseSpawnRate()
	{
		waveNum++;
		if (maxSpawnRate > 1f)
			maxSpawnRate--;
		if (maxSpawnRate == 1f)
			CancelInvoke("IncreaseSpawnRate");
		Debug.Log ("Changed spawn rate to: " + maxSpawnRate);
	}

	public string GetWave(){
		return waveNum.ToString ();
	}
}
