// attached to every grabbable and mergeable item
// controller rumble when the item is Ungrabbed

namespace VRTK
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Tutorial_GrabItem : VRTK_InteractableObject {
		Tutorial_Combine combiner;

		public VRTK_ControllerEvents controllerEvents;   /* Vibration */
		public VRTK_ControllerActions controllerActions; /* Vibration */

		void Start(){
			combiner = Tutorial_ReferenceManager.Instance.combiner;
		}

		public override void Grabbed(GameObject controller){
			base.Grabbed(controller);

			// Vibration Feedback
			controllerEvents = controller.GetComponent<VRTK_ControllerEvents>();  /* Vibration */
			controllerActions = controller.GetComponent<VRTK_ControllerActions>();  /* Vibration */

			// remove child GameObject (remove tooltip from Item)
			if(this.transform.childCount > 0){
				Destroy(this.transform.GetChild(0).gameObject);
			}

			// add gravity to Rigidbody
			if(GetComponent<Rigidbody>().useGravity == false){
				GetComponent<Rigidbody>().useGravity = true;
			}

			// remove tooltip from controller
			if(controller.transform.childCount > 1){
				Destroy(controller.transform.GetChild(0).gameObject);
			}
		}

		public override void Ungrabbed(GameObject controller){
			base.Ungrabbed(controller);
		
			// Vibration Feedback

			controllerEvents = null;  /* Vibration */
			controllerActions = null;  /* Vibration */
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
