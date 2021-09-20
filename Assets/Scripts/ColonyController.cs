using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Methods for controlling individual colony stats

public class ColonyController : MonoBehaviour {

    public static ColonyController instance;

    public Colony activeColony;

    public void Start() {
        
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one colony controller!");
        }

                
    }


    public void UpdateActiveColony() {
        
        UpdatePower(activeColony);

        UpdateAllBuildingRecipeTicks(activeColony);

    }


    public void UpdatePower(Colony colony) {

        Colony.Power power = colony.power;

        // Calculate pos and neg watts

        power.posWatts++;

        // Calculate net power
        power.netWatts = power.posWatts - power.negWatts;

        // Write power panel string
        power.powerPanelString = "+ " + power.posWatts.ToString() + "W - " + power.negWatts.ToString() + "W = " + power.netWatts.ToString() + "W";
    }

    public void UpdateAllBuildingRecipeTicks(Colony colony) {

        foreach(BuildingType type in BuildingController.instance.buildingTypeInstanceListDict.Keys) {
            List<Building> allBuildings = BuildingController.instance.buildingTypeInstanceListDict[type];
            if(allBuildings.Count > 0) {
                foreach (Building b in allBuildings) {
                    if (b.isActive) {

                        b.ticks++;
                        
                        // Check for items left needing to be consumed
                        foreach (ItemType item in b.ticksPerItemDict.Keys) {
                            if (b.ticks % b.ticksPerItemDict[item] == 0 && b.itemAmountDict[item] < 0) {
                                ConsumeItem(colony, b, item);
                            }
                        }

                        // Check if reached end of recipe time
                        ItemType producedItem = b.buildingSO.recipe.producedItem;
                        if (b.ticks % b.ticksPerItemDict[producedItem] == 0 && b.itemAmountDict[producedItem] > 0) {
                            bool complete = false;
                            foreach (ItemType consumed in b.itemsToConsumeDict.Keys) {
                                if (b.itemsToConsumeDict[consumed] == 0) {
                                    complete = true;
                                    continue;
                                }
                                else {
                                    complete = false;
                                    break;
                                }
                            }
                            if (complete) {
                                ProduceItem(colony, b, producedItem);
                                b.ticks = 0;
                            }

                        }
                        
                    }
                        
                }
            }

        }



    }

    public void ConsumeItem(Colony colony, Building building, ItemType item) {

        if(colony.itemInventoryDict[item] > 0 && building.itemsToConsumeDict[item] > 0) {
            colony.itemInventoryDict[item] -= 1;
            
            building.itemsToConsumeDict[item] -= 1;
            
            UiController.instance.UpdateItemCard(item, colony.itemInventoryDict[item]);
        }

    }

    public void ProduceItem(Colony colony, Building building, ItemType item) {

        colony.itemInventoryDict[item] += 1;
        BuildingController.instance.CopyItemAmountDict(building.itemAmountDict, building.itemsToConsumeDict);
        UiController.instance.UpdateItemCard(item, colony.itemInventoryDict[item]);

    }
}
