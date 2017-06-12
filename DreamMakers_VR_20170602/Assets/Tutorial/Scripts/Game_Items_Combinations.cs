using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Items_Combinations : MonoBehaviour
{
    [HideInInspector] public List<List<GameObject[]>> possibleScenarios_level1; // store each scenario which have the difficulti level 1 (pillow, eyepudding, dust)
    [HideInInspector] public List<List<GameObject[]>> possibleScenarios_level2; // difficulty level 2 (unicorn)

    // save the item prefabs in a dictionary so that you can look them up by their name (e. g. in Tutorial.Gameplay.cs)
    [Header("Interactable Items")]
    public static Dictionary<string, GameObject> itemPrefabs = new Dictionary<string, GameObject>();

    public GameObject
    anchor_prefab,
    bikini_prefab,
    bird_prefab,
    canon_prefab,
    diamond_prefab,
    feather_prefab,
    fish_prefab,
    heart_prefab,
    horse_prefab,
    horseshoe_prefab,
    mermaid_prefab,
    moon_prefab,
    pegasus_prefab,
    pecil_prefab,
    phoenix_prefab,
    pillow_prefab,
    pirateShip_prefab,
    rainbow_prefab,
    rainClouds_prefab,
    spaceShip_prefab,
    star_prefab,
    sun_prefab,
    unicorn_prefab,
    emptyItem_prefab;


    [Header("Combinations (filled in automatically)")]

    /*
	 * All Combinations - occur in level 1 and/or 2 - some scenarios consist of 2 combinations, eg. Horseshoue + Heart = Horse AND Horse + Pencil = Unicorn
	 */

    [ReadOnly] public GameObject[] combination_horse = new GameObject[3];
    [ReadOnly] public GameObject[] combination_bird = new GameObject[3];
    [ReadOnly] public GameObject[] combination_pirateShip = new GameObject[3];
    [ReadOnly] public GameObject[] combination_mermaid = new GameObject[3];
    [ReadOnly] public GameObject[] combination_pillow = new GameObject[3];
    [ReadOnly] public GameObject[] combination_rainbow = new GameObject[3];
    [ReadOnly] public GameObject[] combination_star = new GameObject[3];
    [ReadOnly] public GameObject[] combination_unicorn = new GameObject[3];
    [ReadOnly] public GameObject[] combination_pegasus = new GameObject[3];
    [ReadOnly] public GameObject[] combination_phoenix = new GameObject[3];
    [ReadOnly] public GameObject[] combination_spaceShip = new GameObject[3];



    // in a level 1 scenario the scenario consists of one combination (List contains 1 Array[3]), in a level 2 scenario the scenario consists of two combination (List contains 2 Array[3])
    public Dictionary<string, List<GameObject[]>> dictionary_allScenarios = new Dictionary<string, List<GameObject[]>>();




    void OnEnable() // after Awake() and befor Start()
    {
        /*
		 *  fill items dictionary
		 */

        itemPrefabs.Add(anchor_prefab.name, anchor_prefab);
        itemPrefabs.Add(bird_prefab.name, bird_prefab);
        itemPrefabs.Add(canon_prefab.name, canon_prefab);
        itemPrefabs.Add(diamond_prefab.name, diamond_prefab);
        itemPrefabs.Add(feather_prefab.name, feather_prefab);
        itemPrefabs.Add(heart_prefab.name, heart_prefab);
        itemPrefabs.Add(horse_prefab.name, horse_prefab);
        itemPrefabs.Add(horseshoe_prefab.name, horseshoe_prefab);
        itemPrefabs.Add(moon_prefab.name, moon_prefab);
        itemPrefabs.Add(pegasus_prefab.name, pegasus_prefab);
        itemPrefabs.Add(pecil_prefab.name, pecil_prefab);
        itemPrefabs.Add(phoenix_prefab.name, phoenix_prefab);
        itemPrefabs.Add(pillow_prefab.name, pillow_prefab);
        itemPrefabs.Add(pirateShip_prefab.name, pirateShip_prefab);
        itemPrefabs.Add(rainbow_prefab.name, rainbow_prefab);
        itemPrefabs.Add(rainClouds_prefab.name, rainClouds_prefab);
        itemPrefabs.Add(spaceShip_prefab.name, spaceShip_prefab);
        itemPrefabs.Add(star_prefab.name, star_prefab);
        itemPrefabs.Add(sun_prefab.name, sun_prefab);
        itemPrefabs.Add(unicorn_prefab.name, unicorn_prefab);
        itemPrefabs.Add(mermaid_prefab.name, mermaid_prefab);
        itemPrefabs.Add(fish_prefab.name, fish_prefab);
        itemPrefabs.Add(bikini_prefab.name, bikini_prefab);
        itemPrefabs.Add(emptyItem_prefab.name, emptyItem_prefab);

        /** 
		 configure Combinations
		 */

        // Combination Horse

        combination_horse[0] = horse_prefab;           // [0]: result item
        combination_horse[1] = horseshoe_prefab;       // [1]: ingredient 1
        combination_horse[2] = heart_prefab;           // [2]: ingredient 2


        // Combination Bird

        combination_bird[0] = bird_prefab;             // [0]: result item
        combination_bird[1] = heart_prefab;            // [1]: ingredient 1
        combination_bird[2] = feather_prefab;          // [2]: ingredient 2


        // Combination PirateShip

        combination_pirateShip[0] = pirateShip_prefab; // [0]: result item
        combination_pirateShip[1] = canon_prefab;      // [1]: ingredient 1
        combination_pirateShip[2] = anchor_prefab;     // [2]: ingredient 2


        // Combination Mermaid

        combination_mermaid[0] = mermaid_prefab;       // [0]: result item
        combination_mermaid[1] = fish_prefab;          // [1]: ingredient 1
        combination_mermaid[2] = bikini_prefab;        // [2]: ingredient 2


        // Combination Pillow

        combination_pillow[0] = pillow_prefab;         // [0]: result item
        combination_pillow[1] = moon_prefab;           // [1]: ingredient 1
        combination_pillow[2] = feather_prefab;        // [2]: ingredient 2


        // Combination Rainbow

        combination_rainbow[0] = rainbow_prefab;       // [0]: result item
        combination_rainbow[1] = sun_prefab;           // [1]: ingredient 1
        combination_rainbow[2] = rainClouds_prefab;    // [2]: ingredient 2


        // Combination Star

        combination_star[0] = star_prefab;             // [0]: result item
        combination_star[1] = moon_prefab;             // [1]: ingredient 1
        combination_star[2] = sun_prefab;              // [2]: ingredient 2


        // Combination Unicorn

        combination_unicorn[0] = unicorn_prefab;       // [0]: result item
        combination_unicorn[1] = horse_prefab;         // [1]: ingredient 1
        combination_unicorn[2] = pecil_prefab;         // [2]: ingredient 2


        // Combination Pegasus

        combination_pegasus[0] = pegasus_prefab;       // [0]: result item
        combination_pegasus[1] = horse_prefab;         // [1]: ingredient 1
        combination_pegasus[2] = feather_prefab;       // [2]: ingredient 2


        // Combination Phoenix

        combination_phoenix[0] = phoenix_prefab;       // [0]: result item
        combination_phoenix[1] = bird_prefab;          // [1]: ingredient 1
        combination_phoenix[2] = sun_prefab;           // [2]: ingredient 2


        // Combination SpaceShip

        combination_spaceShip[0] = spaceShip_prefab;   // [0]: result item
        combination_spaceShip[1] = star_prefab;        // [1]: ingredient 1
        combination_spaceShip[2] = anchor_prefab;      // [2]: ingredient 2







        /****************************************************************************************
		 * Configure the scenarios for each level 
         * (1 level has one scenario, 1 scenario requires 1 or 2 combinations)
         * -> so each scenario is saved into a list even though there is only one Gameobject Array in the list in level 1 scenarios
         * (not yet distinguished bewteeen level 1 or level 2)
		 *****************************************************************************************/


        // the final result at first place in the list

        // Horseshoe + Heart = Horse (level 1 scenario)
        dictionary_allScenarios.Add("result_horse", new List<GameObject[]> { combination_horse });
        // Feather + Heart = Bird (level 1 scenario)
        dictionary_allScenarios.Add("result_bird", new List<GameObject[]> { combination_bird });
        // Canon + Anchor = PirateShip (level 1 scenario)
        dictionary_allScenarios.Add("result_pirateShip", new List<GameObject[]> { combination_pirateShip });
        // Fish + Bikini = Mermaid (level 1 scenario)
        dictionary_allScenarios.Add("result_mermaid", new List<GameObject[]> { combination_mermaid });
        // Moon + Feather = Pillow (level 1 scenario)
        dictionary_allScenarios.Add("result_pillow", new List<GameObject[]> { combination_pillow });
        // Sun + RainClouds = Rainbow (level 1 scenario)
        dictionary_allScenarios.Add("result_rainbow", new List<GameObject[]> { combination_rainbow });
        // Moon + Sun = Star (level 1 scenario)
        dictionary_allScenarios.Add("result_star", new List<GameObject[]> { combination_star });


        // Horseshoe + Heart = Horse -> Horse + Pencil = Unicorn (level 2 scenario) 
        dictionary_allScenarios.Add("result_unicorn", new List<GameObject[]> { combination_unicorn, combination_horse });
        // Horseshoe + Heart = Horse -> Horse + Feather = Pegasus (level 2 scenario) 
        dictionary_allScenarios.Add("result_pegasus", new List<GameObject[]> { combination_pegasus, combination_horse });
        // Feather + Heart = Bird -> Bird + Sun = Phoenix (level 2 scenario) 
        dictionary_allScenarios.Add("result_phoenix", new List<GameObject[]> { combination_phoenix, combination_bird });
        // Moon + Sun = Star -> Star + Anchor = Spaceship (level 2 scenario) 
        dictionary_allScenarios.Add("result_spaceship", new List<GameObject[]> { combination_spaceShip, combination_star });




        /****************************************************************************************
         * configure the scenario flow continuity
         * Sorting scenarios into 2 difficulty levels
         * Difficulty level 1 contains the scenarios with only one combination required 
         * Difficulty level 2 contains the scenarios with two combination required 
         ****************************************************************************************/

        possibleScenarios_level1 = new List<List<GameObject[]>> {
            dictionary_allScenarios["result_horse"],
            dictionary_allScenarios["result_bird"],
            dictionary_allScenarios["result_pirateShip"],
            dictionary_allScenarios["result_mermaid"],
            dictionary_allScenarios["result_pillow"],
            dictionary_allScenarios["result_rainbow"],
            dictionary_allScenarios["result_star"]
        };

        possibleScenarios_level2 = new List<List<GameObject[]>> { 
            dictionary_allScenarios["result_unicorn"],
            dictionary_allScenarios["result_pegasus"],
            dictionary_allScenarios["result_phoenix"],
            dictionary_allScenarios["result_spaceship"]
        };
    }
}