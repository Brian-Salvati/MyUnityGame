using UnityEngine;
using System.Collections;

public class VulnerableSpotOnPlayer : MonoBehaviour {

	//kill player if bird hits the vulnerable spot on PlayerOstritch
	void OnTriggerEnter2D(Collider2D other)
	{
		OstrichPlayerController thisController = this.GetComponentInParent<OstrichPlayerController> ();
		if (thisController.vulnerable) {
			if (other.tag == "Enemy") {
				Debug.Log ("Vulnerable spot hit by enemy");
				thisController.vulnerable = false;
				//Move bird outside of collider
				EnemyBirdController bird = other.gameObject.GetComponent<EnemyBirdController> ();
				bird.Flap ();
				bird.Flap ();
				//Deduct life from player
				StartCoroutine (thisController.Hit ());

			}
			if (other.tag == "VulnerableSpot") {
				Debug.Log ("Vulnerable spot hit by other player");
				thisController.vulnerable = false;

				//Deduct life from hit player
				StartCoroutine (thisController.Hit ());

			}
		}
	}

}
