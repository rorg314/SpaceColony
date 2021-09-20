using System.Collections.Generic;

public class Building {

    // Colony this building is in
    public Colony colony { get; protected set; }

    public BuildingType buildingType;

    ///////// Consumption/production info /////////

    public BuildingSO buildingSO;

    // Dict of item-> item amount per recipe
    public Dictionary<ItemType, int> itemAmountDict;

    // Dict of item -> items per second (consumed(-)/produced(+)
    public Dictionary<ItemType, float> itemsPerSecondDict;

    // Dict of item -> seconds per item (consumed(-)/produced(+)
    public Dictionary<ItemType, float> secondsPerItemDict;

    // Dict of item -> ticks per item (consumed(-)/produced(+)
    public Dictionary<ItemType, float> ticksPerItemDict;

    // This dict counts down as items are consumed - item only produced if all left to consume are zero
    public Dictionary<ItemType, int> itemsToConsumeDict;

    // How many ticks the recipe takes to complete
    public int recipeTicks;

    // Counts how many ticks have elapsed since the last recipe completed
    public int ticks;

    // Power consumption(-)/production(+) of this building
    public int wattage;

    // Base consumption speed (modified by upgrades) - modifies crafting speed (total time = recipeTime/craftingSpeed)
    public int craftingSpeed = 1;

    ///////// Upgrade info /////////

    ///////// Colonist info /////////

    // Number of colonists required to operate this building
    public int workload { get; protected set; }

    // Colonists working in this building
    public List<Colonist> colonists;

    ///////// Building status /////////

    // Is building currently active? (workload requirement satisfied)
    public bool isActive;

    //public void CalculateItemsPerSecondDict() {
    //    itemsPerSecondDict.Add(buildingSO.recipe.producedItem, buildingSO.recipe.recipeTime / buildingSO.recipe.producedItemAmount);

    //    foreach (ItemType item in buildingSO.recipe.consumedItems) {
    //        itemsPerSecondDict.Add(item, -buildingSO.recipe.recipeTime / buildingSO.recipe.consumedItemsAmount[buildingSO.recipe.consumedItems.IndexOf(item)]);
    //    }

    //    foreach (ItemType item in buildingSO.recipe.byproductItems) {
    //        itemsPerSecondDict.Add(item, buildingSO.recipe.recipeTime / buildingSO.recipe.byproductItemsAmount[buildingSO.recipe.byproductItems.IndexOf(item)]);
    //    }

    //    //BuildingController.instance.RecalculateItemsPerSecond(this.buildingSO.recipe, this);

    //}

    // Prototype constructor
    public Building(BuildingSO buildingSO) {
        this.buildingSO = buildingSO;

        this.buildingType = buildingSO.buildingType;

        this.wattage = buildingSO.wattage;
        this.workload = buildingSO.workload;

        this.ticks = 0;
        this.recipeTicks = 0;
        this.isActive = false;

        this.ticksPerItemDict = new Dictionary<ItemType, float>();
        this.itemsPerSecondDict = new Dictionary<ItemType, float>();
        this.secondsPerItemDict = new Dictionary<ItemType, float>();
        this.itemAmountDict = new Dictionary<ItemType, int>();
        this.itemsToConsumeDict = new Dictionary<ItemType, int>();
        //CalculateItemsPerSecondDict();
        // Doing this when create all prototypes
        //BuildingController.instance.CalculateItemsPerSecond(buildingSO.recipe, this);
    }

    // Instance copy constructor
    public Building(Building other) {
        this.buildingSO = other.buildingSO;

        this.buildingType = other.buildingType;

        this.wattage = other.wattage;
        this.workload = other.workload;
        this.isActive = true;

        this.ticks = other.ticks;
        this.recipeTicks = other.recipeTicks;
        this.ticksPerItemDict = other.ticksPerItemDict;
        this.itemsPerSecondDict = other.itemsPerSecondDict;
        this.itemAmountDict = other.itemAmountDict;
        this.secondsPerItemDict = other.secondsPerItemDict;
        this.itemsToConsumeDict = other.itemsToConsumeDict;
    }
}