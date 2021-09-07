using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony {

    /// Class that holds colony wide statistics ///

    // Planet this colony is on
    public Planet planet;

    // Colony inventory dict
    public Dictionary<Item, int> itemInventoryDict;


    public Colony(Planet planet) {

        this.planet = planet;
        this.itemInventoryDict = new Dictionary<Item, int>();
        planet.planetColony = this;
        UniverseController.instance.universe.planetColonyDict.Add(planet, this);
        UniverseController.instance.universe.colonyPlanetDict.Add(this, planet);

    }
    
}
