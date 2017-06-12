//script attached to Game_TableTrigger.cs
// check if the Table is intersected by an interactable item. If an item is spawned indide this area,
// Tutorial_ItemSpawner.cs will lift the position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_TableTrigger : MonoBehaviour
{

    [HideInInspector] public bool isTableIntersected = false;
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Item")
        {
            isTableIntersected = true;
            // print("isTableIntersected = true");
        }
    }
    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Item")
        {
            isTableIntersected = false;
        }
    }
}
