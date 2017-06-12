// This script is designed to establish references for instantiated prefabs because you cannot 
// assign references to prefabs directly. 
// Attach this script to an empty gameobject (called "Manager") which is always available in scene

using UnityEngine;
using System.Collections.Generic;

public class Game_ReferenceManager : MonoBehaviour {

	

	public GameObject mailbox_prefab; // used in Tutorial_OpenEnvelope.cs
	public GameObject mailbox_container; // used in Tutorial_OpenEnvelope.cs

	[Header("Dependant scripts")]
	public Game_Combine combiner; // Combination script attached to the Manager
	public Game_ItemSpawner itemSpawner;  // used in Tutorial_OpenEnvelope.cs
	public Game_GameplayManager gameplayManager;  // used in Tutorial_OpenEnvelope.cs, Tutorial_Combine.cs
	public Game_Items_Combinations itemsAndCombinations;
	public Game_EnvelopeManager envelopeManager; // used in Tutorial_Gameplay
	
	public HitManager labHitManager;

	// reference to this script, must be static
	private static Game_ReferenceManager refManagerScript;
	void Awake(){
		// reference to this script
		refManagerScript = this;
	}
	
	public static Game_ReferenceManager Instance {
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
// 		combiner = Tutorial_ReferenceManager.Instance.combiner;
// }