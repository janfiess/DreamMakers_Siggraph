using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static Dictionary<string, GameObject> items = new Dictionary<string, GameObject> ();
	public static int itemNumber = 0;
	public GameObject world;


	// Text
	public Text statusText;
	public Text scenarioStopwatchText;
	public Text playStopwatchText;
	public Text healthText;
	public Text dreamCounterText;
	public Text levelCounterText;
	public Text spawnableItemsCounterText;
	public Button changePlayerButton;


	[HideInInspector]
	public bool withinScenarioTime = false;

	[HideInInspector]
	public bool withinPlayTime = false;

	[HideInInspector]
	public int totalLevelNumber = 2;  // the game has 2 levels
	int dreamCounter = 0;
	int currentLevelNumber = 0; // are you in level 0, 1 or ... ?
	[HideInInspector]
	public int health = 3; // every play session has 3 lifes
	
	public int maxScenarioTime = 40; // in seconds
	[HideInInspector]
	public int maxPlayTime = 90; // in seconds
	[HideInInspector]
	public int remainingScenarioTime; // in seconds
	[HideInInspector]
	public int remainingPlayTime; // in seconds
	public itemSpawn uiIcons;
	public NetworkView networkView;
	[HideInInspector]
	public int totalNumberOfSpawnableitemsPerScenario = 1000000;
	[HideInInspector]
	public int spawnableItemsCounter;




	void Awake(){
		networkView = GetComponent<NetworkView>();
		world = GameObject.Find ("World");
	}




	void Start(){
//		uiIcons.SetDraggableStateItems (false); // ui-Icons not draggable when not withinScenarioTime

		GetComponent<NetworkingManager> ().StartClient ();
		healthText.text = "Health: " + health.ToString();
		levelCounterText.text = "Level " + (currentLevelNumber+1).ToString();
		dreamCounterText.text = "Total dreams: " + dreamCounter.ToString();
		Button uiChangePlayerButton = changePlayerButton.GetComponent<Button>();
		uiChangePlayerButton.onClick.AddListener(OnClickChangePlayerButton);

		spawnableItemsCounter = totalNumberOfSpawnableitemsPerScenario;
	}

	public void PrepareScenario(){ // when  the envelope rises in VR
		remainingScenarioTime = maxScenarioTime;
		spawnableItemsCounter = totalNumberOfSpawnableitemsPerScenario;
	
		scenarioStopwatchText.color = new Color (scenarioStopwatchText.color.r, scenarioStopwatchText.color.g, scenarioStopwatchText.color.b, 1);
	
		StartCoroutine(FadeInText (remainingScenarioTime.ToString (), scenarioStopwatchText));
		ShowStatusMessage ("Game starts when the VR player opens the envelope");

	}

	[ContextMenu("StartScenario")]
	public void StartScenario(){ // called when opened envelope in vR
		if (withinPlayTime == false) {
			print ("Start Game");

			withinPlayTime = true;
			playStopwatchText.color = new Color(playStopwatchText.color.r, playStopwatchText.color.g, playStopwatchText.color.b, 1);
			StartCoroutine (PlayTimer (remainingPlayTime));
			foreach (Transform worldItem in world.transform) {
				Destroy (worldItem.gameObject);
			}
		}


		spawnableItemsCounter = totalNumberOfSpawnableitemsPerScenario;
		spawnableItemsCounterText.text = spawnableItemsCounter.ToString() + " items left";

		withinScenarioTime = true;
		StartCoroutine(ScenarioTimer(remainingScenarioTime));

	}

	public void MakeUiItemsDraggable(bool draggable){
		uiIcons.SetDraggableStateItems (draggable);
	}


	public void EnterScenarioAction(){
		// Enable drag & drop
		print("within scenario time");
		ShowStatusMessage ("Game started");
		uiIcons.SetDraggableStateItems (true); // ui-Icons only draggable when not withinScenarioTime
		foreach (Transform worldItem in world.transform) {
			Destroy (worldItem.gameObject);
		}
		spawnableItemsCounter = totalNumberOfSpawnableitemsPerScenario;
	}

	public void ExitScenarioAction(){
		// Disable drag & drop
		FadeoutScenarioStopwatch();
		print("scenario time expired");
		ShowStatusMessage ("Game ended");
		withinScenarioTime = false;
		uiIcons.SetDraggableStateItems (false); // ui-Icons not draggable when not withinScenarioTime
		foreach (Transform worldItem in world.transform) {
			Destroy (worldItem.gameObject);
		}
	}




	IEnumerator ScenarioTimer(int remainingScenarioTime){
		while (remainingScenarioTime > 0) {
			if (withinScenarioTime == true) {
				scenarioStopwatchText.text = remainingScenarioTime.ToString ();
				yield return new WaitForSeconds (1);
				remainingScenarioTime--;

				if (remainingScenarioTime == 15) {
					StartCoroutine (ShowTextForSeconds ("15 seconds left for this combination", statusText, 2.0f));
				}
			} else {
				yield break;
			}
		}
		if (remainingScenarioTime == 0) {
			withinScenarioTime = false;
			statusText.text = "Game time over";
			scenarioStopwatchText.text = remainingScenarioTime.ToString();
		}
	} 




	IEnumerator PlayTimer(int remainingPlayTime){

		//		stopwatch.text = remainingTime.ToString ();
		while (remainingPlayTime > 0) {
			if (withinPlayTime == true) {
				playStopwatchText.text = remainingPlayTime.ToString ();
				yield return new WaitForSeconds (1);
				remainingPlayTime--;
				//				stopwatch.text = remainingTime.ToString ();
				if (remainingPlayTime == 40) {
					print ("The game ends in 40 seconds");
				}
			} else {
				yield break;
			}


			if (remainingPlayTime == 0) {
				yield return new WaitForSeconds(3.0f);
				withinPlayTime = false;
				withinScenarioTime = false;
				playStopwatchText.text = "";
				FadeoutScenarioStopwatch();
				withinPlayTime = false;
				playStopwatchText.color = new Color(playStopwatchText.color.r, playStopwatchText.color.g, playStopwatchText.color.b, 1);
			
				
			}
		}
	}






	void OnClickChangePlayerButton(){
		print ("ChangePlayerButton clicked");
		networkView.RPC ("ChangePlayer", RPCMode.All);
		SceneManager.LoadScene (0);
	}








	// Text Fading

	// FadeText in and out START

	IEnumerator ShowTextForSeconds(string text, Text targetText,float displayTime){
		statusText.text = text;

		float duration = 1.0f;
		// Make transparent, but maintain r, g, b
		Color prevColor = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 1.0f, t);
			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
			targetText.color = newColor;
			yield return null;
		}

		Color fullOpacity = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 1);
		targetText.color = fullOpacity;

		yield return new WaitForSeconds (displayTime);
		StartCoroutine(FadeOutText(targetText));
	}

	IEnumerator FadeOutText(Text targetText){
		float duration = 0.8f;
		Color prevColor = targetText.color;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 0.0f, t);

			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);;
			targetText.color = newColor;
			yield return null;
		}
		Color fullTransparency = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);
		targetText.color = fullTransparency;
		targetText.text = "";
	}

	IEnumerator WaitThenFadeOutText(Text targetText){
		yield return new WaitForSeconds (2.0f);
		float duration = 0.8f;
		Color prevColor = targetText.color;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 0.0f, t);

			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
			targetText.color = newColor;
			yield return null;
		}
		Color fullTransparency = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);
		targetText.color = fullTransparency;
		targetText.text = "";
	}

	IEnumerator FadeInText(string text, Text targetText){
		targetText.text = text;

		float duration = 1.0f;
		// Make transparent, but maintain r, g, b
		Color prevColor = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0);

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 1.0f, t);
			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
			targetText.color = newColor;
			yield return null;
		}

		Color fullOpacity = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 1);
		targetText.color = fullOpacity;
	}

	// FadeText in and out END

	public void FadeoutScenarioStopwatch(){
		StartCoroutine (FadeOutText (scenarioStopwatchText));
		StartCoroutine (FadeOutText (spawnableItemsCounterText));
	}

	public void ShowStatusMessage(string message){
		StartCoroutine (ShowTextForSeconds(message, statusText,3.0f));
	}

}
