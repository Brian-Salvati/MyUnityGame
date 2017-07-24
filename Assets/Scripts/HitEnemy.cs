using UnityEngine;
using System.Collections;

public class HitEnemy : MonoBehaviour {

	public float killDamage;

	void OnTriggerEnter2D(Collider2D other){
		OstrichPlayerController player = this.GetComponentInParent<OstrichPlayerController> ();
			if (other.tag == "Enemy") {
				EnemyHealth killEnemy = other.gameObject.GetComponent<EnemyHealth> ();
				killEnemy.addDamage (killDamage);
				player.KilledEnemy();
			
	}
}

