using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Methods for controlling individual colony stats

public class ColonyController : MonoBehaviour {

    public static ColonyController instance;

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

    public void UpdateInventory(Colony colony) {





    }
}
