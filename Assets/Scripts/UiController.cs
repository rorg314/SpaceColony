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

    // Colony inventory panel
    public GameObject inventoryPanel;
    public GameObject itemCardPrefab;
    private Dictionary<ItemType, GameObject> itemTypeItemCardDict;

    private void Start() {

        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one UI controller!");
        }

        MasterController.instance.onTick += UpdateUI;


        itemTypeItemCardDict = new Dictionary<ItemType, GameObject>();
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
}
