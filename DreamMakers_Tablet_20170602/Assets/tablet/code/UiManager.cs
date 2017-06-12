using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour 
{
	private int screen2Completion = 0;
	public GameObject FrameOutline;

	public void SetScreen2Completion()
	{
		StartCoroutine(SetScreen2CompletionRoutine());
	}
	public IEnumerator SetScreen2CompletionRoutine()
	{
		screen2Completion++;
		if (screen2Completion == 2) 
		{
			//FrameOutline.GetComponent<Animation> ().Play ();
			GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds(0f);
			SceneManager.LoadScene ("tabletView");
		}
		yield return new WaitForEndOfFrame();
	}
}
