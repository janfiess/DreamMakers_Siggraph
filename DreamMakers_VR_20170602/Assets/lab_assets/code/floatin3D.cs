using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatin3D : MonoBehaviour 
{

	public float frequency = 10f;
	public float amplitude = 0.5f;
	public float offset = 0.0f;
	public float seed = 0.5f;

	private Vector3 startPos;
	private Vector3 noisePos;

	// Use this for initialization
	void Start () 
	{
		startPos = transform.localPosition;
		////// temp //////

		//Destroy (this);

		////// temp //////
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		MyNoise ();
	}

	void MyNoise()
	{
		noisePos = new Vector3(Mathf.PerlinNoise(0, (offset + Time.fixedTime + seed + 0.235f) *Time.deltaTime * frequency) * amplitude, Mathf.PerlinNoise(0, (offset + Time.fixedTime + seed + 0.612f)*Time.deltaTime * frequency) * amplitude, Mathf.PerlinNoise(0, (offset + Time.fixedTime+ seed + 0.425f) *Time.deltaTime * frequency) * amplitude);
		//Vector3 nextPos = new Vector3 (Mathf.PerlinNoise (0, offset + Time.fixedTime * Time.deltaTime * frequency) * amplitude, 0, Mathf.PerlinNoise (0, offset + seed + Time.fixedTime * Time.deltaTime * frequency) * amplitude);
		transform.localPosition = startPos + noisePos;


	}
}
