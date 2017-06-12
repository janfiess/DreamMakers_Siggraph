using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour {

	ParticleSystem particleSystem;
	//ParticleSystem.EmissionModule p_emission; 
	//ParticleSystem.MainModule p_main;


	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
		//p_main = GetComponent<ParticleSystem>().main;
		//p_emission = GetComponent<ParticleSystem>().emission;

	}

	void Update () {
		
	}

	public void triggerParticles(){
		// p_emission.enabled = true;
		particleSystem.Play();
		StartCoroutine (StopParticles ());
	}


	IEnumerator StopParticles(){
		yield return new WaitForSeconds (1.5f);
		//p_emission.enabled = false;
	}
}
