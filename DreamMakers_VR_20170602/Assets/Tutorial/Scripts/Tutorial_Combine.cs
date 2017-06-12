// attached to the Manager GameObject which is always avalable in scene
// Tutorial scene script

using System.Collections.Generic;
using UnityEngine;
public class Tutorial_Combine : MonoBehaviour
{



    public Tutorial_ItemSpawner itemSpawner;
    Tutorial_Gameplay gameplay;

    [HideInInspector] public static GameObject[] collisionObjects = new GameObject[2];
    [HideInInspector] public Tutorial_Items_Combinations itemsAndCombinations;
    [HideInInspector] public Tutorial_Text textManager;
    public Global_Feedback feedbackManager;

    public GameObject letterTooltip_tutorial;
    public GameObject tubeTooltip_tutorial;
    Tutorial_MinimizeDestructor minimizeDestructor;
    float spawnDistance = 0.0f;
    float prev_TriggerTime = 0.0f;




    void Awake()
    {
        // get references to dependant scripts
        gameplay = GetComponent<Tutorial_Gameplay>();
        itemsAndCombinations = GetComponent<Tutorial_Items_Combinations>();
        textManager = GetComponent<Tutorial_Text>();
        minimizeDestructor = GetComponent<Tutorial_MinimizeDestructor>();

        // objects and their ingredients
        gameplay.validCombinationsInThisScenario_list.Clear();
    }


    public void MergeItems(GameObject hit_activeGameObject, GameObject hit_passiveGameObject)
    {
        // This function is called by both colliding members. This timer prevents this function from being called more than once
        float current_TriggerTime = Time.time;
        if ((current_TriggerTime - prev_TriggerTime) > 0.5f)
        {
            prev_TriggerTime = current_TriggerTime;

            print("hit_activeGameObject: " + hit_activeGameObject + " |   hit_passiveGameObject: " + hit_passiveGameObject);
            
            // find out midpoint of ingredients
            Vector3 midpoint = (hit_activeGameObject.transform.position + hit_passiveGameObject.transform.position) / 2;

            // spawn a little bit in front of the midpoint (1m * spawnDistance)
            Vector3 spawnPosition = midpoint + Camera.main.transform.forward * spawnDistance;

            GameObject[] currentCombination = new GameObject[3];

            // If the active hit object is psrt of one of the valid combinations (unicorn[] or horse[] - go through both of them) 
            // at position 1, store its combination (eg. horse[]) in the first list, if the aactive hit object belongs to a valid combination
            // at position 2, store its combination in the second list  
            List<GameObject[]> temporaryStorageList_AtCombinationRuleArrayPos1 = new List<GameObject[]>();
            List<GameObject[]> temporaryStorageList_AtCombinationRuleArrayPos2 = new List<GameObject[]>();
            GameObject itemToSpawn = null;
            bool hittingItem_ACTIVE_matches = false;
            bool hitItem_PASSIVE_matches = false;

            // look for the _ACTIVE_ collision object in all 2 combination arrays allowed in this level 
            // example Unicorn: The active object is the pencil. So look up, if the pencil is in 1. (unicorn, horse, pencil) and 2. (horse, horseshoe, heart)
            foreach (GameObject[] aCombinationInThisScenario in gameplay.validCombinationsInThisScenario_list)
            {  // Look for Gameobject array  in list
               // combinedItemArray[0]: result, [1]: ingredient1, [2]: ingredient 2

                // print("aCombinationInLevel[1]: " + aCombinationInThisScenario[1].name);
                // print("hit_activeGameObject.GetComponent<Tutorial_Item>().prefab: " + hit_activeGameObject.GetComponent<Tutorial_Item>().prefab.name);

                if (hit_activeGameObject.GetComponent<Tutorial_Item>().prefab == aCombinationInThisScenario[1])
                {
                    temporaryStorageList_AtCombinationRuleArrayPos1.Add(aCombinationInThisScenario);
                    hittingItem_ACTIVE_matches = true;
                    //					print ("First Ingredient (hitting item: " + combinedItemArray[1].name + ") found at first array pos");
                }

                // print("aCombinationInLevel[2]: " + aCombinationInThisScenario[1].name + " | hit_activeGameObject.GetComponent<Tutorial_Item>().prefab: " + hit_activeGameObject.GetComponent<Tutorial_Item>().prefab.name);
                if (hit_activeGameObject.GetComponent<Tutorial_Item>().prefab == aCombinationInThisScenario[2])
                {
                    temporaryStorageList_AtCombinationRuleArrayPos2.Add(aCombinationInThisScenario);
                    hittingItem_ACTIVE_matches = true;
                    //					print ("First Ingredient (hitting item: " + combinedItemArray[2].name + ") found at second array pos");
                }
            }

            // checking if the object being hit (the _PASSIVE_ collision item) is a partner in the array
            foreach (GameObject[] reducedItemArray in temporaryStorageList_AtCombinationRuleArrayPos1)
            {
                if (reducedItemArray[2] == hit_passiveGameObject.GetComponent<Tutorial_Item>().prefab)
                {
                    currentCombination = reducedItemArray;
                    hitItem_PASSIVE_matches = true;
                    //					print ("Secound Ingredient (hit item: " + reducedItemArray[2].name + ") found at second array pos");
                }
            }

            foreach (GameObject[] reducedItemArray in temporaryStorageList_AtCombinationRuleArrayPos2)
            {
                if (reducedItemArray[1] == hit_passiveGameObject.GetComponent<Tutorial_Item>().prefab)
                {
                    currentCombination = reducedItemArray;
                    hitItem_PASSIVE_matches = true;
                    //					print ("Secound Ingredient (hit item: " + reducedItemArray[2].name + ") found at first array pos");
                }
            }

  

            // if 2 matching ingredients have collided, spawn the result object

            if (hittingItem_ACTIVE_matches == true && hitItem_PASSIVE_matches == true)
            {
                itemToSpawn = currentCombination[0];
                print("Item to spawn: " + itemToSpawn.name);

                // feedbackManager.OnRightCombination();



                // // delete items locally and over network

                // scale down ingredients, then scale up result item, then delete item (gameobject, fromsize, toSize)
                minimizeDestructor.MinimizeAndDestroy(hit_activeGameObject);
                minimizeDestructor.MinimizeAndDestroy(hit_passiveGameObject);

                // networkView.RPC ("DestroyItem", RPCMode.All, hit_passiveGameObject.name);
                // networkView.RPC ("DestroyItem", RPCMode.All, hit_activeGameObject.name);

                itemSpawner.SpawnItem(itemToSpawn, spawnPosition);

                // In Tutorial Scene: Remove tooltip from letter if exists
                if (letterTooltip_tutorial != null)
                {
                    Destroy(letterTooltip_tutorial);
                }

                // Tutorial only: Make tube tooltip visible
                tubeTooltip_tutorial.SetActive(true);



                // Level is completed whenthe final object (itemToSpawn) hits the mailbox collider
            }
        }
    }
}