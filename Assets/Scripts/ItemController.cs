using System;
using System.Collections.Generic;
using UnityEngine;

// Controls unlocking items, creating items etc

public class ItemController : MonoBehaviour {
    public static ItemController instance;

    public Dictionary<ItemType, Item> itemTypeItemDict;

    public Dictionary<ItemType, Sprite> itemSpriteDict;

    public void Start() {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one item controller!");
        }

        itemTypeItemDict = new Dictionary<ItemType, Item>();
        itemSpriteDict = new Dictionary<ItemType, Sprite>();
        // Initialise all item objects
        InitialiseAllItems();
    }

    // Initialise all item objects
    private void InitialiseAllItems() {
        foreach (ItemType type in Enum.GetValues(typeof(ItemType))) {
            // Create the item instance
            Item item = new Item(type);
            // Store in dict
            itemTypeItemDict.Add(type, item);
        }

        LoadAllItemSprites();
    }

    private void LoadAllItemSprites() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Items/");

        foreach (Sprite s in sprites) {
            ItemType item = (ItemType)Enum.Parse(typeof(ItemType), s.name);
            itemSpriteDict.Add(item, s);
        }
    }

    public void UnlockItem(ItemType type) {
        itemTypeItemDict[type].researched = true;
    }
}