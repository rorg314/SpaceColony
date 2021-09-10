using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiController : MonoBehaviour {

    public static UiController instance;

    // Speed modifier panel
    public GameObject speedPanel;

    // Colony power monitor panel
    public GameObject powerPanel;


    private void Start() {

        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one UI controller!");
        }

        MasterController.instance.onTick += UpdateUI;

    }

    public void UpdateUI() {

        UpdatePowerPanel();

    }


    public void UpdatePowerPanel() {

        Colony.Power power = null;
        // Get power stats for active colony
        if(UniverseController.instance.universe.activeColony != null) {
            if(UniverseController.instance.universe.activeColony.power != null) {
                power = UniverseController.instance.universe.activeColony.power;
            }
        }
        if(power == null) {
            return;
        }

        Text powerText = powerPanel.GetComponentInChildren<Text>();

        powerText.text = power.powerPanelString;

    }

}
