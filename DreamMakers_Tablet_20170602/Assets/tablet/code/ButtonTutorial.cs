using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTutorial : MonoBehaviour {
	public SpawnRandom cloudManager;
	public GameObject button;
	public List<GameObject> pages = new List<GameObject> ();
	private int curPage = 0;

	private AudioSource audioS;

	// Use this for initialization
	void Start () 
	{
		audioS = GetComponent<AudioSource> ();
		InitTutorial (4);
	}

	void InitTutorial(float wait)
	{
		curPage = 0;
		for (int i = 0; i < pages.Count; i++) 
		{
			pages [i].SetActive (false);
		}
		StartCoroutine (BlendIn (wait));
		button.SetActive (false);
	}


	public void NextPage()
	{
		audioS.Play ();
		if (curPage < pages.Count) 
		{
			if (curPage != 0) 
			{
				pages [curPage - 1].SetActive (false);
			}
			pages [curPage].SetActive (true);
			curPage++;
		}
		else 
		{
			// activate cloud spawning
			cloudManager.StartSpawnLoop();
			button.SetActive (false);
			pages [curPage - 1].SetActive (false);
		}
	}

	public void RestartTutorial()
	{
		InitTutorial (0);
		cloudManager.EndSpawnLoop ();
		print ("RestartTutorial");
	}

	IEnumerator BlendIn(float wait)
	{
		yield return new WaitForSeconds(wait);
		button.SetActive (true);
		NextPage ();
	}
}
