// script attached to each controller

namespace VRTK.Examples
{
	using UnityEngine;

	public class ViveTriggerTest_Demo : MonoBehaviour
	{
		public Game_GameplayManager gameManager;

		private void Start()
		{
			if (GetComponent<VRTK_ControllerEvents>() == null)
			{
				Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
				return;
			}
			GetComponent<VRTK_ControllerEvents>().TriggerTouchStart += new ControllerInteractionEventHandler(DoTriggerTouchStart);
			GetComponent<VRTK_ControllerEvents>().TriggerTouchEnd += new ControllerInteractionEventHandler(DoTriggerTouchEnd);
			GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouchStart);
			GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

		}


		private void DoTriggerTouchStart (object sender, ControllerInteractionEventArgs e)
		{
			print ("DoTriggerTouchStart");
			// gameManager.isTriggerTouched = true;
		}

		private void DoTriggerTouchEnd (object sender, ControllerInteractionEventArgs e)
		{
			print ("DoTriggerTouchEnd");
			// gameManager.isTriggerTouched = false;
		}

		private void DoTouchpadTouchStart (object sender, ControllerInteractionEventArgs e)
		{
			print ("DoTouchpadTouchStart");
			// gameManager.isTouchpadTouched = true;
		}

		private void DoTouchpadTouchEnd (object sender, ControllerInteractionEventArgs e)
		{
			print ("DoTouchpadTouchEnd");
			// gameManager.isTouchpadTouched = false;
		}
	}
}