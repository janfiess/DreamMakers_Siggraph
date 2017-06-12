// attached to the Manager GameObject which is always available in scene


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ItemSpawner : MonoBehaviour
{

    public GameObject particlesOnSpawnItem_prefab; // This is a container with particle system (activated at start). It parents the item and is deleted right after
    public GameObject interactableItems_container;
    public Global_Feedback feedbackManager;
  
    Game_Items_Combinations Items_prefabs;
    // never spawn under the table
    public Game_TableTrigger tableTrigger;
    Game_GameplayManager gameplayManager;
    public GameObject emptyPrefab;
    Global_NetworkingManager networkManager;
  

    void Start()
    {
        // Acces to the prefabs
        Items_prefabs = GetComponent<Game_Items_Combinations>();
        gameplayManager = GetComponent<Game_GameplayManager>();
        // SpawnItem(emptyPrefab, GetRandomSpawnPosition());
        networkManager = Global_ReferenceManager.Instance.networkManager;

    }

    // called in Tutorial_Combine.cs ( and in Tutorial only: OpenEnvelope.cs: RiseEnvelopeWithDelay() )
    public void SpawnItem(GameObject resultItem_prefab, Vector3 spawnPosition)
    {
        // if spawn position is unter the table, lift the item up
        // not in Tutorial, because there is no table

        spawnPosition = new Vector3(spawnPosition.x, Random.Range(0.6f, 1.7f), spawnPosition.z);

        if (tableTrigger != null)
        {
            if (tableTrigger.isTableIntersected == true)
            {
                print("spawnPosition before: " + spawnPosition);
                spawnPosition.y = 1.5f;
                print("spawnPosition after: " + spawnPosition);
            }
        }

        // prevent spawning under the floor
        if (spawnPosition.y < 0.2f) spawnPosition.y = 0.2f;

        // Instantiate item and particles
        GameObject particlesOnSpawnItem = Instantiate(particlesOnSpawnItem_prefab, spawnPosition, Quaternion.identity);
        GameObject newItem = Instantiate(resultItem_prefab, spawnPosition, Quaternion.identity);
        // parent all items to a central container, later it can be deleted more easily
        newItem.transform.parent = interactableItems_container.transform;

        newItem.name = newItem.name + gameplayManager.itemNumber;
        gameplayManager.itemNumber++;

        // networkManager.spawnItemAsResult_Sender(resultItem_prefab.name, newItem.transform.position);
        feedbackManager.OnSpawnNewItem();
        
        Destroy(particlesOnSpawnItem, 3.0f);
    }


    void Update()
    {
        /**********************************************************************************
         * for debug purposes: items can also be spawned using a key (if there is no network)
        *********************************************************************************** */

        if (Input.GetKeyDown(KeyCode.Q)) { SpawnItem(Items_prefabs.horseshoe_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.H)) { SpawnItem(Items_prefabs.heart_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.F)) { SpawnItem(Items_prefabs.feather_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.C)) { SpawnItem(Items_prefabs.canon_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.A)) { SpawnItem(Items_prefabs.anchor_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.N)) { SpawnItem(Items_prefabs.fish_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.B)) { SpawnItem(Items_prefabs.bikini_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.M)) { SpawnItem(Items_prefabs.moon_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.S)) { SpawnItem(Items_prefabs.sun_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.R)) { SpawnItem(Items_prefabs.rainClouds_prefab, GetRandomSpawnPosition()); };

        if (Input.GetKeyDown(KeyCode.W)) { SpawnItem(Items_prefabs.horse_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.V)) { SpawnItem(Items_prefabs.bird_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.O)) { SpawnItem(Items_prefabs.pirateShip_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.J)) { SpawnItem(Items_prefabs.mermaid_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.U)) { SpawnItem(Items_prefabs.rainbow_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.L)) { SpawnItem(Items_prefabs.pillow_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.X)) { SpawnItem(Items_prefabs.star_prefab, GetRandomSpawnPosition()); };

        if (Input.GetKeyDown(KeyCode.E)) { SpawnItem(Items_prefabs.unicorn_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.G)) { SpawnItem(Items_prefabs.pegasus_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.Z)) { SpawnItem(Items_prefabs.phoenix_prefab, GetRandomSpawnPosition()); };
        if (Input.GetKeyDown(KeyCode.I)) { SpawnItem(Items_prefabs.spaceShip_prefab, GetRandomSpawnPosition()); };

    }

    // for debug purposes: If items are spawned using a key, their position is set randomly
    Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(-1.2f, 1.2f),
            Random.Range(0.6f, 1.7f),
            Random.Range(-1.2f, 1.2f)
        );
    }
}