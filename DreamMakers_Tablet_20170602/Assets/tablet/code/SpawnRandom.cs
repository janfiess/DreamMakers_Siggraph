using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRandom : MonoBehaviour {

	private int curAmount = 0;
	public int maxAmount = 10;
	public float spawnRateSeconds = 2f;
	public float initWait = 4f;
	public List<GameObject> prefabs = new List<GameObject> ();
	public Transform SpawnPointParent; 


	private List<Vector3> SpawnPoints = new List<Vector3>();
	private Vector3 spawnPos;
	private int childCount;

	// Use this for initialization
	void Start () 
	{
		childCount = SpawnPointParent.childCount;
		for (int i = 0; i < childCount-1; i++) 
		{
			SpawnPoints.Add (SpawnPointParent.GetChild (i).position);
			//print (SpawnPointParent.GetChild (i).position);
		}
		for (int i = 0; i < prefabs.Count ; i++) 
		{
			//CreateObject (prefabs[i]);
		}
		//StartCoroutine (SpawnLoop());
	}

	public Vector3 GetRandomPos()
	{
		//Vector3 screenPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), 0));
		spawnPos = SpawnPoints[Random.Range(0,childCount-1)];
		return spawnPos;
	}

	public GameObject GetRandomPrefab()
	{
		GameObject prefab = prefabs [Random.Range (0, prefabs.Count)];
		return prefab;
	}

	public void MinusCurAmount()
	{
		curAmount--;
	}

	private void CreateObject(GameObject prefab)
	{
		GameObject newCloud = Instantiate (prefab, GetRandomPos (), Quaternion.identity) as GameObject;
		newCloud.GetComponent<drop> ().creator = this;
		newCloud.transform.SetParent (SpawnPointParent);
		newCloud.transform.localScale = Vector3.one;
		newCloud.transform.localEulerAngles = Vector3.zero;
		curAmount++;
	}

	public void StartSpawnLoop()
	{
		StartCoroutine (SpawnLoop());
	}

	public void EndSpawnLoop()
	{
		StopAllCoroutines ();
		//StopCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop()
	{
		yield return new WaitForSeconds (initWait);
		while (true) 
		{
			if (curAmount < maxAmount) 
			{
				CreateObject (GetRandomPrefab());
				/*
				https://forum.unity3d.com/threads/randomly-generate-objects-inside-of-a-box.95088/
				Vector3 rndPosWithin;
				rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
				rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
				Instantiate(cloudPrefab, rndPosWithin,  Quaternion.identity);
				*/

			}
			yield return new WaitForSeconds (spawnRateSeconds);
		}
	}
}
