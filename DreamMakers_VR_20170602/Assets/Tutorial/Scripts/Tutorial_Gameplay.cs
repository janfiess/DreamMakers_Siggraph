// this script is attached to the Manager gameobject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Gameplay : MonoBehaviour
{
    public GameObject interactableItems; // items shall be placed into this empty gameobject
    Tutorial_MinimizeDestructor minimizeDestructor;
    Tutorial_Items_Combinations itemsAndCombinations;
    [HideInInspector] public bool isTutorialScene = true;
    [ReadOnly] public Global_Manager globalManager;

    public GameObject table;
    public GameObject envelope_container;




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
    [HideInInspector] public Tutorial_Text textManager;
    Tutorial_EnvelopeManager envelopeManager;

    // Level 2 can have 2 possible scenarios (generated randomly) dependant of in which level you are:
    // - Result Unicorn (outer list) consists of Horse (inner list) and unicorn (also inner list). Each horse and unicorn are arrays which also store their ingredients
    // - Result Diskobrain (outer list) consists of Desklamp (inner list) and Diskobrain (also inner list). Each Desklamp and Diskobrain are arrays which also store their ingredients
    // - Unicorn example: ListAllPossinleScenariosInThisList = Unicorn = (Unicorn (= Horse + Pencil) + Horse (= Heart + Horseshoe)), Discobrain = (...)
    List<List<GameObject[]>> list_allPossibleScenariosInThisLevel = new List<List<GameObject[]>>();

    void Awake()
    {
        itemsAndCombinations = GetComponent<Tutorial_Items_Combinations>();
        minimizeDestructor = GetComponent<Tutorial_MinimizeDestructor>();
        



        remainingScenarioTime = maxScenarioTime; // stopwatch in seconds
        totalLevelNumber = totalLevelCount;
    }

    void Start()
    {
        globalManager = Global_ReferenceManager.Instance.globalManager;
        envelopeManager = Tutorial_ReferenceManager.Instance.envelopeManager;
        textManager = GetComponent<Tutorial_Text>();
         

        // determine combination, envelope appears (gamestarts when the envelope is opened)
        // Only in tutorial in Start function. Later, this function is called when the tablet player enters the lab
        // PrepareScenario(); 
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            PrepareScenario();
        }
    }


    // called in NetworkManager.helloFromTablet()
    public void PrepareScenario()
    {
        feedbackManager.OnEnterTutorial();

        //feedback.OnPrepareScenario (); // rein
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

        string substring_name_finalObject = name_finalObject.Substring(name_finalObject.IndexOf("_") + 1); // e. g. "Unicorn"
        print(substring_name_finalObject);

        // Instantiate envelope with animation and parent it to the placeholder. Pass the name of the final object for the Letter texture
        envelopeManager.RiseEnvelope(substring_name_finalObject); // only in Tutorial, later: RiseEnvelope();
    }



    // called when the envelope is opened in Tutorial:OpenEnvelope.cs. StartUsing()
    public void StartScenario()
    {
        feedbackManager.OnStartScenario();
        print("StartScenario");
        withinScenarioTime = true;
    }

    // Complete level when the final item is being put into the box
    // called in Tutorial_Mailbox.cs. WaitThenSendMailboxAway()
    
    
    public void FinalCombinationOfScenarioSucceded()
    {
        string message = "Now you are a dream maker!";
        textManager.ShowMesageForSeconds_trigger(message, 5.0f);
        
        withinScenarioTime = false;

        StartCoroutine(WaitThenHideInteractableItems());           
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



        // fade out and destroy letter
        if (envelope_container.transform.childCount > 0)
        {
            GameObject envelope = envelope_container.transform.GetChild(0).gameObject;
            minimizeDestructor.MinimizeAndDestroy(envelope);
        }

        yield return new WaitForSeconds(0.1f);

        // rise lab
        globalManager.GetComponent<BuildUpManager>().BuildUpLabReal ();


        
        textManager.FadeOutText_trigger(textManager.statusText);

        yield return new WaitForSeconds(16.0f);

        // in the tutorial don't go on with the gameplay when the combination is completed. Start the main game instead
        globalManager.EnterMainGame();
    }
}