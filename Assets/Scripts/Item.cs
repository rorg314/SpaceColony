using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    // Test item
    TestItem,
    
    // Research items
    BasicResearch, AdvResearch, QuantumResearch,
    
    // Primitive resources
    Oxygen, Water, Hydrogen, CO2, Nitrogen,
    
    // Foods
    BasicFood, GourmetFood, Algae, FruitVeg, Meat, SynthMeat,
     
    // Basic resources 
    Ice, Rock, Coal, CrudeOil, MetalOre, 
    
    // Adv resources
    RareMetalOre, UraniumOre, SiliconOre, Tritium,
    
    // Refined items
    Sand, Metals, RareMetals, Petroleum, Aromatics, EnrichedUranium, Silicon,  

    // Fabricated items
    Concrete, BasicCircuit, MachineParts, Polymers, BasicMeds, AdvMeds, 

    // Adv Fabricated
    OpticalFibres, Graphene, Nanomaterial, AdvCircuit, Processor,

    // Quantum Items
    Superconductors, QuantumProcessor, WeightlessMaterial, QuarkGluonPlasma, Antihydrogen, 

}


public class Item {

    // The type of item
    public ItemType itemType {get; protected set;}

    
    // Is this item currently researched (unlocked)?
    public bool researched;

    // Sprite for this item
    public Sprite itemSprite;

    

    public Item(ItemType type) {

        this.itemType = type;
        this.researched = false;

        ItemController.instance.itemTypeItemDict.Add(type, this);

        this.itemSprite = Resources.Load<Sprite>("Sprites/Items/" + itemType.ToString());
    }

    

}
