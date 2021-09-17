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


    public void UpdateColony(Colony colony) {

        UpdatePower(colony);

        UpdateAllBuildingRecipeTicks(colony);

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

        foreach(BuildingType type in Enum.GetValues(typeof(ItemType))) {

            List<Building> allBuildings = BuildingController.instance.buildingTypeInstanceListDict[type];
            foreach(Building b in allBuildings) {
                if(b.ticks < b.recipeTicks) {
                    b.ticks++;
                }
                
                foreach(ItemType item in b.ticksPerItemDict.Keys) {
                    if( b.ticksPerItemDict[item] >= b.ticks ) {
                        ConsumeItem(colony, item);
                    }
                }
            }


        }



    }

    public void ConsumeItem(Colony colony, ItemType item) {

        if(colony.itemInventoryDict[item] > 0) {
            colony.itemInventoryDict[item] -= 1;
        }

    }
}
