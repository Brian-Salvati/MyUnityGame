using UnityEngine;
using System.Collections;

public class enemyShoot : MonoBehaviour {

	public GameObject projectile;
	public float shootTime;
	public Transform shootFrom;
	public int chanceShoot;

	public AudioSource shootSound;

	float nextShootTime;

	// Use this for initialization
	void Start () {
		nextShootTime = 0f;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" && nextShootTime < Time.time) {
			nextShootTime = Time.time + shootTime;
			if (Random.Range (0, 10) >= chanceShoot) {
				Instantiate (projectile, shootFrom.position, Quaternion.identity);
				shootSound.Play ();
			}
		}
	}
}
