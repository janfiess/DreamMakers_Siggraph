// attached to Manager_gobal gameobject which is always available in the scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Manager : MonoBehaviour {

	[ReadOnly] public bool isTutorial = true; // also watched by the controllers: when isTutorial == false: Tooltips get destroyed
	
	public GameObject tutorialScene;
	public GameObject mainGameScene;
	public GameObject tutorialAssets;
	public GameObject gameAssets;

    

	// Lab will already be rised in Tutorial_GameplayManager.cs: FinalCombinationOfScenarioSucceded();
	// Then the MainGame starts with the envelope with a delay of a couple of seconds with EnterMainGame();
	public void EnterMainGame(){
		print("EnterMainGame()");
		isTutorial = false;
		
		tutorialAssets.SetActive(false);
		gameAssets.SetActive(true);
		tutorialScene.SetActive(false);
		mainGameScene.SetActive(true);

		
	}











	public void ResetGameData(){
	}


	public bool tabletConnected;

}
