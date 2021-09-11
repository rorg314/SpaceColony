using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains data for entire universe spanning multuple colonies
// Research queue shared accross colonies

public class Universe {

    // List of all colonies in the universe
    public List<Colony> colonies;

    // List of all planets in the universe
    public List<Planet> planets;


    public Universe() {

        this.colonies = new List<Colony>();
        this.planets = new List<Planet>();

        // Create a planet and colony for testing
        Planet testPlanet = new Planet(this);
        Colony testColony = new Colony(testPlanet);
        ColonyController.instance.activeColony = testColony;
    }


}
