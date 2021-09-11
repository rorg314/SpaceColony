using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe Data", menuName = "Data/Recipe Data")]
public class RecipeSO : ScriptableObject {


    //////////// CONSUMED ITEMS ////////////

    // List of consumed resources
    public List<ItemType> consumedItems;

    // Dict of item -> units (consumed(-)/produced(+) per recipe 
    public Dictionary<ItemType, int> itemAmountDict;

    


    //////////// PRODUCED ITEMS ////////////

    // List of items + byproducts produced by this producer
    public List<ItemType> allProducedItems;

    // Item produced by this recipe
    public ItemType producedItem;
    // Byproduct items
    public List<ItemType> byproducts;
    

    // How long the recipe takes to produce
    public int recipeTime;

    // Building that uses this recipe
    public Building building;


}
