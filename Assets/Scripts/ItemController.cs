using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controls unlocking items, creating items etc

public class ItemController : MonoBehaviour {

    public static ItemController instance;

    public Dictionary<ItemType, Item> itemTypeItemDict;

    


    public void Start() {
        
        if(instance != null) {
            instance = this;
        }
        
        // Initialise all item objects
        InitialiseAllItems();
    }

    // Initialise all item objects
    private void InitialiseAllItems() {

        itemTypeItemDict = new Dictionary<ItemType, Item>();
        
        throw new NotImplementedException();
    }


    public void UnlockItem(ItemType type) {

        itemTypeItemDict[type].researched = true;

    }


}
