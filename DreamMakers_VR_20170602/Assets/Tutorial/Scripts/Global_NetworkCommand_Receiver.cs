using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_NetworkCommand_Receiver : MonoBehaviour
{
    public Game_GameplayManager gameplayManager;
    public void ExecuteCommand(string receivedCommandString)
    {
        print("From ExecuteCommand(): " + receivedCommandString);
        string commandString = receivedCommandString;

        switch (commandString)
        {
            case "Show it":
                print("Execute Command Show It");
                break;

			case "test":
				print("Execute Command test");
                break;

	
        }

    }
    public void testIt()
    {
        print("receiver tested");
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
