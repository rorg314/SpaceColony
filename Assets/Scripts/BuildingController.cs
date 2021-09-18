using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour {


    public static BuildingController instance;

    public event Action<BuildingType> onBuildingAdded;
    public event Action<BuildingType> onBuildingRemoved;


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
        buildingPrototypesDict = new Dictionary<BuildingType, Building>();

        // Assign recalculate tick times to onSpeedChanged
        MasterController.instance.onSpeedChanged += RecalculateAllBuildingTickSpeeds;

        CreateAllBuildingPrototypes();

        onBuildingAdded += addBuildingToColony;
        onBuildingRemoved += RemoveBuildingFromColony;
    }


    // Add item amounts to building dict
    public void SetItemAmountDict(Building proto) {
        RecipeSO recipe = proto.buildingSO.recipe;

        proto.itemAmountDict.Add(recipe.producedItem, recipe.producedItemAmount);
        foreach (ItemType item in recipe.consumedItems) {
            proto.itemAmountDict.Add(item, - recipe.consumedItemsAmount[recipe.consumedItems.IndexOf(item)]);
        }
        foreach (ItemType item in recipe.byproductItems) {
            proto.itemAmountDict.Add(item, recipe.byproductItemsAmount[recipe.byproductItems.IndexOf(item)]);
        }

        recipe.itemAmountDict = proto.itemAmountDict;

    }

    // Calculate items per second produced by this building
    public void CalculateItemsPerSecond(RecipeSO recipe, Building building) {

        // Recipe time in seconds (with speed modifier)
        int recipeTime = recipe.recipeTime * building.craftingSpeed;

        foreach (ItemType item in building.itemAmountDict.Keys) {

            float ips =  (float)building.itemAmountDict[item] / (float)recipeTime;
            float spi = (float)recipeTime / (float)building.itemAmountDict[item];
            if (building.itemsPerSecondDict.ContainsKey(item) == false) {
                building.itemsPerSecondDict.Add(item, ips);
                building.secondsPerItemDict.Add(item, spi);
            }
            else {
                building.itemsPerSecondDict[item] = ips;
                building.secondsPerItemDict[item] = spi;
            }
        }
        //Recipe ticks
        SetTicksPerRecipe(building);
    }

    


    // Convert items per second into ticks per item - calculated each time speed changes
    public void SetTicksPerRecipe(Building building) {

        // Recipe time in seconds (with building speed modifier)
        int baseRecipeTime = building.buildingSO.recipe.recipeTime * building.craftingSpeed;

        // Actual time in seconds for recipe adjusted for game speed
        int realRecipeTime = baseRecipeTime / MasterController.instance.getGameSpeedInt();

        building.recipeTicks = MasterController.instance.GetTicksInRealtimeInterval(realRecipeTime);

        SetTicksPerItem(building);
    }

    public void SetTicksPerItem(Building building) {
        foreach(ItemType item in building.itemsPerSecondDict.Keys) {
            //Ticks in interval (adjusted to game speed)
            if (building.ticksPerItemDict.ContainsKey(item) == false){
                building.ticksPerItemDict.Add(item, MasterController.instance.GetTicksInRealtimeInterval(building.itemsPerSecondDict[item]));
            }
            else {
                building.ticksPerItemDict[item] = MasterController.instance.GetTicksInRealtimeInterval(building.itemsPerSecondDict[item]);
            }

        }
    }

    public void RecalculateAllBuildingTickSpeeds() {

        foreach (BuildingType type in Enum.GetValues(typeof(ItemType))) {
            if (buildingTypeInstanceListDict.ContainsKey(type)) {
                foreach (Building b in buildingTypeInstanceListDict[type]) {
                    SetTicksPerRecipe(b);
                }
            }

        }

    }

    public void CreateAllBuildingPrototypes() {

        BuildingSO[] buildingSOs = Resources.LoadAll<BuildingSO>("ScriptableObjects/Buildings");

        foreach(BuildingSO bSO in buildingSOs) {

            Building proto = new Building(bSO);
            buildingPrototypesDict.Add(bSO.buildingType, proto);
            buildingTypeInstanceListDict.Add(bSO.buildingType, new List<Building>());
            
            SetItemAmountDict(proto);
            CalculateItemsPerSecond(proto.buildingSO.recipe, proto);
        }

    }





    public Building AddBuildingToColony(BuildingType buildingType) {


        Building proto = buildingPrototypesDict[buildingType];

        Building instance = new Building(proto);

        buildingTypeInstanceListDict[buildingType].Add(instance);



        return instance;
    }

    public void invokeBuildingAdded(BuildingType type) {
        onBuildingAdded?.Invoke(type);
    }
    public void invokeBuildingRemoved(BuildingType type) {
        onBuildingRemoved?.Invoke(type);
    }

    // On building added event
    public void addBuildingToColony(BuildingType buildingType) {

        Building proto = buildingPrototypesDict[buildingType];

        Building instance = new Building(proto);

        buildingTypeInstanceListDict[buildingType].Add(instance);

        int amount = buildingTypeInstanceListDict[buildingType].Count;

        UiController.instance.UpdateBuildingCardNumber(UiController.instance.buildingTypeCardDict[buildingType], amount);

        CalculateItemsPerSecond(instance.buildingSO.recipe, instance);

        Debug.Log("Building added");

    }

    public void RemoveBuildingFromColony(BuildingType type) {

        int amount = buildingTypeInstanceListDict[type].Count - 1;

        if (amount < 0) {
            return;
        }

        UiController.instance.UpdateBuildingCardNumber(UiController.instance.buildingTypeCardDict[type], amount);

        Building instance = buildingTypeInstanceListDict[type][0];

        buildingTypeInstanceListDict[type].Remove(instance);

        Debug.Log("Building removed");
    }

    
    
    

}
