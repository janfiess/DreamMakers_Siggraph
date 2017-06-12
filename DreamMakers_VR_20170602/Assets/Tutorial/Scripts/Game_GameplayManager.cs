// this script is attached to the Manager gameobject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_GameplayManager : MonoBehaviour
{
    public GameObject interactableItems; // items shall be placed into this empty gameobject
    Game_MinimizeDestructor minimizeDestructor;
    Game_Items_Combinations itemsAndCombinations;
    [HideInInspector] public bool isTutorialScene = true;

    public GameObject tube_placeholder;
    public GameObject envelope_container;
    [HideInInspector] public int itemNumber = 0;  // every instance of newy spawned objects gets an incrementing number




    [HideInInspector] public int currentLevelNumber = 0; // eg. Pillow is a level 1 theme and unicorn is a level 2 theme
                                                         // The final object of the "Unicorn" combination (GameObject array - Horse + Pecil = Unicorn) is a Unicorn (GameObject); finalCombination.Length = 3	
    [HideInInspector] public GameObject finalObject;

    // Defines which combinations are available in one level: If the final result in the level is a unicorn, 
    // two combination arrays[3] are accepted in this list: Horseshoe + Heart = Horse AND Horse + Pencil = Unicorn.
    // If the final item requires only one combination, this list holds only 1 array

    [ReadOnly] public List<GameObject[]> validCombinationsInThisScenario_list = new List<GameObject[]>();

    public int maxScenarioTime = 120;  // You must not need more time than 120 seconds for 1 scenario (consisting of 1 or 2 combinations)
    public int remainingScenarioTime;
    int totalLevelCount = 2;  // the game has 2 levels
    [HideInInspector] public int totalLevelNumber;
    public float timeUntilNextScenario = 7.0f;
    public float timeBeforeInteractableItemsDisappear = 4.0f;

    [HideInInspector] public bool withinScenarioTime = false;

    public Global_Feedback feedbackManager;
    [HideInInspector] public Game_Text textManager;
    public Global_NetworkingManager networkManager;
    Game_EnvelopeManager envelopeManager;

    // Level 2 can have 2 possible scenarios (generated randomly) dependant of in which level you are:
    // - Result Unicorn (outer list) consists of Horse (inner list) and unicorn (also inner list). Each horse and unicorn are arrays which also store their ingredients
    // - Result Diskobrain (outer list) consists of Desklamp (inner list) and Diskobrain (also inner list). Each Desklamp and Diskobrain are arrays which also store their ingredients
    // - Unicorn example: ListAllPossinleScenariosInThisList = Unicorn = (Unicorn (= Horse + Pencil) + Horse (= Heart + Horseshoe)), Discobrain = (...)
    List<List<GameObject[]>> list_allPossibleScenariosInThisLevel = new List<List<GameObject[]>>();

    // substring_name_finalObject is eg. "Unicorn" - in this script we get the name of the final item prefach (eg. "Item_Unicorn"). This varianle stores the name withous "Item_"
    // This name will be sent to Game_EnvelopeManager.cs when the enveloped is instantiated (Game_EnvelopeManager: RiseEnvelope(substring_name_finalObject) ).
    // The instantiation is done automatically in PrepareScenario()
    // This string is used in Game_EnvelopeManager.LoadLetter(string name_finalObject) for loading the letter texture by Putting "Letter_" in front of name_finalObject, -> so "Letter_Unicorn.jpg" can be loaded
    string substring_name_finalObject;
    void Awake()
    {
        itemsAndCombinations = GetComponent<Game_Items_Combinations>();
        minimizeDestructor = GetComponent<Game_MinimizeDestructor>();



        remainingScenarioTime = maxScenarioTime; // stopwatch in seconds
        totalLevelNumber = totalLevelCount;

        
    }

    void Start()
    {
        envelopeManager = Game_ReferenceManager.Instance.envelopeManager;
        textManager = GetComponent<Game_Text>();




        // determine combination, envelope appears (gamestarts when the envelope is opened)
        // Only in tutorial in Start function. Later, this function is called when the tablet player enters the lab
        PrepareScenario();

        networkManager.HelloFromVR_sender(maxScenarioTime);
    }





    public void PrepareScenario()
    {
        networkManager.PrepareScenarioOnTablet_sender();
        feedbackManager.OnPrepareScenario (); 
        // print("Tutorial_Gameplay.cs: preparescenario");

        switch (currentLevelNumber)
        {
            case 0:
                // copy all possible scenarios of this level into an extra list
                print("Tutorial_Gameplay.cs: You are in Level 1");
                list_allPossibleScenariosInThisLevel = itemsAndCombinations.possibleScenarios_level1; // in Tutorial_Items_Combinations.cs
                // print("list_allPossibleScenariosInThisLevel.Count 2: " + list_allPossibleScenariosInThisLevel.Count);
                break;

            case 1:
                print("Tutorial_Gameplay.cs: You are in Level 2");
                list_allPossibleScenariosInThisLevel = itemsAndCombinations.possibleScenarios_level2; // in Tutorial_Items_Combinations.cs
                break;
        }


        int randomScenarioNumberInThisLevel = Random.Range(0, list_allPossibleScenariosInThisLevel.Count);


        // One possible randomly selected scenario in level 2 is the final result Unicorn (Unicorn here is the name for the list of the available 
        // combinations to make a Unicorn object and a horse object (Both are GameObject arrays in this list), which must be made to master the final object Unicorn
        // if you shall make a unicorn in this scenario, the currently available combinations in the list are the gameObject arrays Unicorn (Horse + Pencil) Horse (horseshoe + heart)
        validCombinationsInThisScenario_list = list_allPossibleScenariosInThisLevel[randomScenarioNumberInThisLevel];



        // The final combination in a scenario is always the first GameObject array in the list validCombinationsInLevel_list 
        // (In order to make the final result "Unicorn", the GameObject array list keeps the GameObject arrays "Unicorn" and "Horse")
        // So the final combination for the scenario "Unicorn" is the GmeObject array "Unicorn"
        GameObject[] finalCombinationInThisScenario = validCombinationsInThisScenario_list[0];
        print("Tutorial_Gameplay: Name of target obj: " + finalCombinationInThisScenario[0].name);


        // The final object of the "Unicorn" combination (GameObject array) is a Unicorn (GameObject); finalCombination.Length = 3
        finalObject = finalCombinationInThisScenario[0];

        // get the right texture for the letter
        string name_finalObject = finalObject.name;  // e. g. "Item_Unicorn"

        substring_name_finalObject = name_finalObject.Substring(name_finalObject.IndexOf("_") + 1); // e. g. "Unicorn"
        print(substring_name_finalObject);

        // Instantiate envelope with animation and parent it to the placeholder. Pass the name of the final object for the Letter texture
        // triggered by the tablet player or on Key press T in EnvelopeManager.cs: RiseEnvelope(substring_name_finalObject);
        envelopeManager.RiseEnvelope(substring_name_finalObject);

        // Stopwatch


        // remainingScenarioTime = maxScenarioTime; // Reset the stopwatch
        //                                          //		print ("remainingTime: " + remainingTime);
        // textManager.scenarioStopwatchText.color = new Color(1, 1, 1, 1);
        // textManager.scenarioStopwatchText.text = "Time: " + remainingScenarioTime.ToString() + "s";

        // remainingScenarioTime = maxScenarioTime; // Reset the stopwatch
        // textManager.scenarioStopwatchText.text = "Time: " + remainingScenarioTime.ToString() + "s";
    }



    // called when the envelope is opened in Tutorial:OpenEnvelope.cs. StartUsing()
    public void StartScenario()
    {
        feedbackManager.OnStartScenario();
        networkManager.StartScenarioOnNetwork_sender();
        print("StartScenario");
        withinScenarioTime = true;

        StartCoroutine(ScenarioTimer(remainingScenarioTime));

        textManager.scenarioStopwatchText.text = "Time: " + remainingScenarioTime.ToString() + "s";
        Color currentColor = textManager.scenarioStopwatchText.color;
        textManager.scenarioStopwatchText.color  = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
           
    }

    IEnumerator ScenarioTimer(int remainingScenarioTime)
    {

        //		stopwatch.text = remainingTime.ToString ();
        while (remainingScenarioTime > 0)
        {
            if (withinScenarioTime == true)
            {
                textManager.scenarioStopwatchText.text = "Time: " + remainingScenarioTime.ToString() + "s";
                yield return new WaitForSeconds(1);
                remainingScenarioTime--;

                if (remainingScenarioTime == 15) feedbackManager.OnSecondsLeft(15);
                if (remainingScenarioTime == 30) feedbackManager.OnSecondsLeft(30);
                if (remainingScenarioTime == 45) feedbackManager.OnSecondsLeft(45);
                if (remainingScenarioTime == 60) feedbackManager.OnSecondsLeft(60);
            }
            else
            {
                yield break;
            }

        }

        if (remainingScenarioTime == 0)
        {
            textManager.scenarioStopwatchText.text = "Time: " + remainingScenarioTime.ToString() + "s";
            textManager.WaitThenFadeOutText_trigger(textManager.scenarioStopwatchText);

            print("Time over");

            feedbackManager.OnTimeOver();
            withinScenarioTime = false;

            // networkView.RPC("ExitScenarioTime", RPCMode.All);
            ChangeScenario();
        }
    }





    // Complete level when the final item is being put into the box
    // called in Tutorial_Mailbox.cs. WaitThenSendMailboxAway()
    public void FinalCombinationOfScenarioSucceded()
    {
        string message = "You made it.";
        textManager.ShowMesageForSeconds_trigger(message, 5.0f);
        print("You made it.");
        withinScenarioTime = false;
        // networkView.RPC ("ExitScenarioTime", RPCMode.All);
        //		networkView.RPC ("ScenarioSuccessfullyMade", RPCMode.All);


        // reached max level number
        if (currentLevelNumber == (totalLevelNumber - 1))
        {
            currentLevelNumber = 0;
            print("Resetted: currentLevelNumber: " + currentLevelNumber + " | totalLevelNumber: " + (totalLevelNumber - 1));
            feedbackManager.OnCorrectCombination();
        }

        // not yet reached max level
        else if (currentLevelNumber < (totalLevelNumber - 1))
        {
            print("currentLevelNumber: " + currentLevelNumber + " | totalLevelNumber: " + (totalLevelNumber - 1));
            feedbackManager.OnCorrectCombination();
            currentLevelNumber++;
            print("Incremented: currentLevelNumber: " + currentLevelNumber + " | totalLevelNumber: " + (totalLevelNumber - 1));



        }

        // networkView.RPC ("UpdateLevelNumber", RPCMode.All, (gameplay.currentLevelNumber + 1));
        ChangeScenario();

    }

    void ChangeScenario()
    {

        // Drop Mail tube animation
        StartCoroutine(DropAndDestroy_Mailbox());


        // Fade out stopwatch
        textManager.FadeOutText_trigger(textManager.scenarioStopwatchText);

        networkManager.ExitScenarioTime_sender(); // hide stopwatch on tablet if scenario completed or if the time ran out
        // networkView.RPC ("HideScenarioStopwatch", RPCMode.All);
        StartCoroutine(WaitThenHideInteractableItems());  // Clean 
        StartCoroutine(WaitBeforePrepareScenario());
    }

    IEnumerator DropAndDestroy_Mailbox()
    {
        if (tube_placeholder.transform.childCount > 0)
        {
            tube_placeholder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("DropMailbox");
        }
        yield return new WaitForSeconds(3.0f);
        Destroy(tube_placeholder.transform.GetChild(0).gameObject);
        yield return null;
    }
    IEnumerator WaitThenHideInteractableItems()
    {
        yield return new WaitForSeconds(timeBeforeInteractableItemsDisappear);
        print("timeBeforeInteractableItemsDisappear over");


        // fade all Interactable items out and destroy
        if (interactableItems.transform.childCount > 0)
        {
            foreach (Transform interactableItem in interactableItems.transform)
            {
                minimizeDestructor.MinimizeAndDestroy(interactableItem.gameObject);
            }
        }




        yield return new WaitForSeconds(0.8f);

        // fade out and destroy letter
        if (envelope_container.transform.childCount > 0)
        {
            GameObject envelope = envelope_container.transform.GetChild(0).gameObject;
            minimizeDestructor.MinimizeAndDestroy(envelope);
        }
    }

    IEnumerator WaitBeforePrepareScenario()
    {
        // make text opaque
        Color fullOpacity = new Color(textManager.hudMessage.color.r, textManager.hudMessage.color.g, textManager.hudMessage.color.b, 1);
        textManager.hudMessage.color = fullOpacity;

        // Countdown
        float counter = timeUntilNextScenario;
        while (counter > -1.0f)
        {
            textManager.hudMessage.text = "Next scenario starts in " + counter + " seconds";
            counter--;
            if (counter == 0)
            {
                // Fade out
                textManager.WaitThenFadeOutText_trigger(textManager.hudMessage);

            }
            yield return new WaitForSeconds(1.3f);
        }



        // rise new envelope, new combination
        PrepareScenario();
    }
}

