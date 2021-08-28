using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    // Primitive resources
    Power, Oxygen, Water, Hydrogen, CO2, 
    
    // Foods
    BasicFood, GourmetFood, Fruit, Vegetables, Meat, SynthMeat,
    
    // Refinable items
    Ice, Rock, Coal, CrudeOil, MetalOre, RareMetalOre, UraniumOre, SiliconOre,
    
    // Refined items
    Sand, Metals, RareMetals, Petroleum, Aromatics, EnrichedUranium, Silicon,  

    // Fabricated items
    Concrete, BasicCircuit, AdvCircuit, MachineParts, Polymers, BasicMeds, AdvMeds, 

    // Advanced Items
    OpticalFibres, QuantumProcessor, WeightlessMaterial, QuarkGluonPlasma, 

}


public class Item {


    public ItemType itemType {get; protected set;}




}
