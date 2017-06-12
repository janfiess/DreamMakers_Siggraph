// attached to every interactable item´s prtefab
// When an item is hit by another intaractable item, this script tells 

using System.Collections;
using UnityEngine;

public class Game_ItemCollide : MonoBehaviour
{
    Game_Combine combiner;
    Game_Item item;
    void Start()
    {
        combiner = Game_ReferenceManager.Instance.combiner;
        item = GetComponent<Game_Item>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit_activeGameObject = this.gameObject;
        GameObject hit_passiveGameObject = collision.gameObject;
        // every interactable item is tagged with "Item"
        if (hit_passiveGameObject.tag == "Item")
        {

            // add gravity to the own item´s Rigidbody as soon as it hits something
            if (GetComponent<Rigidbody>().useGravity == false)
            {
                GetComponent<Rigidbody>().useGravity = true;
            }


            // add gravity to the collided item´s Rigidbody if it hasn't been grabbed yet
            if (hit_passiveGameObject.GetComponent<Rigidbody>().useGravity == false)
            {
                hit_passiveGameObject.GetComponent<Rigidbody>().useGravity = true;
            }

            // combine items in Tutorial_Combine.cs
            combiner.MergeItems(hit_activeGameObject, hit_passiveGameObject); // is called at both collision partners  
        }

    }
}