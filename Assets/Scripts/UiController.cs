using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UiController : MonoBehaviour {

    public static UiController instance;

    // Speed modifier panel
    public GameObject speedPanel;

    // Colony power monitor panel
    public GameObject powerPanel;

    // Colony inventory panel
    public GameObject inventoryPanel;
    public GameObject itemCardPrefab;
    public Dictionary<ItemType, GameObject> itemTypeItemCardDict;

    //Building panel
    public GameObject buildingPanel;
    public GameObject buildingCardPrefab;
    public Dictionary<BuildingType, GameObject> buildingTypeCardDict;

    private void Start() {

        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one UI controller!");
        }

        MasterController.instance.onTick += UpdateUI;

        // Item and building card dicts
        itemTypeItemCardDict = new Dictionary<ItemType, GameObject>();
        buildingTypeCardDict = new Dictionary<BuildingType, GameObject>();

        AddAllBuildingCards();
    }

    public void UpdateUI() {

        //Unregister the callback to avoid multiple calls before method finishes
        MasterController.instance.onTick -= UpdateUI;
        
        UpdatePowerPanel();

        UpdateInventoryPanel();

        // Add back for next invoke to execute
        MasterController.instance.onTick += UpdateUI;
    }


    public void UpdatePowerPanel() {

        Colony.Power power = null;
        // Get power stats for active colony
        if(ColonyController.instance.activeColony != null) {
            if(ColonyController.instance.activeColony.power != null) {
                power = ColonyController.instance.activeColony.power;
            }
        }
        if(power == null) {
            return;
        }

        Text powerText = powerPanel.GetComponentInChildren<Text>();

        powerText.text = power.powerPanelString;

    }

    public void UpdateInventoryPanel() {

        Colony activeColony = ColonyController.instance.activeColony;

        if (activeColony != null) {

            foreach (ItemType item in activeColony.itemInventoryDict.Keys) {
                int amount = activeColony.itemInventoryDict[item];
                if (amount > 0) {
                    GameObject itemCard = AddItemCard(item);
                    UpdateItemCard(itemCard, amount);
                }

            }

        }

    }

    public GameObject AddItemCard(ItemType item) {

        // Do not add another card if card already exists
        if (itemTypeItemCardDict.ContainsKey(item)) {
            return itemTypeItemCardDict[item];
        }

        //GameObject itemCard = new GameObject("ItemCard " + itemType.ToString());
        GameObject itemCard = (GameObject)Instantiate(itemCardPrefab, inventoryPanel.transform);
        //itemCard.transform.parent = inventoryPanel.transform;
        itemCard.transform.name = "ItemCard " + item.ToString();


        //SpriteRenderer image = itemCard.GetComponentInChildren<SpriteRenderer>();
        Image image = itemCard.GetComponentInChildren<Image>();
        if (ItemController.instance.itemSpriteDict.ContainsKey(item)) {
            image.sprite = ItemController.instance.itemSpriteDict[item];
            //image.sortingOrder = 1;
            //image.transform.localScale = new Vector3(80, 80, 1);
        }
        else {
            Debug.LogError("Trying to add sprite for item not in dict!");
        }

        itemTypeItemCardDict.Add(item, itemCard);

        return itemCard;
    }

    public void UpdateItemCard(GameObject itemCard, int amount) {

        Text amountText = itemCard.GetComponentInChildren<Text>();

        amountText.text = amount.ToString();

    }

    public void AddAllBuildingCards() {

        foreach (BuildingType type in Enum.GetValues(typeof(BuildingType))) {
            
            GameObject buildingCard = (GameObject)Instantiate(buildingCardPrefab, UiController.instance.buildingPanel.transform);
            buildingCard.transform.name = "BuildingCard: " + type.ToString();
            
            buildingTypeCardDict.Add(type, buildingCard);
            
            Transform[] children = buildingCard.transform.GetComponentsInChildren<Transform>();

            foreach (Transform t in children) {
                //Debug.Log(t.name);
                if (t.name == "BuildingImage") {
                    t.gameObject.GetComponent<Image>().sprite = BuildingController.instance.buildingPrototypesDict[type].buildingSO.sprite;
                }
                if (t.name == "Add") {

                    t.gameObject.GetComponent<Button>().onClick.AddListener(() => BuildingController.instance.invokeBuildingAdded(type));

                }
                if (t.name == "Subtract") {

                    t.gameObject.GetComponent<Button>().onClick.AddListener(() => BuildingController.instance.invokeBuildingRemoved(type));

                }
            }
        }

    }


    public void UpdateBuildingCardNumber(GameObject card, int amount) {

        RectTransform[] children = card.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform t in children) {
            //Debug.Log(t.name);
            if (t.name == "BuildingNumberText") {
                t.gameObject.GetComponent<Text>().text = amount.ToString().Split('.')[0];
            }
        }

    }
}
