using UnityEngine;
using System.Collections;

public class CloudScaler : MonoBehaviour {

	private float rate = 0.25f;
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 15);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localScale += Vector3.one * Time.deltaTime * rate;
	}
}
