using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		KillMe ();	
	}


	void KillMe()
	{
		Destroy (gameObject,10f);
	}
}
