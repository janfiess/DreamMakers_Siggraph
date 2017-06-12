using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class itemSpawn : MonoBehaviour {

	public List<GameObject> prefabs = new List<GameObject> ();
	public List<Transform> spawnP = new List<Transform> ();
	public List<GameObject> itemInstances = new List<GameObject> ();
	public Transform SpawnPointParent; 
	public AudioClip dopSound;
	private AudioSource aSource;
	private bool spawnState = true;

	// Use this for initialization
	void Start () 
	{
		aSource = GetComponent<AudioSource> ();
		StartCoroutine (InitSpawnLoop());
	}

	public void SetDraggableStateItems(bool state)
	{
		//for loop
		spawnState = state;
		for (int i = 0; i < itemInstances.Count; i++) 
		{
			itemInstances [i].GetComponent<UiDrag>().SetDraggableState (state);
		}
	}

	public void RemoveItemInstance(GameObject item)
	{
		itemInstances.Remove (item);
	}

	public void SpawnItem(int itemIndex)
	{
		StartCoroutine (Spawn(itemIndex, 1f));
	}

	public void SpawnItem(int itemIndex, float time)
	{
		StartCoroutine (Spawn(itemIndex, time));
	}

	public void PlaySound()
	{
		aSource.PlayOneShot (dopSound);
	}

	IEnumerator Spawn(int itemIndex, float time)
	{
		//yield return new WaitForSeconds (time);
		if (time > 0.1f) 
		{
			yield return new WaitForSeconds (2f);
		}
		yield return new WaitForEndOfFrame();
		GameObject newItem = Instantiate (prefabs[itemIndex], spawnP[itemIndex].position, Quaternion.identity) as GameObject;
		itemInstances.Add (newItem);
		newItem.GetComponent<UiDrag> ().m_active = spawnState;
		newItem.GetComponent<itemDrop> ().myCreator = this;
		newItem.GetComponent<itemDrop> ().itemIndex = itemIndex;
		newItem.transform.SetParent (SpawnPointParent);
		newItem.transform.localScale = Vector3.one;
		newItem.transform.localEulerAngles = Vector3.zero;

	}

	IEnumerator InitSpawnLoop()
	{
		for (int i = 0; i < prefabs.Count; i++)
		{
			SpawnItem(i, 0f);
			yield return new WaitForSeconds (0.25f);
		}
	}
}
