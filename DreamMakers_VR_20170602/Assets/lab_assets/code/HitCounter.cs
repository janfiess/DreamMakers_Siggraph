// attached to the Manager GameObject which is always in scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitCounter : MonoBehaviour
{
    public bool collided = false;
    private HitManager hitManager;
    public GameObject managerObject;
    bool isCounted = false;



    void Update()
    {
        if (managerObject.activeSelf != false)
        {
            hitManager = Game_ReferenceManager.Instance.labHitManager;
            if (isCounted == false)
            {
                hitManager.cagesCount++;
                isCounted = true;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        



        if (hitManager != null)
        {
            GetComponent<AudioSource>().Play();
            if (!collided)
            {
                print("collided !!!");
                collided = true;
                hitManager.OnCageHit();
            }
        }
    }
}
