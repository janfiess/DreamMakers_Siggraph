// attach script to the UIContainer component of each tooltip 
// for preventing the outline from becoming transparent again

using UnityEngine;
using UnityEngine.UI;

public class Tutorial_TooltipOutlineVisible : MonoBehaviour {
Color targetColor = new Color(0,0,0,1);

	void Update () {
		if(GetComponent<Image>().color!= targetColor){
			GetComponent<Image>().color = new Color(0,0,0,1);
		}
		
		
	}
}
