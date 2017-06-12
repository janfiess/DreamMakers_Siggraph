using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerOnNetwork : itemDrop {
	public Spawner spawner;
	GameManager gameManager;

//	private UiDrag drag;

	void Awake(){
		spawner = Spawner.instance;
		GameObject manager = Spawner.instance.manager;
		gameManager = manager.GetComponent<GameManager> ();
	}

	public override void Start(){
		base.Start ();
//		drag = GetComponent<UiDrag> ();
	}

	public override void OnDragEnd(){
		base.OnDragEnd ();
		Vector3 vrPos = vrRay.GetVRPoint (Input.mousePosition);
//		print ("overriden OnDragEnd1: ");

		if (vrPos != Vector3.zero && drag.m_active) {
//			print ("overriden OnDragEnd2: ");

			gameManager.spawnableItemsCounter--;
			gameManager.spawnableItemsCounterText.text = gameManager.spawnableItemsCounter.ToString() + " items left";
			if (gameManager.spawnableItemsCounter == 0) {
				gameManager.MakeUiItemsDraggable (false);
			}
//			print("spawnableItemsCounter: " + gameManager.spawnableItemsCounter);

			string prefabName = gameObject.name.Substring (0, gameObject.name.IndexOf ("("));
			print("instantiate: prefabName: " + prefabName);
			spawner.SpawnItem(prefabName, vrPos);

		}
	}
}
