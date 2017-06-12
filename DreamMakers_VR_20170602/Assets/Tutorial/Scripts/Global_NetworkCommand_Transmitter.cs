// script attached to the player prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Global_NetworkCommand_Transmitter : NetworkBehaviour {


	Global_NetworkCommand_Receiver networkCommand_Receiver;
	void Start () {


		// networkCommand_Receiver = Global_ReferenceManager.Instance.networkCommand_Receiver;
		networkCommand_Receiver.testIt();

		Global_NetworkCommand_Sender.networkCommand_Event += TransmitCommand;
	}
	
	public void TransmitCommand(){
		if(!isLocalPlayer)return;
		string commandString = Global_NetworkCommand_Sender.Instance.commandString;
		print(" CommandString from TransmitCommand(): " + commandString);
		CmdSendCommandToServer(commandString);
	}

	[Command]
	void CmdSendCommandToServer(string commandString){
		if(!isServer) return;
		print(" CommandString from CmdSendCommandToServer(): " + commandString);

		RpcSendCommandToClients(commandString);
	}

	[ClientRpc]
	void RpcSendCommandToClients(string commandString){
		if(!isClient) return;
		print(" CommandString from RpcSendCommandToClients(): " + commandString);
		networkCommand_Receiver.ExecuteCommand(commandString);
	}
}
