using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public float fireSpeed;
	Rigidbody2D firerb;

	bool isCollected = false;

	public AudioSource collectedSound;



	void Awake () {
		firerb = GetComponent<Rigidbody2D> ();

		//Add force so that the project moves
		firerb.AddForce (new Vector2 (1, 0) * fireSpeed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Show(){
		this.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponent<CircleCollider2D>().enabled = true;
		isCollected = false;
	}

	void Hide() {
		this.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<CircleCollider2D>().enabled = false;
	}

	void Collect(){
		isCollected = true;
		Hide ();
		collectedSound.Play ();
		GameManager.instance.CollectedEgg();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			OstrichPlayerController thisPlayer = other.GetComponent<OstrichPlayerController>();
			Collect ();
			thisPlayer.CollectedEgg();
		}
	}
}
