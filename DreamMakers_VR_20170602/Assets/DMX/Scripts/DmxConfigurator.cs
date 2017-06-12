using UnityEngine;
using System.Collections;

public class DmxConfigurator : MonoBehaviour {
	public Artnet artnet;
	Animator animator;
	public Material testLamp;

	public Light led_1;
	int port_led_1_r = 5; // DMX6
	int port_led_1_g = 6; // DMX7
	int port_led_1_b = 7; // DMX8
	Color prevLed_1_Color;

	public Light led_2;
	int port_led_2_r = 6;
	int port_led_2_g = 7;
	int port_led_2_b = 8;
	Color prevLed_2_Color;

	public Light shaker;
	int port_shaker = 0;
	Color prevShaker_Value;

	void Start(){
		animator = GetComponent<Animator> ();

		// set all channels to black
		for (int i = 0; i < 512; i++) {
			artnet.send (i, 0);
		}
		// DMX5 always on because master fader
		artnet.send (4, (byte) 255);
	}

	void Update () {

//		artnet.send (port_led_1_r, (byte)(testLamp.color.r * 255));
//		artnet.send (port_led_1_g, (byte)(testLamp.color.g * 255));
//		artnet.send (port_led_1_b, (byte)(testLamp.color.b * 255));

		
//		 control LED 1
		if (led_1.color != prevLed_1_Color) {
			artnet.send (port_led_1_r, (byte)(led_1.color.r * 255));
			artnet.send (port_led_1_g, (byte)(led_1.color.g * 255));
			artnet.send (port_led_1_b, (byte)(led_1.color.b * 255));
			prevLed_1_Color = led_1.color;
		}

//		 control LED 2
		if (led_2.color != prevLed_2_Color) {
			artnet.send (port_led_2_r, (byte)(led_2.color.r * 255));
			artnet.send (port_led_2_g, (byte)(led_2.color.g * 255));
			artnet.send (port_led_2_b, (byte)(led_2.color.b * 255));
			prevLed_2_Color = led_2.color;
		}

//		 control shaker
		if (shaker.color != prevShaker_Value) {
			artnet.send (port_shaker, (byte)(shaker.color.r * 255));
			prevShaker_Value = shaker.color;
		}
	}


	public void DMX_OpenEnvelope(){
		animator.SetTrigger ("trg_OpenEnvelope");
	}

	public void DMX_StartGame(){
		animator.SetTrigger ("trg_StartGame");
	}

	public void DMX_IdleState(){
		animator.SetTrigger ("trg_IdleState");
	}

	public void DMX_Spawn(){
		animator.SetTrigger ("trg_Spawn");
	}

	public void DMX_RightCombination(){
		animator.SetTrigger ("trg_RightCombination");
	}

	public void DMX_WrongCombination(){
		animator.SetTrigger ("trg_WrongCombination");
	}

	public void DMX_PrepareScenario(){
		animator.SetTrigger ("trg_PrepareScenario");
	}

	public void DMX_StartScenario(){
		animator.SetTrigger ("trg_StartScenario");
	}

	public void DMX_ScenarioTimeOver(){
		animator.SetTrigger ("trg_ScenarioTimeOver");
	}

	public void DMX_TutorialIdle(){
		animator.SetTrigger ("trg_Tutorial");
	}

	public void DMX_PutInTube(){
		animator.SetTrigger ("trg_PutInTube");
	}

}
