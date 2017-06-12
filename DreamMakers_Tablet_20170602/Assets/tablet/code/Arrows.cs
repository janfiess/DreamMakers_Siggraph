using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {

	public GameObject arrow;
	// Use this for initialization

	public void OnClicked()
	{
		Destroy (arrow);
	}

}
