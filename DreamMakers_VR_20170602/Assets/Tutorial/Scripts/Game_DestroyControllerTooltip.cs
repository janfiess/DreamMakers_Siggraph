// script attached to the Controller tooltips
// if the Tutorial scene is exitted / the MainGame scene is entered, the tooltip destroys itself

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DestroyControllerTooltip : MonoBehaviour {

	public Global_Manager globalManager;

	void Update () {
		if(globalManager.isTutorial == false){
			Destroy(this.gameObject);
		}
	}
}
