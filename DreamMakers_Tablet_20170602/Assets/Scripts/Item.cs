using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	public NetworkView networkView;
	Vector3 prevPos;
	Vector3 prevOrientation;
	float prevSize;
	//	Color prevColor;
	public bool canModify = false;
	GameObject thatItem = null;
	GameObject floor;

	void Start () {
		prevPos = this.gameObject.transform.position;
		prevOrientation = this.transform.eulerAngles;
		prevSize = this.transform.localScale.x;
		//		prevColor = this.gameObject.GetComponent<Renderer> ().material.color;
		GameObject manager = Spawner.instance.manager;
		networkView = manager.GetComponent<NetworkView> ();

		GameManager.items.Add (this.gameObject.name, this.gameObject);
		floor = Spawner.Instance.floor;
	}

	void Update () {
/* 

		// if position has changed
		if (transform.position != prevPos) {
			//			if (transform.position.y < 0.5f) {
			//				transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
			//			}
			networkView.RPC ("RemoteMove", RPCMode.Others, this.gameObject.transform.position, this.gameObject.name);
			prevPos = transform.position;
		}


		// if orientation (rotation) has changed
		if (transform.eulerAngles != prevOrientation) {
			networkView.RPC ("RemoteRotate", RPCMode.Others, this.gameObject.transform.eulerAngles, this.gameObject.name);
			prevOrientation = transform.eulerAngles;
		}
*/


		// if size (zoom) has changed
//		if (transform.localScale.x != prevSize) {
//			networkView.RPC ("RemoteScale", RPCMode.Others, this.gameObject.transform.localScale.x, this.gameObject.name);
//			prevSize = transform.localScale.x;
//		}

		// if color has changed
		//		if (GetComponent<Renderer> ().material.color != prevColor) {
		//			
		//			Color myColor = GetComponent<Renderer> ().material.color;
		//			networkView.RPC ("RemoteTint", RPCMode.Others, new float[]{myColor.r, myColor.g, myColor.b, myColor.a}, this.gameObject.name);
		//			prevColor = GetComponent<Renderer> ().material.color;
		//		}	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent<Item> () != null) { // 20170222
			print ("hit item");
			if (GetComponent<DraggableXZ> ().isDragging == true) {
				//			Debug.Log (this.gameObject.name + " collided with " + col.gameObject.name);
				col.gameObject.GetComponent<Item> ().canModify = true;
				thatItem = col.gameObject;
			}
		} // 20170222
		if (col.gameObject == floor) { // 20170222
//			print ("hit floor"); // 20170222
			this.GetComponent<Rigidbody> ().useGravity = true; // 20170222
		} // 20170222
	}

	void OnCollisionExit(Collision col){
		//		print ("col.gameObject: " + col.gameObject.name + " | thatitem: " + thatItem.name + " | this.gameObject: " + this.gameObject.name);
		if (col.gameObject == thatItem) {
			//			print ("col.gameObject " + col.gameObject.name);
			StartCoroutine (WaitAndDectivateAgain ());
		}
	}

	IEnumerator WaitAndDectivateAgain(){
		if (thatItem != this.gameObject) {
			yield return new WaitForSeconds (1.4f);
			thatItem = null;
		}
	}
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	public NetworkView networkView;
	Vector3 prevPos;
	Vector3 prevOrientation;
	float prevSize;
	Color prevColor;
	public bool canModify = false;
	GameObject thatItem = null;

	void Start () {
		prevPos = this.gameObject.transform.position;
		prevOrientation = this.transform.eulerAngles;
		prevSize = this.transform.localScale.x;
		prevColor = this.gameObject.GetComponent<Renderer> ().material.color;
		GameObject manager = Spawner.instance.manager;
		networkView = manager.GetComponent<NetworkView> ();

		GameManager.items.Add (this.gameObject.name, this.gameObject);
	}

	void Update () {


		// if position has changed
		if (transform.position != prevPos) {
//			if (transform.position.y < 0.5f) {
//				transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
//			}
			networkView.RPC ("RemoteMove", RPCMode.Others, this.gameObject.transform.position, this.gameObject.name);
			prevPos = transform.position;
		}
	

		// if orientation (rotation) has changed
		if (transform.eulerAngles != prevOrientation) {
			networkView.RPC ("RemoteRotate", RPCMode.Others, this.gameObject.transform.eulerAngles, this.gameObject.name);
			prevOrientation = transform.eulerAngles;
		}

		// if size (zoom) has changed
		if (transform.localScale.x != prevSize) {
			networkView.RPC ("RemoteScale", RPCMode.Others, this.gameObject.transform.localScale.x, this.gameObject.name);
			prevSize = transform.localScale.x;
		}

		// if color has changed
		if (GetComponent<Renderer> ().material.color != prevColor) {
			
			Color myColor = GetComponent<Renderer> ().material.color;
			networkView.RPC ("RemoteTint", RPCMode.Others, new float[]{myColor.r, myColor.g, myColor.b, myColor.a}, this.gameObject.name);
			prevColor = GetComponent<Renderer> ().material.color;
		}	
	}

	void OnCollisionEnter(Collision col){
		if (GetComponent<DraggableXZ> ().isDragging == true) {
//			Debug.Log (this.gameObject.name + " collided with " + col.gameObject.name);
			col.gameObject.GetComponent<Item> ().canModify = true;
			thatItem = col.gameObject;
		}
	}

	void OnCollisionExit(Collision col){
//		print ("col.gameObject: " + col.gameObject.name + " | thatitem: " + thatItem.name + " | this.gameObject: " + this.gameObject.name);
		if (col.gameObject == thatItem) {
//			print ("col.gameObject " + col.gameObject.name);
			StartCoroutine (WaitAndDectivateAgain ());
		}
	}

	IEnumerator WaitAndDectivateAgain(){
		if (thatItem != this.gameObject) {
			yield return new WaitForSeconds (1.4f);
			thatItem = null;
		}
	}
}

*/