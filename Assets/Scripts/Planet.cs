using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet {

    // Amount of each resource item in this planet
    public Dictionary<Item, int> planetItemCount;

    // Background sprite for this planet
    public Sprite planetBackground;

    // The universe this planet is in
    public Universe universe;

    // The colony on this planet
    public Colony colony;

    public Planet(Universe universe) {

        this.universe = universe;
        universe.planets.Add(this);

    }


}
