// script attached to the global manager script always in the scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_NetworkCommand_Sender : MonoBehaviour
{

    // Use this for initialization
    public delegate void NetworkCommand_Event();
    public static event NetworkCommand_Event networkCommand_Event;
    [HideInInspector] public string commandString;

	private static Global_NetworkCommand_Sender thisScript;

	void Awake(){
		thisScript = this;
	}

	public static Global_NetworkCommand_Sender Instance{
		get{
			return thisScript;
		}
	}

	public void SendCommand(string receivedCommand){
		commandString = receivedCommand;
		print("Received command on NetworkCommandSender: " + commandString);
		if(networkCommand_Event != null) networkCommand_Event();

	}

}
