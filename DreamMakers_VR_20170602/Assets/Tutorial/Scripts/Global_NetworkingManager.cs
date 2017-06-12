using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK.Examples;
using UnityEngine.SceneManagement;

public class Global_NetworkingManager : MonoBehaviour
{

    bool isServer = false;
    bool isStarted = false;
    public string ipAddress = "127.0.0.1";

    Global_Feedback feedback;

    Global_Manager globalManager;
    NetworkView networkView;
    public Tutorial_Gameplay tutorial_GameplayManager;
	public Game_ItemSpawner itemSpawner;

    void Awake()
    {
        networkView = GetComponent<NetworkView>();

    }

    void Start()
    {
        globalManager = GetComponent<Global_Manager>();
        feedback = GetComponent<Global_Feedback>();
        StartServer();
    }

    public void StartServer()
    {
        print("Start Server!");
        isServer = true;
        Network.InitializeServer(32, 3000, false);
        isStarted = true;

    }

    public void StartClient()
    {
        print("Start Client!");
        Network.Connect(ipAddress, 3000);
        isStarted = true;

    }




    [RPC]
    void TestIt()
    {
        print("huhuhuhuhuhuhu");
    }

    [RPC]
    void spawnItem(string itemName, Vector3 itemPosition)
    {
        print("Network: spawnItem: " + itemName);
        if (itemSpawner != null)
        {
            feedback.OnSpawnNewItem();
            GameObject selectedItem = Game_Items_Combinations.itemPrefabs[itemName];
            itemSpawner.SpawnItem(selectedItem, itemPosition);

            // GameObject selectedItem = (GameObject)Resources.Load("Prefabs/" + itemName, typeof(GameObject));
            // GameObject instantiatedItem = Instantiate(selectedItem, itemPosition, Quaternion.identity);
    

        }

    }

	
	public void Stop_TurnAnimation_Sender(string itemName){
		print("stop animation on " + itemName);
		networkView.RPC("StopTurnAnimation", RPCMode.All, itemName);
	}

	[RPC]
	void StopTurnAnimation(string itemName){
		print("Network: StopTurnAnimation on " + itemName);
	}


	public void RemoteMove_Sender(Vector3 position, string name){
		networkView.RPC ("RemoteMove", RPCMode.Others, position, name);
	}



	public void RemoteRotate_Sender(Vector3 orientation, string name){
		networkView.RPC ("RemoteRotate", RPCMode.Others, orientation, name);
			
	}

	public void DestroyItem_Sender(string itemName){
		networkView.RPC ("DestroyItem", RPCMode.All, itemName);
           
	}
	[RPC]
	void DestroyItem(string itemName){
		print("Network: Destroy Item " + itemName);
	}

	public void spawnItemAsResult_Sender(string itemName, Vector3 itemPosition){
		networkView.RPC ("spawnItemAsResult", RPCMode.All, itemName, itemPosition);
	}

	[RPC]
	void spawnItemAsResult (string itemName, Vector3 itemPosition){
		print("Network: spawnItemAsResult: " + itemName);
	}


	public void SynchronizeHeadset_Sender(Vector3 headsetPosition, Vector3 headsetOrientation){
		networkView.RPC ("synchronizeHeadset", RPCMode.All, headsetPosition, headsetOrientation);
	}

	[RPC]
	void synchronizeHeadset (Vector3 headsetPosition, Vector3 headsetOrientation){
		// print("Network: synchronizeHeadset: ");
	}





    [RPC]
    void CanNotModify(string itemName)
    {
        GameObject selectedItem = GameObject.Find(itemName);
        //		if (selectedItem.GetComponent<Item>().canModify == true)
        //			return;	
        selectedItem.GetComponent<Game_Item>().canModify = false;
    }

    [RPC]
    void OnlyVrCanModify(string itemName)
    {
        GameObject selectedItem = GameObject.Find(itemName);
        selectedItem.GetComponent<Game_Item>().canModify = true;
    }


    [RPC]
    void RemoteMove(Vector3 position, string itemName)
    {
        // GameObject selectedItem = GameObject.Find(itemName);
        // if (selectedItem != null)
        // {
        //     if (selectedItem.GetComponent<Game_Item>().canModify == true)
        //         return;
        //     selectedItem.transform.position = position;
        // }
    }



    [RPC]
    void RemoteRotate(Vector3 orientation, string itemName)
    {
        // GameObject selectedItem = GameObject.Find(itemName);
        // if (selectedItem != null)
        // {
        //     if (selectedItem.GetComponent<Game_Item>().canModify == true)
        //         return;
        //     selectedItem.transform.eulerAngles = orientation;
        // }

    }

   



    /**
	 * Send game status to tablet
	 */

    // Start Game (envelope appears) when the tablet spawn scene starts - called on tablet side in NetworkingManafer.cs (Start)
    // Tablet -> VR
    [RPC]
    void helloFromTablet()
    {

        print("Network: helloFromTablet Spawn Tutorial Envelope");
        globalManager.tabletConnected = true;
        tutorial_GameplayManager.PrepareScenario();
    }

    public void HelloFromVR_sender(int maxScenarioTime)
    {
        networkView.RPC("HelloFromVR", RPCMode.All, maxScenarioTime);
    }

    // VR -> Tablet
    [RPC]
    void HelloFromVR(int maxScenarioTimee)
    {
        print("Network: Hello from VR: Send MaxPlayTime, when Game is activated");
    }

    public void StartScenarioOnNetwork_sender()
    {
        networkView.RPC("StartScenarioOnNetwork", RPCMode.All);
    }

    // VR -> Tablet
    [RPC]
    void StartScenarioOnNetwork()
    {
        print("Network: StartScenarioOnNetwork when open envelope");
    }


    public void ExitScenarioTime_sender()
    {
        networkView.RPC("ExitScenarioTime", RPCMode.All);
    }


    // is the scenario time running?
    // VR -> Tablet
    [RPC]
    void ExitScenarioTime()
    {
        print("Network: ExitScenarioTime: hide stopwatch on tablet if scenario completed or if the time ran out");
    }


    public void PrepareScenarioOnTablet_sender()
    {
        networkView.RPC("PrepareScenarioOnTablet", RPCMode.All);
    }


    // Write the number of the current level to the tablet
    // VR -> Tablet
    [RPC]
    void PrepareScenarioOnTablet()
    {
        print("Network: PrepareScenarioOnTablet: Fade in stopwatch, message");
    }



    // Change player on tablet (unten links: new Game)
    // Tablet -> VR
    [RPC]
    void ChangePlayer()
    {
        Tutorial_Items_Combinations.itemPrefabs.Clear();
        Game_Items_Combinations.itemPrefabs.Clear();
        SceneManager.LoadScene(0);
    }



}
