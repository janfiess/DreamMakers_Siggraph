using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiontest : MonoBehaviour {
	
	void OnCollisionEnter(Collision col){
		if (GetComponent<DraggableXZ_test> ().isDragging == true) {
			Debug.Log (this.gameObject.name + " collided with " + col.gameObject.name);
		}
	}
}
