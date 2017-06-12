using UnityEngine;
using System.Collections;

public class SpawnClouds : MonoBehaviour {

	public GameObject cube;
	public int numberOfClouds;
	public int min, max;
	public float waitTime;

	// Use this for initialization
	void Start () {
		PlaceClouds ();
	}

	void PlaceClouds(){
		for (int i = 0; i < numberOfClouds; i++) {
			Instantiate (cube, GeneratedPosition (), Quaternion.identity);

		}
	}
	Vector3 GeneratedPosition()
	{
		int x, y, z;
		x=UnityEngine.Random.Range(min, max);
		y=UnityEngine.Random.Range(min, max);
		z=UnityEngine.Random.Range(min, max);
		return new Vector3 (x, y, z);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
