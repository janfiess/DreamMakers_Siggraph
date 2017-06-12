using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public static Spawner instance;
	public GameObject manager;

	public Camera GuiCam;
	public Camera VrCam;
	public GameObject VrTexture;
	public GameObject floor;

	void Awake(){
		manager = this.gameObject;
		// reference this script
		instance = this;
	}

	public static Spawner Instance {
		get {
			return instance;
		}
	}

	public void SpawnItem(string prefabName, Vector3 position){
//		print ("Object to instantiate: " + prefabName);
		// Vector3 transmitPosition = new Vector3 (position.x, Random.Range(1.3f,2.2f), position.z);
		Vector3 transmitPosition = new Vector3 (position.x, Random.Range(0.7f,1.7f), position.z);
		GetComponent<NetworkView>().RPC ("spawnItem", RPCMode.All, prefabName, transmitPosition);
	}
}
