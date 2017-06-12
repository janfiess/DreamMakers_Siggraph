using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Items_Combinations : MonoBehaviour
{



    [HideInInspector] public List<List<GameObject[]>> possibleScenarios_level1; // store each scenario which have the difficulti level 1 (pillow, eyepudding, dust)
    [HideInInspector] public List<List<GameObject[]>> possibleScenarios_level2; // difficulty level 2 (unicorn)

    


    // save the item prefabs in a dictionary so that you can look them up by their name (e. g. in Tutorial.Gameplay.cs)
    [Header("Interactable Items")]
    public static Dictionary<string, GameObject> itemPrefabs = new Dictionary<string, GameObject>();

    public GameObject tutorial_red, tutorial_blue, tutorial_violet;

    [Header("Combinations (filled in automatically)")]

    /*
	 * Combinations
	 */

    
    [ReadOnly] public GameObject[] tutorial_combination_violet = new GameObject[3];


    // in a level 1 scenario the scenario consists of one combination (List contains 1 Array[3]), in a level 2 scenario the scenario consists of two combination (List contains 2 Array[3])
    public Dictionary<string, List<GameObject[]>> dictionary_allScenarios = new Dictionary<string, List<GameObject[]>>();
    



    void OnEnable() // after Awake() and befor Start()
    {
        /*
		 *  fill items dictionaries
		 */

        itemPrefabs.Add(tutorial_red.name, tutorial_red);
        itemPrefabs.Add(tutorial_blue.name, tutorial_blue);
        itemPrefabs.Add(tutorial_violet.name, tutorial_violet);


        /** 
		 configure Combinations
		 */

        // Tutorial combination violet

        tutorial_combination_violet[0] = tutorial_violet;   // [0]: result item
        tutorial_combination_violet[1] = tutorial_red;      // [1]: ingredient 1
        tutorial_combination_violet[2] = tutorial_blue;     // [2]: ingredient 2

      

        /*
		 * Configure the scenarios for each level (1 level has one scenario, 1 scenario requires 1 or 2 combinations)
		 */


        // the final result at first place in the list

        // red + blue = violet (level 1 scenario)
        dictionary_allScenarios.Add("result_tutorial_violet", new List<GameObject[]> { tutorial_combination_violet }); 
      

        // configure the scenario flow continuity
        possibleScenarios_level1 = new List<List<GameObject[]>> {dictionary_allScenarios["result_tutorial_violet"]};
          
    }


}
