using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPageSound : MonoBehaviour {

	private AudioSource audioS;

	// Use this for initialization
	void Start () 
	{
		audioS = GetComponent<AudioSource> ();	
	}
	
	public void OnPageTurn()
	{
		audioS.Play ();
	}
}
