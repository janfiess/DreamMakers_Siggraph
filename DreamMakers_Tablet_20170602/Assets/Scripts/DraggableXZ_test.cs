﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DraggableXZ_test : MonoBehaviour{
	GameObject hitGameObject;
	public bool isDragging = false;


	void FixedUpdate(){
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mPosScreen = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mPosScreen);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject == gameObject) {
					hitGameObject = hit.collider.gameObject;
					isDragging = true;
				}
			}
		}

		if (Input.GetMouseButton(0)){
			if (isDragging == true) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit)){
					Vector3 newPos = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
					hitGameObject.transform.position = newPos;
				}
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			if (isDragging == true) {
				isDragging = false;
				StartCoroutine (WaitAndActivateAgain ());
			}
		}
	}

	IEnumerator WaitAndActivateAgain(){
		yield return new WaitForSeconds (0.4f);
	}
}

