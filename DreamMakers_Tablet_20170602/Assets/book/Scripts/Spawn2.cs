using UnityEngine;
using System.Collections;

public class Spawn2 : MonoBehaviour {


	public float maxAmount;
	public float growFactor;
	public GameObject cube;
	public int numberOfClouds;
	public int min, max;
	public float waitTime;

	void Start()
	{
		StartCoroutine(Scale());
	}

	IEnumerator Scale()
	{
		float timer = 0;

		while(true) // this could also be a condition indicating "alive or dead"
		{
			// we scale all axis, so they will have the same value, 
			// so we can work with a float instead of comparing vectors
			while(maxAmount < numberOfClouds)
			{
				timer += Time.deltaTime;
				transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
				PlaceClouds ();
				yield return null;
			}
			// reset the timer

			yield return new WaitForSeconds(waitTime);
			timer = 0;
		}
	}

	void PlaceClouds(){
			Instantiate (cube, GeneratedPosition (), Quaternion.identity);
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
}