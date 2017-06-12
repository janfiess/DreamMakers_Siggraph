// attached to the Manager GameObject which is always avalable in scene


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ItemSpawner : MonoBehaviour
{

    public GameObject particlesOnSpawnItem_prefab; // This is a container with particle system (activated at start). It parents the item and is deleted right after
    public GameObject interactableItems_container;
    public Global_Feedback feedbackManager;

  
    // called in Tutorial_Combine.cs ( and in Tutorial only: Tutorial_OpenEnvelope.cs: RiseEnvelopeWithDelay() )
    public void SpawnItem(GameObject resultItem_prefab, Vector3 spawnPosition)
    {
        // prevent spawning under the floor
        if(spawnPosition.y < 0.2f) spawnPosition.y = 0.2f;

        // Instantiate item and particles
        GameObject particlesOnSpawnItem = Instantiate(particlesOnSpawnItem_prefab, spawnPosition, Quaternion.identity);
        GameObject newItem = Instantiate(resultItem_prefab, spawnPosition, Quaternion.identity);
        // parent all items to a central container, later it can be deleted more easily
        newItem.transform.parent = interactableItems_container.transform;

        // feedbackManager.OnSpawnNewItem();
        Destroy(particlesOnSpawnItem, 3.0f);
    }
}