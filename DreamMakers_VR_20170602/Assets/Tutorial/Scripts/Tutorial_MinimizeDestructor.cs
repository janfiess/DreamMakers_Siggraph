using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_MinimizeDestructor : MonoBehaviour
{
    // called eg. from Tutorial_Combine.cs: MergeItems() and from Tutorial_Gameplay.cs: WaitThenHideInteractableItems()
    public void MinimizeAndDestroy(GameObject item)
    {
		print("minimizeDestructor in MinimizeAndDestroy");
        StartCoroutine(ScaleDownAndDestroy(item));
    }

    /* Scale animation: Scale down ingredient */
    IEnumerator ScaleDownAndDestroy(GameObject item)
    {
        Vector3 fromSize = item.transform.localScale;
        Vector3 toSize = Vector3.zero;
        // deactivate collider for preventing further collisions, not needed for envelope :-)
        if(item.GetComponent<Collider>() != null){
            item.GetComponent<Collider>().enabled = false;
        }
        
        // Scale down animation
        float val_duration = 0.3f;
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / val_duration)
        {
            item.transform.localScale = Vector3.Lerp(fromSize, toSize, t);
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        item.transform.localScale = toSize;
        yield return new WaitForEndOfFrame();

        // Destroy
        if (toSize == Vector3.zero)
        {
            Destroy(item);
        }
    }
}
