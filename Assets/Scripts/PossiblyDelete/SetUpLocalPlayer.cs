using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (isLocalPlayer)
			GetComponent<OstrichPlayerController> ().enabled = true;
	}

}
