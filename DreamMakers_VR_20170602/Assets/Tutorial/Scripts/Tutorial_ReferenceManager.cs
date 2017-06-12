// This script is designed to establish references for instantiated prefabs because you cannot 
// assign references to prefabs directly. 
// Attach this script to an empty gameobject (called "Manager") which is always available in scene

using UnityEngine;
using System.Collections.Generic;

public class Tutorial_ReferenceManager : MonoBehaviour {

	
	

	public GameObject letter_tooltip;
	public GameObject mailbox_tooltip;
	public GameObject mailbox_prefab; // used in Tutorial_OpenEnvelope.cs
	public GameObject mailbox_container; // used in Tutorial_OpenEnvelope.cs


	[Header("Dependant scripts")]
	public Tutorial_Combine combiner; // Combination script attached to the Manager
	public Tutorial_ItemSpawner itemSpawner;  // used in Tutorial_OpenEnvelope.cs
	public Tutorial_Gameplay gameplayManager;  // used in Tutorial_OpenEnvelope.cs, Tutorial_Combine.cs
	public Tutorial_Items_Combinations itemsAndCombinations;
	public Tutorial_EnvelopeManager envelopeManager; // used in Tutorial_Gameplay



	// reference to this script, must be static
	private static Tutorial_ReferenceManager refManagerScript;
	void Awake(){
		// reference to this script
		refManagerScript = this;
	}
	
	public static Tutorial_ReferenceManager Instance {
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