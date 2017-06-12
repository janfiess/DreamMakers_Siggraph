// this script is attached to every grabbable item (prefabs)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Item : MonoBehaviour
{
    Tutorial_Items_Combinations itemsAndCombinations;

    [ReadOnly] public GameObject prefab; // check to which prefab this gameobject belongs even after instantiation
    void Start()
    {
        string thisItemName = this.gameObject.name;
        string prefabName;
        if (thisItemName.Contains("("))
        {
            prefabName = thisItemName.Substring(0, thisItemName.IndexOf("(")); // get the prefab's name by removing the "(Instance)"
        }
        else prefabName = thisItemName; // if the item is not instantiated from a prefab but just dragged into the scene

        prefab = Tutorial_Items_Combinations.itemPrefabs[prefabName];
        // print("Prefab Name: " + prefab.name);
    }
}
