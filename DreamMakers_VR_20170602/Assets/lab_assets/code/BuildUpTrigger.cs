using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUpTrigger : MonoBehaviour 
{
	public BuildUpManager manager;
	public GameObject particle;

	void OnTriggerEnter()
	{
		GetComponent<AudioSource> ().Play ();
		// manager.BuildUpLabReal ();
		//Destroy(transform.parent.gameObject,6f);
		GetComponent<Collider> ().enabled = false;
		GameObject particleI = Instantiate (particle, transform.position, Quaternion.identity);
		Destroy (particleI, 3f);
	}
}
