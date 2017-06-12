// attached to the Manager GameObject which is always avalable in scene
// This script triggers the animation when the envelope appears. 
// The envelope can be opened in the script Tutorial_OpenEnvelope

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_EnvelopeManager : MonoBehaviour {

	public Transform envelope_placeholder;
	public GameObject envelope_prefab;
	public Global_Feedback feedbackManager;
	[HideInInspector] public GameObject envelope_instance;
	public float envelopeRise_StartTime = 4.0f;
	// void Start () {
	// 	Invoke("RiseEnvelope", envelopeRise_StartTime);
	// }
	
	// Load Envelope-Prefab and attach to placeholder object
	// called in Tutorial_Gameplay.cs - PrepareScenario()
	public void RiseEnvelope(string name_finalObject){
		envelope_instance = Instantiate(envelope_prefab);
		envelope_instance.transform.parent = envelope_placeholder;
		LoadLetter(name_finalObject);		
	}


	// called in Tutorial_Gameplay.cs - PrepareScenario()
	public void RiseEnvelopeWithDelay_trigger(string name_finalObject){
		StartCoroutine(RiseEnvelopeWithDelay(name_finalObject));
	}
	// Load Envelope-Prefab and attach to placeholder object
	IEnumerator RiseEnvelopeWithDelay(string name_finalObject){
		yield return new WaitForSeconds(envelopeRise_StartTime);
		envelope_instance = Instantiate(envelope_prefab);
		envelope_instance.transform.parent = envelope_placeholder;
		LoadLetter(name_finalObject);
	}

// attach the right texture to the letter
	 void LoadLetter(string name_finalObject){
		 feedbackManager.OnRiseEnvelope();
		  Texture letter_texture = Resources.Load("Textures/Letter/Letter_" + name_finalObject, typeof(Texture)) as Texture;
     

        // apply texture to the letter in the envelope;
        GameObject letter = envelope_instance.transform.GetChild(2).gameObject;
        letter.GetComponent<Renderer>().material.mainTexture = letter_texture;
	}
}
