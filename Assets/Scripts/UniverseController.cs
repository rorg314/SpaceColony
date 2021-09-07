using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : MonoBehaviour {


    public static UniverseController instance;

    public Universe universe;

    public void Awake() {
        
        if (!instance) {
            instance = this;
        }

        if (universe == null) {
            universe = new Universe();
        }
        else {
            Debug.LogError("Cannot create more than one universe!");
        }

    }



}
