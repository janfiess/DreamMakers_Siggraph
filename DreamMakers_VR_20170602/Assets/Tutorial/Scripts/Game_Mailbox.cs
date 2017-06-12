// this script is attached to the Mailbox prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mailbox : MonoBehaviour
{
    public Transform resultPosition; // where you put the object to
    Vector3 pointToAttachResultItem;
    public Game_GameplayManager gameplayManager;
    bool mailboxSentAway = false;
    Global_Feedback feedbackManager;

    public GameObject smoke;
    public GameObject tubeEffect;



    void Start()
    {
        pointToAttachResultItem = resultPosition.position;
        // pointToAttachResultItem = new Vector3(-0.335f, 0.486f, -0.444f);
        gameplayManager = Game_ReferenceManager.Instance.gameplayManager;
        feedbackManager = Global_ReferenceManager.Instance.feedbackManager;
    }
    void OnTriggerEnter(Collider otherCollider)
    {
        // function should be only triggered if the hit item is an interactable item
        if (otherCollider.gameObject.GetComponent<Game_Item>() != null)
        {
            // check if the prefab of the hit object equals the final prefab
            if (otherCollider.gameObject.GetComponent<Game_Item>().prefab == gameplayManager.finalObject)
            {
                GameObject hitItem_prefab = otherCollider.gameObject.GetComponent<Game_Item>().prefab;
                // destroy hit item
                Destroy(otherCollider.gameObject);

                // instantiate again at the right place (prevent flicker)
                GameObject finalItemInBox = Instantiate(hitItem_prefab, pointToAttachResultItem, new Quaternion(0, 0, 0, 0));
                finalItemInBox.transform.parent = gameObject.transform;
                finalItemInBox.transform.position = resultPosition.position;


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

        gameplayManager.FinalCombinationOfScenarioSucceded();
        yield return new WaitForSeconds(2.0f);
        print("Drop tube");
        feedbackManager.OnEnteredTube();
        //GetComponent<Animator>().SetTrigger("DropMailbox");
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);


        yield return null;


    }
}
