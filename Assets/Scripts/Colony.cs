using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class that holds colony wide statistics ///
public class Colony {

    // Planet this colony is on
    public Planet planet;

    //////////// Colony Resources ////////////

    // Colony inventory dict
    public Dictionary<Item, int> itemInventoryDict;

    // Building number dict
    public Dictionary<Building, int> buildingNumberDict;

    //Colony power consumption
    public Power power { get; protected set; }
    public class Power {
        // Produced watts
        public int posWatts;
        // Consumed watts stored as positive integer
        public int negWatts;
        // Net watts
        public int netWatts;
        // Joules stored - stored joules are consumed in watts per second
        public int joulesStored;

        //Power panel string to display
        public string powerPanelString;

        public Power(int posWatts, int negWatts, int joulesStored) {
            this.posWatts = posWatts;
            this.negWatts = negWatts;
            this.joulesStored = joulesStored;

            this.netWatts = posWatts - negWatts;
            this.powerPanelString = "Init";

        }

    }
    
    


    public Colony(Planet planet) {

        // Associate planet <-> colony
        this.planet = planet;
        planet.colony = this;
        // Add to universe
        planet.universe.colonies.Add(this);


        // Set up item and building number dicts
        this.itemInventoryDict = new Dictionary<Item, int>();
        this.buildingNumberDict = new Dictionary<Building, int>();

        this.power = new Power(10, 0, 0);

    }
    
}
