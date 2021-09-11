using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controls unlocking items, creating items etc

public class ItemController : MonoBehaviour {

    public static ItemController instance;

    public Dictionary<ItemType, Item> itemTypeItemDict;

    
    


    public void Start() {

        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one item controller!");
        }

        itemTypeItemDict = new Dictionary<ItemType, Item>();

        // Initialise all item objects
        InitialiseAllItems();
    }

    // Initialise all item objects
    private void InitialiseAllItems() {
       
        foreach(ItemType type in Enum.GetValues(typeof(ItemType))) {

            // Create the item instance
            Item item = new Item(type);
            // Store in dict
            itemTypeItemDict.Add(type, item);

        }
        
    }


    public void UnlockItem(ItemType type) {

        itemTypeItemDict[type].researched = true;

    }




    // Calculate items per second produced by this building
    public void CalculateItemsPerSecond(RecipeSO recipe, Building building) {

        // Recipe time in seconds (with speed modifier)
        int recipeTime = recipe.recipeTime * building.craftingSpeed;

        foreach(ItemType item in recipe.itemAmountDict.Keys) {

            float ips = recipeTime / recipe.itemAmountDict[item];

            building.itemsPerSecondDict.Add(item, ips);

        }

    }
    // Convert items per second into items per tick
    public void CalculateItemsPerTick(RecipeSO recipe, Building building) {



    }


}
