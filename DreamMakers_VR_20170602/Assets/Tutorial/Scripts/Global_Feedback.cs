
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using VRTK;

	public class Global_Feedback : MonoBehaviour {
		// NetworkView networkView;
		public Game_Text textManager;
		Game_GameplayManager gameplay;
		public GameObject leftController, rightController;
		VRTK_ControllerActions rightControllerActions;
		VRTK_ControllerActions leftControllerActions;
		private AudioSource audioS;
		public AudioSource musicS;
		public DmxConfigurator dmx; // rein

		private int introVal = 0;

		public List<AudioClip> sounds = new List<AudioClip>();



		void Awake()
		{
			audioS = GetComponent<AudioSource> ();
			leftControllerActions = leftController.GetComponent<VRTK_ControllerActions>();
			rightControllerActions = rightController.GetComponent<VRTK_ControllerActions>();
		}

		void Start () {
			gameplay = GetComponent<Game_GameplayManager>();
	

		}

		void Update(){
			if (Input.GetKeyDown (KeyCode.K)) {
				StartCoroutine(OnWrongCombination_ControllerRumble()); 
			}
		}
		
		public void OnEnterTutorial(){
			print("Feedback: OnEnterTutorial");
			musicS.Stop ();
			musicS.clip = sounds [7];
			musicS.Play ();
			//musicS.PlayOneShot (sounds[0]);
			dmx.DMX_TutorialIdle();

		}
		
		// from Global_NetworkingManager and Tutorial_Gameplay: When the tablet spawns a new item
		public void OnSpawnNewItem(){
			print("Feedback: OnSpawnNewItem");
			dmx.DMX_Spawn (); // rein
		}

		// called in BuiltUpManager
		public void OnRiseLab(){
			print("Feedback: OnRiseLab");
			dmx.DMX_StartGame (); // rein
			musicS.Stop ();
			musicS.PlayOneShot (sounds[0]);
			introVal++;
			StopCoroutine ("WaitForIntroEnd");
			StartCoroutine (WaitForIntroEnd (introVal));

		}

		IEnumerator WaitForIntroEnd(int curIntro)
		{
			yield return new WaitForSeconds (25f);
			print (musicS.clip);
			if (introVal == curIntro) 
			{
				musicS.clip = sounds [1];
				musicS.Play ();
				if (musicS.clip != sounds [1]) 
				{
					musicS.clip = sounds [1];
					musicS.Play ();
				}
			}
		}
			
		// called in Game_GameplayManager.cs
		public void OnCorrectCombination(){ 
			print("Feedback: OnCorrectCombination");
			dmx.DMX_RightCombination (); // rein
			

			audioS.clip = sounds [4];
			audioS.Play();
		}

		// called in Game_Combine.cs
		public void OnWrongCombination(){
			print("Feedback: OnWrongCombination");
			dmx.DMX_WrongCombination ();
			StartCoroutine(OnWrongCombination_ControllerRumble()); 





			// vibration feedback on the controller which holds the wrong item

			// var thisTransformItem = item.GetComponent<TransformItem> ();
			// if (thisTransformItem.controllerEvents) {
			// 	var power = thisTransformItem.controllerEvents.GetTriggerAxis ();
			// 	thisTransformItem.controllerActions.TriggerHapticPulse (power * 0.5f, 0.1f, 0.01f);
			// }

		}

		IEnumerator OnWrongCombination_ControllerRumble(){
			if(leftController.active) rightControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			if(rightController.active) leftControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			yield return new WaitForSeconds(0.3f);
			if(leftController.active)rightControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			if(rightController.active)leftControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			yield return new WaitForSeconds(0.3f);
			if(leftController.active)rightControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			if(rightController.active)leftControllerActions.TriggerHapticPulse(0.93f, 0.05f, 0.01f);
			yield return null;
		}

		// called in Game_GameplayManager.cs 
		// when envelope appears
		public void OnPrepareScenario(){
			print("Feedback: OnPrepareScenario");
			dmx.DMX_PrepareScenario (); // rein

			// networkView.RPC ("PrepareScenarioOnTablet", RPCMode.All);
			
			if (musicS.clip != sounds [1]) 
			{
				//musicS.Stop ();
				musicS.clip = sounds [1];
				musicS.Play ();
			}
		}

		// called in Tutorial_EnvelopeManager and Game_EnvelopeManager
		public void OnRiseEnvelope(){
			print("Feedback: OnRiseEnvelope");
		}

		// called in Game_GameplayManager.cs 
		// when envelope is opened
		public void OnStartScenario(){
			print("Feedback: OnStartScenario");
			introVal++;
			dmx.DMX_StartScenario (); // rein
			// networkView.RPC ("EnterScenarioTime", RPCMode.All);
			//
			musicS.clip = sounds [2];
			musicS.Play();
		}

		// called in Tutorial_OpenEnvelope and Game_OpenEnvelope
		public void OnOpenEnvelope(){
			print("Feedback: OnOpenEnvelope");
		}


		public void OnSecondsLeft(int seconds){
			textManager.hudMessage.color = new Color(textManager.hudMessage.color.r, textManager.hudMessage.color.g, textManager.hudMessage.color.b, 1);
			textManager.hudMessage.text = seconds + " seconds left";
			print(seconds + " seconds left");
			// Fade out
        	textManager.WaitThenFadeOutText_trigger(textManager.hudMessage);
		}


		// called in Game_GameplayManager.cs 
		public void OnTimeOver(){
			print("Feedback: OnTimeOver");
			textManager.hudMessage.text = "Time over";
			
			dmx.DMX_ScenarioTimeOver (); // rein

			audioS.clip = sounds [6];
			audioS.Play();
		}

		// called in Tutorial_Mailbox and Game_Mailbox
		public void OnEnteredTube(){
			print("Feedback: OnEnteredTube");
			audioS.clip = sounds [5];
			audioS.Play();
			dmx.DMX_PutInTube();
		}



		

		// called in Tutorial_Combine and Game_Combine
		public void OnRightCombination(){
			print("Feedback: OnRightCombination");

			audioS.clip = sounds [5];
			audioS.Play();
		}
		
	}
