using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour 
{
	public GameObject bookPanel;
	private Animation bookOpenAnim;
	public GameObject bookBlocker;

	private bool openState = false;
	// Use this for initialization
	void Start () 
	{
		bookPanel.SetActive (false);
		bookOpenAnim = bookPanel.GetComponent<Animation> ();
	}


	public void OnBook()
	{
		openState = !openState;
		if (openState) 
		{
			bookBlocker.SetActive (true);
			OpenBook ();
		}
		else
		{
			bookBlocker.SetActive (false);
			StartCoroutine (CloseBook());
		}
	}

	public void OpenBook()
	{
		GetComponent<AudioSource> ().Play ();
		bookPanel.SetActive (true);
		bookOpenAnim ["open"].speed = 1f;
		bookOpenAnim.Play ();
	}
		

	IEnumerator CloseBook()
	{
		GetComponent<AudioSource> ().Play ();
		bookOpenAnim ["open"].speed = -1f;
		bookOpenAnim ["open"].time = bookOpenAnim ["open"].length;
		bookOpenAnim.Play ();
		//yield return new WaitForSeconds (1.35f);
		yield return new WaitForSeconds (0.9f);
		bookPanel.SetActive (false);
	}
}
