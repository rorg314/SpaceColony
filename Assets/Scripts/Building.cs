using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {
    
    // Colony this building is in 
    public Colony colony { get; protected set; }

    public BuildingType buildingType;

    ///////// Consumption/production info /////////

    public BuildingSO buildingSO;
            
    // Power consumption(-)/production(+) of this building
    public int wattage;



    // Base consumption speed (modified by upgrades)
    public int speedModifier = 1;
        
    
    
    ///////// Upgrade info /////////

    



    ///////// Colonist info /////////

    // Number of colonists required to operate this building
    public int workload { get; protected set; }

    // Colonists working in this building
    public List<Colonist> colonists;



    ///////// Building status /////////

    // Is building currently active? (workload requirement satisfied)
    public bool isActive;

    // Is the building currently producing resources?
    public bool isProducing;


    // Prototype constructor
    public Building(BuildingSO buildingSO, RecipeSO recipeSO, RecipeSO buildRecipeSO) {

        this.buildingSO = buildingSO;

        this.buildingType = buildingSO.buildingType;

        this.wattage = buildingSO.wattage;
        this.workload = buildingSO.workload;

    }

    // Instance copy constructor 
    public Building(Building other) {

        this.buildingSO = other.buildingSO;

        this.buildingType = other.buildingType;

        this.wattage = other.wattage;
        this.workload = other.workload;

    }

}
