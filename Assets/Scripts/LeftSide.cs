using UnityEngine;
using System.Collections;

public class LeftSide : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		//if (other.tag == "PlayerOstrich") {
			other.gameObject.transform.position = new Vector3 (3.1f, other.gameObject.transform.position.y, -0.5f);
		//}
	} 

}
