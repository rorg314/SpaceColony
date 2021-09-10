using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : MonoBehaviour {


    public static UniverseController instance;

    public Universe universe;

    

    public void Start() {
        
        if (instance == null) {
            instance = this;
        }

        if (universe == null) {
            universe = new Universe();
        }
        else {
            Debug.LogError("Cannot create more than one universe!");
        }

        MasterController.instance.onTick += UpdateAllPlanets;

    }

    public void UpdateAllPlanets() {

        foreach(Planet p in universe.planets) {

            //Update planet colony
            ColonyController.instance.UpdateColony(p.colony);

        }

    }


}
