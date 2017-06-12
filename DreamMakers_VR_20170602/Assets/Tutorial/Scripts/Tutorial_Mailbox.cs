// this script is attached to the Mailbox prefab
// the scenario completes if the right item hits the tube´s Collider
// In the tutorial the Manager_Globa's script's Global_Manager.cs is triggered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Mailbox : MonoBehaviour
{
    public Transform resultPosition; // where you put the object to
    Vector3 pointToAttachResultItem;
    public Tutorial_Gameplay gameplayManager;

    GameObject mailbox_tootltip;
    bool mailboxSentAway = false;
    Global_Feedback feedbackManager;

    public GameObject smoke;


    void Start()
    {
        pointToAttachResultItem = resultPosition.position;
        // pointToAttachResultItem = new Vector3(-0.335f, 0.486f, -0.444f);
        gameplayManager = Tutorial_ReferenceManager.Instance.gameplayManager;

        mailbox_tootltip = Tutorial_ReferenceManager.Instance.mailbox_tooltip;
        feedbackManager = Global_ReferenceManager.Instance.feedbackManager;
    }
    void OnTriggerEnter(Collider otherCollider)
    {
        // function should be only triggered if the hit item is an interactable item
        if (otherCollider.gameObject.GetComponent<Tutorial_Item>() != null)
        {
            // check if the prefab of the hit object equals the final prefab
            if (otherCollider.gameObject.GetComponent<Tutorial_Item>().prefab == gameplayManager.finalObject)
            {
                GameObject hitItem_prefab = otherCollider.gameObject.GetComponent<Tutorial_Item>().prefab;
                // destroy hit item
                Destroy(otherCollider.gameObject);
                // instantiate again at the right place (prevent flicker)
                GameObject finalItemInBox = Instantiate(hitItem_prefab, pointToAttachResultItem, new Quaternion(0, 0, 0, 0));
                finalItemInBox.transform.parent = gameObject.transform;
                finalItemInBox.transform.position = resultPosition.position;




                // remove tooltip from final item
                if (finalItemInBox.transform.childCount > 0)
                {
                    Destroy(finalItemInBox.transform.GetChild(0).gameObject);
                }
                // remove tooltip from mailbox
                if (mailbox_tootltip != null)
                {
                    Destroy(mailbox_tootltip);
                }

                // small down and destroy mailbox
                if (mailboxSentAway == false)
                {
                    StartCoroutine(WaitThenSendMailboxAway());
                    mailboxSentAway = true;

                    GameObject tubeParticles = Instantiate(smoke, resultPosition.position, new Quaternion(0, 0, 0, 0));
                    tubeParticles.transform.parent = gameObject.transform;
                    
                }


            }
        }


    }

    IEnumerator WaitThenSendMailboxAway()
    {
        // feedbackManager.OnEnteredTube();
        gameplayManager.FinalCombinationOfScenarioSucceded();

        yield return new WaitForSeconds(2.0f);
        print("Send Mailbox away");
        GetComponent<Animator>().SetTrigger("DropMailbox");
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);

        yield return null;
    }
}
