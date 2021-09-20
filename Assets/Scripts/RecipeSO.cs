using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Data", menuName = "Data/Recipe Data")]
public class RecipeSO : ScriptableObject {
    //////////// CONSUMED ITEMS ////////////

    // List of consumed resources
    public List<ItemType> consumedItems;

    public List<int> consumedItemsAmount;

    // Dict of item -> units (consumed(-)/produced(+) per recipe
    public Dictionary<ItemType, int> itemAmountDict = new Dictionary<ItemType, int>();

    //////////// PRODUCED ITEMS ////////////

    // Item produced by this recipe
    public ItemType producedItem;

    public int producedItemAmount;

    // Byproduct items
    public List<ItemType> byproductItems;

    public List<int> byproductItemsAmount;

    // How long the recipe takes to produce
    public int recipeTime;

    // Building that uses this recipe
    public Building building;
}