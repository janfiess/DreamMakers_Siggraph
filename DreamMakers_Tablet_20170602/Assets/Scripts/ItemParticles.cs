using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticles : MonoBehaviour {
	public GameObject particles_item;


	void Awake(){

		GameObject prefab = (GameObject)Resources.Load("Prefabs/SpawnItem_Particles", typeof(GameObject));
		particles_item = Instantiate (prefab, this.transform.position, Quaternion.identity);
		particles_item.transform.parent = transform;

		StartCoroutine (DestroyMySelf ());
	}

	IEnumerator DestroyMySelf (){
		yield return new WaitForSeconds (2.0f);
		Destroy (particles_item);
	}
}

