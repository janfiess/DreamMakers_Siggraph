
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using VRTK;

	public class Tutorial_Feedback : MonoBehaviour {
		// NetworkView networkView;
		Tutorial_Gameplay gameplay;
		public VRTK_ControllerActions rightControllerActions;
		public VRTK_ControllerActions leftControllerActions;
		private AudioSource audioS;
		public AudioSource musicS;
		// public DmxConfigurator dmx; // rein

		private int introVal = 0;

		public List<AudioClip> sounds = new List<AudioClip>();



		void Awake()
		{
			audioS = GetComponent<AudioSource> ();
		}

		void Start () {
			gameplay = GetComponent<Tutorial_Gameplay>();

		}

		void Update(){
			if (Input.GetKeyDown (KeyCode.Q)) {
				rightControllerActions.TriggerHapticPulse(0.63f, 0.2f, 0.01f);
				leftControllerActions.TriggerHapticPulse(0.63f, 0.2f, 0.01f);
			}
		}
		
		public void OnSpawnNewItem(){
			// dmx.DMX_Spawn (); // rein
		}

		public void OnIntro(){
			// dmx.DMX_StartGame (); // rein
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
					//musicS.clip = sounds [1];
					//musicS.Play ();
				}
			}
		}
			
		public void OnBreakBubble(){
			
		}

		public void OnCorrectCombination(){
			// dmx.DMX_RightCombination (); // rein
			audioS.clip = sounds [5];
			audioS.Play();
		}

		public void OnWrongCombination(GameObject item){
			// dmx.DMX_WrongCombination (); rein 



			// vibration feedback on the controller which holds the wrong item

			// var thisTransformItem = item.GetComponent<TransformItem> ();
			// if (thisTransformItem.controllerEvents) {
			// 	var power = thisTransformItem.controllerEvents.GetTriggerAxis ();
			// 	thisTransformItem.controllerActions.TriggerHapticPulse (power * 0.5f, 0.1f, 0.01f);
			// }

		}

		public void OnCompletedGame(){

		}

		public void OnPrepareScenario(){
			// dmx.DMX_PrepareScenario (); // rein

			// networkView.RPC ("PrepareScenarioOnTablet", RPCMode.All);
			
			if (musicS.clip != sounds [1]) 
			{
				//musicS.Stop ();
				musicS.clip = sounds [1];
				musicS.Play ();
			}
		}

		public void OnStartScenario(){
			// dmx.DMX_StartScenario (); // rein
			// networkView.RPC ("EnterScenarioTime", RPCMode.All);
			//
			musicS.clip = sounds [2];
			musicS.Play();
		}

		public void OnSucceededMaxLevel (){
			//
			audioS.clip = sounds [4];
			audioS.Play();
		}

		public void On15SecondsLeft(){

		}

		public void OnTimeOver(){
			// dmx.DMX_ScenarioTimeOver (); // rein
			audioS.clip = sounds [6];
			audioS.Play();
		}
	}
