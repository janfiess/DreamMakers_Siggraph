using UnityEngine;
using System.Collections;

public class NetworkingManager : MonoBehaviour {


	bool isServer = false;
	bool isStarted = false;
	public string ipAddress = "127.0.0.1";
	public GameObject btn_connectClient;
	public static string text = "text";

	public GameObject bubblePrefab;
	public NetworkView networkView;
	GameManager gameManager;
	public GameObject hmd;


	void Awake(){
		GameObject manager = Spawner.instance.manager;
		networkView = manager.GetComponent<NetworkView> ();
		gameManager = this.gameObject.GetComponent<GameManager> ();
	}

	public void StartClient(){
		print ("Start Client!");
		Network.Connect (ipAddress, 3000);
		isStarted = true;
		Destroy (btn_connectClient);
		StartCoroutine (WaitUntilConnectToVR());
		// Start Game in VR -> Show envelope
//		networkView.RPC ("StartVR", RPCMode.Others);

	}





	// Tablet -> VR
	[RPC]  
	void TestIt (){
		print ("huhu ");

		networkView.RPC ("StartVR", RPCMode.Others);
	}
	

	public void TryIt(){
		networkView.RPC ("TestIt", RPCMode.All);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.S)){
			TryIt();
		}
	}







	[RPC]
	void spawnItem (string itemName, Vector3 itemPosition){
		
		GameObject selectedItem = (GameObject)Resources.Load("Prefabs/" + itemName, typeof(GameObject));
		GameObject instantiatedItem = Instantiate (selectedItem, itemPosition, Quaternion.identity);
		
		instantiatedItem.name = instantiatedItem.name + GameManager.itemNumber;
		
		GameManager.itemNumber++;

		instantiatedItem.transform.parent = GameObject.Find ("World").transform;
		
		if (instantiatedItem != null) {
			print ("NetworkManager spawnItem " + itemName);
		}
	}

	[RPC]
	void StopTurnAnimation(string itemName){
		print("Network: stopTurnAnimation: " + itemName);
		GameObject selectedItem = GameObject.Find(itemName);
		selectedItem.transform.GetChild(0).GetComponent<Animation>().Stop();
	}
	

	[RPC]
	void DestroyItem (string itemName){
		print("Network: DestroyItem: " + itemName);
		GameObject selectedItem = GameObject.Find(itemName);
		Destroy (selectedItem);
	}

	[RPC]
	void spawnItemAsResult (string itemName, Vector3 itemPosition){
		print("Network: spawnItemAsResult: " + itemName);
		spawnItem(itemName, itemPosition);
	}
	
	/*
	[RPC]
	void DestroyBubbleAndMakeInteractable(string bubbleItemName){
		GameObject bubble = GameObject.Find (bubbleItemName);
		GameObject freeItem = bubble.transform.GetChild (0).gameObject;
		Rigidbody freeItem_rigidbody = freeItem.GetComponent<Rigidbody> ();
		freeItem_rigidbody.isKinematic = false;
		freeItem_rigidbody.useGravity = false;
		freeItem.transform.parent = bubble.transform.parent;
		// Remove bubble
		Destroy (bubble);
	}
	*/






	[RPC]
	void CanNotModify (string itemName){
		GameObject selectedItem = GameObject.Find(itemName);

		if(selectedItem != null){
			selectedItem.GetComponent<Item>().canModify = false;
			selectedItem.GetComponent<Rigidbody> ().useGravity = false; // 20170222
		}
	}

	[RPC]
	void OnlyVrCanModify(string itemName){
		GameObject selectedItem = GameObject.Find(itemName);
		selectedItem.GetComponent<Item>().canModify = false;
	}


	[RPC]
	void RemoteMove (Vector3 position, string itemName){
		GameObject selectedItem = GameObject.Find(itemName);
		if (selectedItem != null) {
			//if (selectedItem.GetComponent<Item> ().canModify == true)
			//	return;	
			text = itemName;
			selectedItem.transform.position = position;
		}
	}

	[RPC]
	void RemoteRotate (Vector3 orientation, string itemName){
		GameObject selectedItem = GameObject.Find(itemName);
		if (selectedItem != null) {
		//	if (selectedItem.GetComponent<Item> ().canModify == true)
		//		return;
			selectedItem.transform.eulerAngles = orientation;
		}
	}

	[RPC]
	void synchronizeHeadset (Vector3 headsetPosition, Vector3 headsetOrientation){
		hmd.transform.position = new Vector3(headsetPosition.x, hmd.transform.position.y, headsetPosition.z);
		hmd.transform.eulerAngles = new Vector3(0, headsetOrientation.y, 0);
		
		//print(headsetOrientation.y);
		//print(headsetOrientation.y * Mathf.Rad2Deg);
	}



	/*
	 * Exchange game status with VR
	 */







	[ContextMenu("WaitUntilConnectToVR")]
	IEnumerator WaitUntilConnectToVR(){
		yield return new WaitForSeconds (0.5f);
		networkView.RPC ("helloFromTablet", RPCMode.Others);
		print ("Network: Hello from tablet !");
	}

	// Tablet -> VR
	[RPC]
	void helloFromTablet(){
		print ("Network: Started  via tablet !");
	}

	// VR -> Tablet
	[RPC]
	void HelloFromVR(int maxScenarioTime){

		gameManager.maxScenarioTime = gameManager.remainingScenarioTime = maxScenarioTime;
		print("Network: Hello from VR");
	}











	// Start Scenario Stopwatch /when enevelope is opened
	// VR -> Tablet
	[RPC]
	void StartScenarioOnNetwork(){
		print("Network: Envelope opened in VR: StartScenarioOnNetwork()");
		gameManager.StartScenario ();
		gameManager.EnterScenarioAction();
	} 



	// is the scenario time running?
	// VR -> Tablet
	[RPC]
	void ExitScenarioTime(){
		gameManager.ExitScenarioAction ();
		print("Network: Exit scenario time");
	}









	// Write the number of the current level onto the tablet
	// VR -> Tablet
	[RPC]
	void PrepareScenarioOnTablet(){
		gameManager.PrepareScenario ();
		print("Network: PrepareScenarioOnTablet");
	}




	// Change player on tablet
	// Tablet -> VR
	[RPC]
	void ChangePlayer(){

	}


}
