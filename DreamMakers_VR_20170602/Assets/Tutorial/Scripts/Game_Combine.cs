// attached to the Manager GameObject which is always avalable in scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game_Combine : MonoBehaviour
{



    public Game_ItemSpawner itemSpawner;
    public GameObject wrongCombinationParticles_prefab;
    Game_GameplayManager gameplay;

    [HideInInspector] public static GameObject[] collisionObjects = new GameObject[2];
    [HideInInspector] public Game_Items_Combinations itemsAndCombinations;
    [HideInInspector] public Game_Text textManager;
    public Global_Feedback feedbackManager;
    [ReadOnly] public GameObject[] currentCombination = new GameObject[3];


    Game_MinimizeDestructor minimizeDestructor;
    float spawnDistance = 0.0f;
    float prev_TriggerTime = 0.0f;
    public Global_NetworkingManager networkManager;




    void Awake()
    {
        // get references to dependant scripts
        gameplay = GetComponent<Game_GameplayManager>();
        itemsAndCombinations = GetComponent<Game_Items_Combinations>();
        textManager = GetComponent<Game_Text>();
        minimizeDestructor = GetComponent<Game_MinimizeDestructor>();

        // objects and their ingredients
        gameplay.validCombinationsInThisScenario_list.Clear();
    }


    public void MergeItems(GameObject hit_activeGameObject, GameObject hit_passiveGameObject)
    {
        // This function is called by both colliding members. This timer prevents this function from being called more than once
        float current_TriggerTime = Time.time;
        if ((current_TriggerTime - prev_TriggerTime) > 0.5f)
        {
            // at least one of the colliding items has be grabbed before for preventing that the tablet player is combining without the VR player
            if (hit_activeGameObject.GetComponent<Game_Item>().hasBeenGrabbedAtLeastOnce == true || hit_passiveGameObject.GetComponent<Game_Item>().hasBeenGrabbedAtLeastOnce == true)
            {



                prev_TriggerTime = current_TriggerTime;

                print("hit_activeGameObject: " + hit_activeGameObject + " |   hit_passiveGameObject: " + hit_passiveGameObject);
                print("hit_activeGameObject grabbed: " + hit_activeGameObject.GetComponent<Game_Item>().hasBeenGrabbedAtLeastOnce + " | hit_passiveGameObject grabbed: " + hit_passiveGameObject.GetComponent<Game_Item>().hasBeenGrabbedAtLeastOnce);
      
                // find out midpoint of ingredients
                Vector3 midpoint = (hit_activeGameObject.transform.position + hit_passiveGameObject.transform.position) / 2;

                // spawn a little bit in front of the midpoint (1m * spawnDistance)
                Vector3 spawnPosition = midpoint + Camera.main.transform.forward * spawnDistance;


                // If the active hit object is psrt of one of the valid combinations (unicorn[] or horse[] - go through both of them) 
                // at position 1, store its combination (eg. horse[]) in the first list, if the aactive hit object belongs to a valid combination
                // at position 2, store its combination in the second list  
                List<GameObject[]> temporaryStorageList_AtCombinationRuleArrayPos1 = new List<GameObject[]>();
                List<GameObject[]> temporaryStorageList_AtCombinationRuleArrayPos2 = new List<GameObject[]>();
                GameObject itemToSpawn = null;
                bool hittingItem_ACTIVE_matches = false;
                bool hitItem_PASSIVE_matches = false;

                print(gameplay.validCombinationsInThisScenario_list[0][0]);

                // look for the _ACTIVE_ collision object in all 2 combination arrays allowed in this level 
                // example Unicorn: The active object is the pencil. So look up, if the pencil is in 1. (unicorn, horse, pencil) and 2. (horse, horseshoe, heart)
                foreach (GameObject[] aCombinationInThisScenario in gameplay.validCombinationsInThisScenario_list)
                {  // Look for Gameobject array  in list
                   // combinedItemArray[0]: result, [1]: ingredient1, [2]: ingredient 2

                    // print("aCombinationInLevel[1]: " + aCombinationInThisScenario[1].name);
                    // print("hit_activeGameObject.GetComponent<Tutorial_Item>().prefab: " + hit_activeGameObject.GetComponent<Tutorial_Item>().prefab.name);

                    print(aCombinationInThisScenario[0].name);


                    if (hit_activeGameObject.GetComponent<Game_Item>().prefab == aCombinationInThisScenario[1])
                    {
                        print(aCombinationInThisScenario[1].name);

                        temporaryStorageList_AtCombinationRuleArrayPos1.Add(aCombinationInThisScenario);
                        hittingItem_ACTIVE_matches = true;
                        //					print ("First Ingredient (hitting item: " + combinedItemArray[1].name + ") found at first array pos");

                    }

                    // print("aCombinationInLevel[2]: " + aCombinationInThisScenario[1].name + " | hit_activeGameObject.GetComponent<Tutorial_Item>().prefab: " + hit_activeGameObject.GetComponent<Tutorial_Item>().prefab.name);
                    if (hit_activeGameObject.GetComponent<Game_Item>().prefab == aCombinationInThisScenario[2])
                    {
                        print(aCombinationInThisScenario[2].name);

                        temporaryStorageList_AtCombinationRuleArrayPos2.Add(aCombinationInThisScenario);
                        hittingItem_ACTIVE_matches = true;
                        //					print ("First Ingredient (hitting item: " + combinedItemArray[2].name + ") found at second array pos");

                    }
                }

                // checking if the object being hit (the _PASSIVE_ collision item) is a partner in the array
                foreach (GameObject[] reducedItemArray in temporaryStorageList_AtCombinationRuleArrayPos1)
                {
                    print(reducedItemArray[0].name);


                    if (reducedItemArray[2] == hit_passiveGameObject.GetComponent<Game_Item>().prefab)
                    {
                        currentCombination = reducedItemArray;
                        hitItem_PASSIVE_matches = true;
                        //					print ("Secound Ingredient (hit item: " + reducedItemArray[2].name + ") found at second array pos");

                    }
                }

                foreach (GameObject[] reducedItemArray in temporaryStorageList_AtCombinationRuleArrayPos2)
                {
                    if (reducedItemArray[1] == hit_passiveGameObject.GetComponent<Game_Item>().prefab)
                    {
                        currentCombination = reducedItemArray;
                        hitItem_PASSIVE_matches = true;
                        //					print ("Secound Ingredient (hit item: " + reducedItemArray[2].name + ") found at first array pos");

                    }
                }


                // if at least 1 item was not found give vibration feedback at the controller of the wrong item

                if (hittingItem_ACTIVE_matches == false || hitItem_PASSIVE_matches == false)
                {
                    // vibration feedback on the controller with the wrong item
                    feedbackManager.OnWrongCombination();
                    
                    print(" Spawn monster");
                    GameObject wrongCombinationParticles = Instantiate(wrongCombinationParticles_prefab, spawnPosition, Quaternion.identity);
                    minimizeDestructor.MinimizeAndDestroy(hit_activeGameObject);
                    minimizeDestructor.MinimizeAndDestroy(hit_passiveGameObject);

                     networkManager.DestroyItem_Sender(hit_activeGameObject.name);
                      networkManager.DestroyItem_Sender(hit_passiveGameObject.name);
                    Destroy(wrongCombinationParticles, 4.0f);
                }

                // if 2 matching ingredients have collided, spawn the result object

                if (hittingItem_ACTIVE_matches == true && hitItem_PASSIVE_matches == true)
                {
                    itemToSpawn = currentCombination[0];
                    print("Item to spawn: " + itemToSpawn.name);

                    feedbackManager.OnRightCombination();

                    // // delete items locally and over network

                    // scale down ingredients, then scale up result item, then delete item (gameobject, fromsize, toSize)
                    minimizeDestructor.MinimizeAndDestroy(hit_activeGameObject);
                    minimizeDestructor.MinimizeAndDestroy(hit_passiveGameObject);

                    networkManager.DestroyItem_Sender(hit_passiveGameObject.name);
                    networkManager.DestroyItem_Sender(hit_activeGameObject.name);

                    itemSpawner.SpawnItem(itemToSpawn, spawnPosition);
                    





                    // Level is completed whenthe final object (itemToSpawn) hits the mailbox collider




                    /*
                    // if the currently combined (itemToSpawn) item is the finalItem, go to the next level
                    if (itemToSpawn == gameplay.finalObject)
                    {
                        print("before calling gameplay.FinalCombinationOfScenarioSucceded();");
                        gameplay.FinalCombinationOfScenarioSucceded();
                    }
                     */

                }
            }
        }
    }
}