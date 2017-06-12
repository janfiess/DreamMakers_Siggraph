// attached to every grabbable and mergeable item
// controller rumble when the item is Ungrabbed

namespace VRTK
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Game_GrabItem : VRTK_InteractableObject {
		Game_Combine combiner;
		Game_Item item;
		GameObject currentlyGrabbedItem;
		Global_NetworkingManager networkManager;


		public VRTK_ControllerEvents controllerEvents;   /* Vibration */
		public VRTK_ControllerActions controllerActions; /* Vibration */

		void Start(){
			combiner = Game_ReferenceManager.Instance.combiner;
			item = GetComponent<Game_Item>();
			currentlyGrabbedItem = Global_ReferenceManager.Instance.grabbedItem;
			networkManager = Global_ReferenceManager.Instance.networkManager;
		}

		public override void Grabbed(GameObject controller){
			base.Grabbed(controller);

			// save this object in the central available scriptGlobal_ReferenceManager.cs
			currentlyGrabbedItem = gameObject;
			print("Grabbed: " + this.gameObject);


			// Vibration Feedback
			controllerEvents = controller.GetComponent<VRTK_ControllerEvents>();  /* Vibration */
			controllerActions = controller.GetComponent<VRTK_ControllerActions>();  /* Vibration */

	

			// add gravity to Rigidbody
			if(GetComponent<Rigidbody>().useGravity == false){
				GetComponent<Rigidbody>().useGravity = true;
				// GetComponent<ConstantForce>().enabled = true;
			}

			// prevent the tablet player from combining himself by dragging the matching items onto each other.
			// In this script (Game_ItemCollide.cs) an item will only collide with another item if at least one of them has been grabbed.
			// so mark this item as being grabbed.
			
			if(item.hasBeenGrabbedAtLeastOnce != true){
				item.hasBeenGrabbedAtLeastOnce = true;

				// further stop the animation of an item once being grabbed
				if(item.gameObject.transform.childCount > 0){
					item.gameObject.transform.GetChild(0).GetComponent<Animation>().Stop();
					networkManager.Stop_TurnAnimation_Sender(gameObject.name);
				}
				

			}

			item.isCurrentlyGrabbed = true;



		}

		public override void Ungrabbed(GameObject controller){
			base.Ungrabbed(controller);
		
			// Vibration Feedback

			controllerEvents = null;  /* Vibration */
			controllerActions = null;  /* Vibration */

			item.isCurrentlyGrabbed = false;

			currentlyGrabbedItem = null;
		}

		protected override void Update () {
			base.Update();
			/* Vibration all the time when an item is grabbed */

			if (controllerEvents) {
				var power = 0.05f;
				controllerActions.TriggerHapticPulse (power, 0.1f, 0.01f); /* Vibration */
			}

			/* end Vibration */
		}
	}
}
