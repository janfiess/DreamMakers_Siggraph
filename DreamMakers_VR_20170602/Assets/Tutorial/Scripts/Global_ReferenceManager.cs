// This script is designed to establish references for instantiated prefabs because you cannot 
// assign references to prefabs directly. 
// Attach this script to an empty gameobject (called "Manager") which is always available in scene

using UnityEngine;
using System.Collections.Generic;

public class Global_ReferenceManager : MonoBehaviour {

	[Header("Dependant gameObjects")]

	[ReadOnly] public GameObject grabbedItem;
	public Global_Manager globalManager;
	public Global_Feedback feedbackManager;
	public Global_NetworkingManager networkManager;
	
	[Header("Dependant scripts")]


	// reference to this script, must be static
	private static Global_ReferenceManager refManagerScript;
	void Awake(){
		// reference to this script
		refManagerScript = this;
	}
	
	public static Global_ReferenceManager Instance {
		get {
			return refManagerScript; // returns a reference to this script
		}
	}

}

// in order to reference other gameobjects in prefabs (no linking possible), reference these gameobjects
// in this script which is always available in scene. When the prefab needing the reference
// becomes instantiated, the reference to the reference needed. In the Start function. Like this:
// GameObject combiner;
// void Start(){
// 		combiner = Global_ReferenceManager.Instance.combiner;
// }