using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour 
{
	public int cagesCount;
	public int curHitCound = 0;
	public GameObject hitText;
	public GameObject morpheus;
	public AudioSource  audioS;
	public AudioClip soundWin;
	public AudioClip soundGangnam;

	// Use this for initialization
	void Start () 
	{
		//audioS = GetComponent<AudioSource> ();
	}

	public void OnCageHit()
	{
		curHitCound++;
		hitText.SetActive (true);
		hitText.GetComponent <TextMesh> ().text = "Hit " + curHitCound + " of " + cagesCount + " flying Cages";
		audioS.loop = false;
		audioS.clip = soundWin;
		audioS.Play ();

		if (curHitCound == cagesCount) 
		{
			audioS.loop = true;
			audioS.clip = soundGangnam;
			audioS.Play ();
			print ("GangnamStyle");
			morpheus.GetComponent<BuildUp>().StartBuildUp ();
			//morpheus.GetComponent<AudioSource> ().Play ();
			StartCoroutine (RemoveMorpheus ());
		}
	}

	IEnumerator RemoveMorpheus()
	{
		yield return new WaitForSeconds (20);
		audioS.Stop ();
		Destroy (morpheus);
	}
}
