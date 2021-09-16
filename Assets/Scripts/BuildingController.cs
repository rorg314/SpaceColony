using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingController : MonoBehaviour {


    public static BuildingController instance;


    public Dictionary<BuildingType, Building> buildingPrototypesDict;

    public Dictionary<BuildingType, List<Building>> buildingTypeInstanceListDict;


    private void Start() {
        if(instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one buildingcontroller!");
        }

        buildingTypeInstanceListDict = new Dictionary<BuildingType, List<Building>>();


        // Assign recalculate tick times to onSpeedChanged
        MasterController.instance.onSpeedChanged += RecalculateAllBuildingTickSpeeds;
    }



    // Calculate items per second produced by this building
    public void CalculateItemsPerSecond(RecipeSO recipe, Building building) {

        // Recipe time in seconds (with speed modifier)
        int recipeTime = recipe.recipeTime * building.craftingSpeed;

        foreach (ItemType item in recipe.itemAmountDict.Keys) {

            float ips = recipeTime / recipe.itemAmountDict[item];

            building.itemsPerSecondDict.Add(item, ips);

        }

    }

    public void RecalculateAllBuildingTickSpeeds() {

        foreach(BuildingType type in Enum.GetValues(typeof(ItemType))) {
            if (buildingTypeInstanceListDict.ContainsKey(type)) {
                foreach (Building b in buildingTypeInstanceListDict[type]) {
                    SetTicksPerRecipe(b);
                }
            }
            
        }

    }


    // Convert items per second into ticks per item - calculated each time speed changes
    public void SetTicksPerRecipe(Building building) {

        // Recipe time in seconds (with building speed modifier)
        int baseRecipeTime = building.buildingSO.recipe.recipeTime * building.craftingSpeed;

        // Actual time in seconds for recipe adjusted for game speed
        int realRecipeTime = baseRecipeTime / MasterController.instance.getGameSpeedInt();

        building.recipeTicks = MasterController.instance.GetTicksInInterval(realRecipeTime);

    }


    public void CreateAllBuildingPrototypes() {

        BuildingSO[] buildingSOs = Resources.LoadAll<BuildingSO>("ScriptableObjects/Buildings");

        foreach(BuildingSO bSO in buildingSOs) {

            Building proto = new Building(bSO);
            buildingPrototypesDict.Add(bSO.buildingType, proto);

        }

    }




}
