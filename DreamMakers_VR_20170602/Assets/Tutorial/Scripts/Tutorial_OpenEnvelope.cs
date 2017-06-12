// script attached to the envelope prefab


namespace VRTK
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Tutorial_OpenEnvelope : VRTK_InteractableObject {

		Animator animator;
		GameObject letter_tooltip;
		Tutorial_ItemSpawner itemSpawner;
		Tutorial_Gameplay gameplayManager;
		GameObject mailbox_prefab, mailbox_container;
		[ReadOnly] public GameObject tutorial_ingredient1_prefab;
		[ReadOnly] public GameObject tutorial_ingredient2_prefab;
		Global_Feedback feedbackManager;
		
		protected void Start () {
			animator = GetComponent<Animator>();
			letter_tooltip = Tutorial_ReferenceManager.Instance.letter_tooltip;	
			itemSpawner = Tutorial_ReferenceManager.Instance.itemSpawner;
			gameplayManager = Tutorial_ReferenceManager.Instance.gameplayManager;
			mailbox_prefab = Tutorial_ReferenceManager.Instance.mailbox_prefab;
			mailbox_container = Tutorial_ReferenceManager.Instance.mailbox_container;

			Tutorial_Items_Combinations itemsAndCombinations = Tutorial_ReferenceManager.Instance.itemsAndCombinations;
			tutorial_ingredient1_prefab = itemsAndCombinations.tutorial_red;
			tutorial_ingredient2_prefab = itemsAndCombinations.tutorial_blue;
			feedbackManager = Global_ReferenceManager.Instance.feedbackManager;
	
		}
		
		// called when you grab the envelope with the controller to open
		public override void StartUsing(GameObject controller)
        {
            base.StartUsing(controller);
            // OpenEnvelope
			animator.SetTrigger("OpenEnvelope");

			// delete Envelope Tooltip when envelope is grabbed
			if(transform.GetChild(4)){
				Destroy(transform.GetChild(4).gameObject);
			}
			
			// delete controller Tooltip when envelope is grabbed
			if(controller.transform.childCount > 1){
				Destroy(controller.transform.GetChild(0).gameObject);
			}

			// show Letter Tooltip as soon as the letter is on the table
			StartCoroutine(ShowLetterTooltip());

			// show ingredients after the letter is on the table
			StartCoroutine(ShowIngredients_InTutorial_WithoutTablet());

			// instantiate and rise mailbox
			GameObject mailbox_instance = Instantiate(mailbox_prefab);
			mailbox_instance.transform.parent = mailbox_container.transform;

			// delete collider of the envelope so that the items do not collide with it anymore
			Destroy(GetComponent<Collider>());

			feedbackManager.OnOpenEnvelope();

        }

	
		// only in tutorial
		IEnumerator ShowLetterTooltip(){
			yield return new WaitForSeconds(9.5f);
			if(letter_tooltip != null){
				letter_tooltip.SetActive (true);
			}
			
			yield return null;
		}


		IEnumerator ShowIngredients_InTutorial_WithoutTablet(){
			yield return new WaitForSeconds(12.0f);
			
			if(gameplayManager.isTutorialScene == true){
				// spawn Ingredients
				itemSpawner.SpawnItem(tutorial_ingredient1_prefab, tutorial_ingredient1_prefab.transform.position);
				itemSpawner.SpawnItem(tutorial_ingredient2_prefab, tutorial_ingredient2_prefab.transform.position);
			}
			yield return null;
		}
	}
}
