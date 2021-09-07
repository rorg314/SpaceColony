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

    // Dict of planet -> colony
    public Dictionary<Planet, Colony> planetColonyDict;
    // Dict of colony -> planet
    public Dictionary<Colony, Planet> colonyPlanetDict;

    public Universe() {

        this.colonies = new List<Colony>();
        this.planets = new List<Planet>();

    }


}
