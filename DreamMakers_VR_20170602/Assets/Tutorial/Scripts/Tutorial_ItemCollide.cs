// attached to every interactable item´s prtefab
// When an item is hit by another intaractable item, this script tells 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ItemCollide : MonoBehaviour
{
	Tutorial_Combine combiner;
    void Start()
    {
		combiner = Tutorial_ReferenceManager.Instance.combiner;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit_activeGameObject = this.gameObject;
        GameObject hit_passiveGameObject = collision.gameObject;
        // every interactable item is tagged with "Item"
        if (hit_passiveGameObject.tag == "Item")
        {
            // remove tooltip from the collided item
            if (hit_passiveGameObject.transform.childCount > 0)
            {
                Destroy(hit_passiveGameObject.transform.GetChild(0).gameObject);
            }

            // add gravity to the collided item´s Rigidbody if it hasn't been grabbed yet
            if (hit_passiveGameObject.GetComponent<Rigidbody>().useGravity == false)
            {
                hit_passiveGameObject.GetComponent<Rigidbody>().useGravity = true;
                hit_passiveGameObject.GetComponent<ConstantForce>().enabled = true;
            }

            // combine items in Tutorial_Combine.cs
            combiner.MergeItems(hit_activeGameObject, hit_passiveGameObject); // is called at both collision partners  
        }
    }
}