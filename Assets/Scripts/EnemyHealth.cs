using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float enemyMaxHealth;

	float currentHealth;

	public void addDamage(float damage){
		EnemyBirdController theBird = this.GetComponentInParent<EnemyBirdController>();
		if (theBird.vulnearble) {
			Debug.Log ("adding damage to enemy");
			currentHealth -= damage;
			if (currentHealth <= 0)
				makeDead ();
		}
	}

	void makeDead(){
		EnemyBirdController theBird = this.GetComponentInParent<EnemyBirdController>();
		if (theBird.vulnearble) {
			Debug.Log ("enemy killed");
			Destroy (gameObject);
		}
	}
}
