using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_HeadsetPosition : MonoBehaviour {

	public GameObject headset;
	Global_NetworkingManager networkManager;
	Vector3 previous_headsetPosition, previous_headsetOrientation;
	void Start () {
		networkManager = GetComponent<Global_NetworkingManager>();
		previous_headsetPosition = headset.transform.position;
		previous_headsetOrientation = headset.transform.eulerAngles;
	}
	
	void Update () {
		Vector3 headsetPosition = headset.transform.position;
		Vector3 headsetOrientation = headset.transform.eulerAngles;

		if(headsetPosition != previous_headsetPosition || headsetOrientation != previous_headsetOrientation){
		
			networkManager.SynchronizeHeadset_Sender(headsetPosition, headsetOrientation);
			previous_headsetPosition = headsetPosition;
			previous_headsetOrientation = headsetOrientation;
			
		}
	}
}
