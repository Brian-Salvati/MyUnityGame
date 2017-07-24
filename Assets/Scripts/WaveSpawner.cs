using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
	public static WaveSpawner instance;

	public Text waveLabel;

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave{
		public string name;
		public Transform enemy; 
		public int count;
		public float rate; 

	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	public SpawnState state = SpawnState.COUNTING;

	// Use this for initialization
	void Start () {
		waveCountdown = timeBetweenWaves;
	}

	// Update is called once per frame
	void Update () {
		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				//Start spawning wave
				StartCoroutine(SpawnWave (waves[nextWave]));
			}
		} else {
			waveCountdown -= Time.deltaTime;
		}
	
	}

	IEnumerator SpawnWave(Wave _wave){
		state = SpawnState.SPAWNING;

		//Spawn
		for (int i = 0; i < _wave.count; i++) {
			SpawnEnemy (_wave.enemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Debug.Log ("Spawning Enemy: " + _enemy.name);
	}
}
