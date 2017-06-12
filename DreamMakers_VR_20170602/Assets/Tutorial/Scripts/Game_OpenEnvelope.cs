// script attached to the envelope prefab


namespace VRTK
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Game_OpenEnvelope : VRTK_InteractableObject {

		Animator animator;
		Game_ItemSpawner itemSpawner;
		Game_GameplayManager gameplayManager;
		GameObject mailbox_prefab, mailbox_container;
		Global_Feedback feedbackManager;
		
		protected void Start () {
			animator = GetComponent<Animator>();
			itemSpawner = Game_ReferenceManager.Instance.itemSpawner;
			gameplayManager = Game_ReferenceManager.Instance.gameplayManager;
			mailbox_prefab = Game_ReferenceManager.Instance.mailbox_prefab;
			mailbox_container = Game_ReferenceManager.Instance.mailbox_container;

			Game_Items_Combinations itemsAndCombinations = Game_ReferenceManager.Instance.itemsAndCombinations;
			feedbackManager = Global_ReferenceManager.Instance.feedbackManager;
		}


		// called when you grab the envelope with the controller to open
		public override void StartUsing(GameObject controller)
        {
            base.StartUsing(controller);
			OpenEnvelope();
			gameplayManager.StartScenario();
			print("Fire feedback on open in game");
			feedbackManager.OnOpenEnvelope();
        }


		/************************************************************************************
		 * Open the envelope
		 ************************************************************************************ */
		

		void OpenEnvelope(){
			// OpenEnvelope
			animator.SetTrigger("OpenEnvelope");
	
			// instantiate and rise mailbox
			GameObject mailbox_instance = Instantiate(mailbox_prefab);
			mailbox_instance.transform.parent = mailbox_container.transform;

			// delete collider of the envelope so that the items do not collide with it anymore
			Destroy(GetComponent<Collider>());
		}
	}
}