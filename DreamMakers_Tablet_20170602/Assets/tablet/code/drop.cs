using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

	[HideInInspector]
	public SpawnRandom creator;

	public void Harakiri()
	{
		creator.MinusCurAmount ();
		Destroy (gameObject);
	}
}
