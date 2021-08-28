using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {
    
    // Colony this building is in 
    public Colony colony { get; protected set; }

    
    ///////// Consumption/production info /////////

    // Consumer component attached to this building
    public ConsumerSO consumer { get; protected set; }

    // Producer component attached to this building
    public ProducerSO producer { get; protected set; }

    // Power consumption of this building
    public int wattage;

    // Base consumption speed (modified by upgrades)
    public int consumptionSpeed = 1;

    // Base production speed (modified by upgrades)
    public int productionSpeed = 1;

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
