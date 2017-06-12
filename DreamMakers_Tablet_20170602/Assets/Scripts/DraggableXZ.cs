

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DraggableXZ : MonoBehaviour{

	public Camera GuiCam;
	public Camera VrCam;
	public GameObject VrTexture;
	Vector3 vrPos;


	GameObject hitGameObject;
	public GameObject networkManager;
	public bool isDragging = false;

	void Start(){
		networkManager = Spawner.Instance.manager;
		GuiCam = Spawner.Instance.GuiCam;
		VrCam = Spawner.Instance.VrCam;
		VrTexture = Spawner.Instance.VrTexture;
	}




	/*
	 * */


	void FixedUpdate(){
		if (Input.GetMouseButtonDown (0)) {
			


			Vector2 mPosScreen = Input.mousePosition;
			RaycastHit hit;
			Ray ray = GuiCam.ScreenPointToRay (mPosScreen); //

//			Vector3 vrPos = Vector3.zero; //

			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == VrTexture) { //
				Vector3 texPos = hit.textureCoord; //
				ray = VrCam.ViewportPointToRay(texPos); //
				if (Physics.Raycast (ray, out hit)) { //

					vrPos = hit.point; //
//					print(vrPos);

					if (hit.collider.gameObject == this.gameObject) {

						// set canModify == false on every client except the active one 
						networkManager.GetComponent<NetworkView>().RPC ("CanNotModify", RPCMode.Others, this.gameObject.name); // 20170215


						hitGameObject = hit.collider.gameObject;
						isDragging = true;

						GetComponent<Item> ().canModify = true;
					}
				}
			}
		}

		if (Input.GetMouseButton(0)){
			if (isDragging == true) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == VrTexture) { //

					Vector3 texPos = hit.textureCoord;
					print (texPos);
					ray = VrCam.ViewportPointToRay(texPos);

					if (Physics.Raycast (ray, out hit)) {
						Vector3 newPos = new Vector3 (hit.point.x, this.transform.position.y, hit.point.z);
						hitGameObject.transform.position = newPos;
						vrPos = hit.point; //
					}
				}
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			if (isDragging == true) {
				isDragging = false;
				networkManager.GetComponent<NetworkView>().RPC ("OnlyVrCanModify", RPCMode.Others, this.gameObject.name); // 20170215

			}
		}
	}
}

