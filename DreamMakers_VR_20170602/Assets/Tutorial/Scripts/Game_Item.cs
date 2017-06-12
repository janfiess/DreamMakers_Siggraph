// this script is attached to every grabbable item (prefabs)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Item : MonoBehaviour {
	[ReadOnly] public bool canModify = false;
	Game_Items_Combinations itemsAndCombinations;

	public GameObject prefab; // check to which prefab this gameobject belongs even after instantiation
	
	// prevent the tablet player from combining himself by dragging the matching items onto eachh other.
	// In Game_ItemCollide.cs an item will only collide with another item if at least one of them has been grabbed
	[ReadOnly] public bool hasBeenGrabbedAtLeastOnce = false; 
	[ReadOnly] public bool isCurrentlyGrabbed = false; 
	// store item position for making sure it does not move when not grabbed
	// [SerializeField] private Vector3 previousPosition;
	Vector3 prevPos;
	Vector3 prevOrientation;
	Global_NetworkingManager networkManager;



	void Start () {


		string thisItemName = this.gameObject.name;
		string prefabName;
		if(thisItemName.Contains("(")){
			prefabName = thisItemName.Substring(0, thisItemName.IndexOf ("(")); // get the prefab's name by removing the "(Instance)"
		}
		else prefabName = thisItemName; // if the item is not instantiated from a prefab but just dragged into the scene
		
		prefab = Game_Items_Combinations.itemPrefabs [prefabName];
		// print("Prefab Name: " + prefab.name);

		prevPos = this.gameObject.transform.position;
		prevOrientation = this.transform.eulerAngles;

		networkManager = Global_ReferenceManager.Instance.networkManager;
	}

	void Update () {
		if (transform.position != prevPos) {
			networkManager.RemoteMove_Sender(this.gameObject.transform.position, this.gameObject.name);
				prevPos = transform.position;
		}

		if (transform.eulerAngles != prevOrientation) {
			networkManager.RemoteRotate_Sender(this.gameObject.transform.eulerAngles, this.gameObject.name);
			prevOrientation = transform.eulerAngles;
		}
	}

	
	

}
