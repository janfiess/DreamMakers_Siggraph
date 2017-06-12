using UnityEngine;
using System.Collections;

public class itemDrop : drop 
{
	[HideInInspector]
	public itemSpawn myCreator;
	[HideInInspector]
	public int itemIndex;
	[HideInInspector]
	public VRRay vrRay;
	[HideInInspector]
	public UiDrag drag;

	// Use this for initialization
	public virtual void Start () 
	{
		vrRay = GameObject.Find ("VRRAYCASTER").GetComponent<VRRay> ();
		drag = GetComponent<UiDrag> ();
	}

	public virtual void OnDragEnd()
	{
		Vector3 vrPos = vrRay.GetVRPoint (Input.mousePosition);
		//Vector3 vrPos = vrRay.GetVRPoint (transform.position); THER IS A PROBLEM needs to be in worldspace???
		if (vrPos != Vector3.zero && drag.m_active) 
		{
			GetComponent<UiDrag> ().KillParticles ();
			myCreator.RemoveItemInstance(gameObject);
			myCreator.PlaySound ();
//			print ("VR Positon: " + vrPos + " Iem: " + gameObject.name);
			myCreator.SpawnItem (itemIndex);
			Destroy (gameObject);
		}
	}
}



//using UnityEngine;
//using System.Collections;
//
//public class itemDrop : drop 
//{
//	[HideInInspector]
//	public itemSpawn creator;
//	[HideInInspector]
//	public int itemIndex;
//
//	public VRRay vrRay;
//	public Spawner spawner;
//
//
//	void Awake(){
//		spawner = Spawner.instance;
//	}
//
//	// Use this for initialization
//	void Start () 
//	{
//		vrRay = GameObject.Find ("VRRAYCASTER").GetComponent<VRRay> ();
//	}
//		
//
//	public virtual void OnDragEnd()
//	{
//		Vector3 vrPos = vrRay.GetVRPoint (Input.mousePosition);
//		//Vector3 vrPos = vrRay.GetVRPoint (transform.position); THER IS A PROBLEM needs to be in worldspace???
//		if (vrPos != Vector3.zero) 
//		{
//			string prefabName = gameObject.name.Substring (0, gameObject.name.IndexOf ("("));
//			spawner.SpawnItem(prefabName, vrPos);
//
//			creator.SpawnItem (itemIndex);
//
//			// Kill 2D Item
//			Destroy (gameObject);
//		}
//	}
//}


