using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {
    
    // Colony this building is in 
    public Colony colony { get; protected set; }



    ///////// Consumption/production info /////////

    // Recipe to build this building
    public RecipeSO buildRecipe;

    // Recipe object produced by this building
    public RecipeSO recipe;
    
    // Power consumption/production of this building
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

}
