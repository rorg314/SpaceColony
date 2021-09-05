using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe Data", menuName = "Data/Recipe Data")]
public class RecipeSO : ScriptableObject {


    //////////// CONSUMED ITEMS ////////////

    // List of consumed resources
    public List<ItemType> consumedItems;

    // Dict of resource -> units consumed 
    public Dictionary<ItemType, int> itemConsumptionAmountDict;


    //////////// PRODUCED ITEMS ////////////

    // List of items + byproducts produced by this producer
    public List<ItemType> allProducedItems;

    // Item produced by this recipe
    public ItemType producedItem;
    // Byproduct items
    public List<ItemType> byproducts;

    // Dict of resource -> units produced
    public Dictionary<ItemType, int> itemProductionAmountDict { get; protected set; }

    // How long the recipe takes to produce
    public int recipeTime;

    // Building that uses this recipe
    public Building building;


}
